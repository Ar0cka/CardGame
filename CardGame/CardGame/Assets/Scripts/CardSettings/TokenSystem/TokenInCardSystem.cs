using UnityEngine;
using UnityEngine.Serialization;

namespace CardSettings.CardPrefabSettings
{
    public class TokenInCardSystem : MonoBehaviour, ITokenInCardSystem
    {
        [SerializeField] private bool _canAttack = false;
        
        #region FearTokenSettings
        
        public bool canAttack => _canAttack;
        
        public void CardHaveFearToken()
        {
            _canAttack = false;
        }

        public void DeleteFearToken()
        {
            _canAttack = true;
        }

        #endregion
    }
}
