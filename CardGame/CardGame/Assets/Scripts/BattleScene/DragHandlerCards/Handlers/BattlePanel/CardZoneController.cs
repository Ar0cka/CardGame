using System.Collections.Generic;
using Deck.InitiallizeObjectPool.Interfase;
using UnityEngine;
using Zenject;


public class CardZoneController : MonoBehaviour
{
    private List<CardPrefab> _cardInBattleZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInMilyArmyZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInRangeHumanZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInRangeBuildZone = new List<CardPrefab>(9);

    [SerializeField] private DeckController _deckController;
    [SerializeField] private HandCards _handCards;
    [SerializeField] private ManaController manaController;
    
    [Inject] private ICreateNewObjectToPool _createNewObjectToPool;
    [Inject] private IReturnObjectToPool _returnObjectToPool;

    private int countHandler = 0;
    public int _countHandler => countHandler;
    
    public RectTransform miliArmyZone;
    public RectTransform rangeArmyZone;
    public RectTransform rangeBuildZone;
    public RectTransform battleZone;
    public GameObject _hendlerZone;
    
    public void DropNewCardInPanel(CardPrefab cardPrefab, string zoneTag)
    {
        switch (zoneTag)
        {
            case "MiliArmy":
                _cardsInMilyArmyZone.Add(cardPrefab);
                break;
            
            case "RangeSolder":
                _cardsInRangeHumanZone.Add(cardPrefab);
                break;
            
            case "RangeBuild":
                _cardsInRangeBuildZone.Add(cardPrefab);
                
                break;
        }
        cardPrefab.SetZoneTag(zoneTag);
        _createNewObjectToPool.CreateObjectToBattelZonePool(cardPrefab);
        _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
        manaController.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
    }

    public void RemoveCardFromBattleZone(string uniqueID)
    {
        List<CardPrefab> cardInTable = new List<CardPrefab>();
        repackAllLists(cardInTable);
        
        int index = cardInTable.FindIndex(card => card.uniqueID == uniqueID);

        if (index >= 0)
        {
            _deckController.DiscardCardFromBattleZone(cardInTable[index]);
            _returnObjectToPool.ReturnObjectToHandPool(cardInTable[index]);

            switch (cardInTable[index].currentZoneTag)
            {
                case "MiliArmy":
                    _cardsInMilyArmyZone.Remove(cardInTable[index]);
                    break;
                case "RangeSolder":
                    _cardsInRangeHumanZone.Remove(cardInTable[index]);
                    break;
                case "RangeBuild":
                    _cardsInRangeBuildZone.Remove(cardInTable[index]);
                    break;
                case "BattleZone":
                    _cardInBattleZone.Remove(cardInTable[index]);
                    break;
            }
        }                                                           
    }

    public void ChangeLane(string zoneTag, CardPrefab cardPrefab)
    {
        switch (cardPrefab.currentZoneTag)
        {
            case "MiliArmy":
                _cardsInMilyArmyZone.Remove(cardPrefab);
                break;
            case "RangeSolder":
                _cardsInRangeHumanZone.Remove(cardPrefab);
                break;
            case "RangeBuild":
                _cardsInRangeBuildZone.Remove(cardPrefab);
                break;
            case "BattleZone":
                _cardInBattleZone.Remove(cardPrefab);
                cardPrefab.isBattleZone = false;
                break;
        } 
        
        switch (zoneTag)
        {
            case "MiliArmy":
                _cardsInMilyArmyZone.Add(cardPrefab);
                break;
            
            case "RangeSolder":
                _cardsInRangeHumanZone.Add(cardPrefab);
                break;
            
            case "RangeBuild":
                _cardsInRangeBuildZone.Add(cardPrefab);
                break;
            case "BattleZone":
                _cardInBattleZone.Add(cardPrefab);
                cardPrefab.isBattleZone = true;
                break;
        }
    }

    public RectTransform ReturnCurrentZone(string currentZoneTag)
    {
        switch (currentZoneTag)
        {
            case "MiliArmy":
                return miliArmyZone;
            
            case "RangeSolder":
                return rangeArmyZone;
            
            case "RangeBuild":
                return rangeBuildZone;
            case "BattleZone":
                return battleZone;
                
            default:
                return null;
        }
    }

    public List<CardPrefab> repackAllLists(List<CardPrefab> list)
    {
        foreach (var cards in _cardsInMilyArmyZone)
        {
            list.Add(cards);
        }
        foreach (var cards in _cardsInRangeHumanZone)
        {
            list.Add(cards);
        }
        foreach (var cards in _cardsInRangeBuildZone)
        {
            list.Add(cards);
        }

        foreach (var cards in _cardInBattleZone)
        {
            list.Add(cards);
        }
        return list;
    }

    public int ReturnCountInCardOnTable()
    {
        List<CardPrefab> cardInTable = new List<CardPrefab>();
        repackAllLists(cardInTable);

        return cardInTable.Count;
    }

    public void CheakBattleZone(ref List<CardPrefab> list)
    {
        foreach (var battleZoneCards in _cardInBattleZone)
        {
            list.Add(battleZoneCards);
        }

        foreach (var rangeCards in _cardsInRangeHumanZone)
        {
            list.Add(rangeCards);
        }
        //Добавить в этот метод условие, которое будет проверять, может ли карта защищаться или атаковать.
        //Для этого стоит добавить флаг в кард префаб который будет говорить о боеспособности
    }
    
    public void ChangeCountHandler()
    {
        countHandler++;
    }

    public void ResetCountHandler()
    {
        countHandler = 0;
    }
}
