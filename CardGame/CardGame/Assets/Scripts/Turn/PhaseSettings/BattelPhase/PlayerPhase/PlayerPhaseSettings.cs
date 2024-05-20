using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Turn.PhaseSettings.BattelPhase.PlayerPhase
{
    public class PlayerPhaseSettings : IPlayerPhaseSettings
    {
        private string buildPhase, permutationPhase, battlePhase, attackPhase, _endPhase;
        
        #region IsPhase

        private bool beginBuildPhase, beginPenutationFase, _beginBattleFase, _beginAttackPhase, _beginEndPhase;
   
        public bool _isBuildPhase => beginBuildPhase;
        public bool _isPenutationPhase => beginPenutationFase;
        public bool _isBattlePhase => _beginBattleFase;
        public bool _isEndPhase => _beginEndPhase;
        public bool _isAttackPhase => _beginAttackPhase;
   
        private bool isFirstTurn;

        #endregion
        
        private HandlerController _handlerController;
        private ChangePhaseController _changePhase;
        private IRepackLists _repackLists;
        private IBeginAttackUnits _beginAttackUnits;
        
        [Inject] private TokenEffectOnOpponent _tokenEffectOnOpponent;

        
        public void BeginBuildPhase(List<CardPrefab> cardsHand, List<CardPrefab> cardsZone, ChangePhaseController changePhase)
        {
            _changePhase = changePhase;
            
            if (!isFirstTurn)
            {
                foreach (var vCard in cardsHand)
                {
                    _handlerController = vCard.GetComponent<HandlerController>();
                    _handlerController.SettingsHandlersFromHand(_isBuildPhase);
                }
                cardsZone.Clear();
            }
            buildPhase = "Build phase";
            #region BuildFase

            if (_beginEndPhase)
            {
                _changePhase.ChangePhase(ref _beginEndPhase, ref beginBuildPhase, "End build phase", buildPhase);
            }
            else
            {
                _changePhase.FirstTurn(ref beginBuildPhase, "End build phase", buildPhase);
            }
            #endregion
        }
        
        public void BeginPenutationPhase(IBeginAttackUnits beginAttackUnits ,IRepackLists repackLists, CardZoneController _zoneCards)
        {
            if (_repackLists == null)
            {
                _repackLists = repackLists;
            }
            
            if (_beginAttackUnits == null)
            {
                _beginAttackUnits = beginAttackUnits;
            }
            
            permutationPhase = "Permutation phase";
            _changePhase.ChangePhase(ref beginBuildPhase, ref beginPenutationFase, "End permutation phase", permutationPhase);
      
            _repackLists.FirstRepackList();
            _repackLists.SecondRepackList(_isBuildPhase);
      
            _zoneCards.ResetCountHandler();
        }
    
        public void BeginBattle(List<CardPrefab> cardsZone)
        {
            foreach (var vCard in cardsZone)
            {
                _handlerController = vCard.GetComponent<HandlerController>();
                _handlerController.BeginBattleFase();
            }
            #region ChangeFase
            battlePhase = "Battle phase";
            _changePhase.ChangePhase(ref beginPenutationFase, ref _beginBattleFase, "End battle phase", battlePhase);
            #endregion
        }
    
        public void BeginAttackPhase(List<CardPrefab> cardsZone)
        {
            foreach (var vCard in cardsZone)
            {
                _handlerController = vCard.GetComponent<HandlerController>();
                _handlerController.BeginAttackPhase();
            }

            attackPhase = "Attack phase";
            _changePhase.ChangePhase(ref _beginBattleFase, ref _beginAttackPhase, "End attack phase", attackPhase);
            EndPhase();
        }
    
        public void EndPhase()
        {
            _endPhase = "End phase";
            _changePhase.ChangePhase(ref _beginAttackPhase, ref _beginEndPhase, "End turn", _endPhase);
            isFirstTurn = false;
        }
        
        private void DealDamageFromTokensTheEndTurn()
        {
            if (_tokenEffectOnOpponent._deathTokenInEnemy.Count > 0)
            {
                _tokenEffectOnOpponent.DealDamageFromDeathToken();
            }
        }
        
        public void BeginEnemyTurn()
        {
            _beginEndPhase = false;
     
            DealDamageFromTokensTheEndTurn();
        }
    }
}