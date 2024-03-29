using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


public class DropCardInPanel : MonoBehaviour
{
    private List<CardPrefab> _cardInBattleZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInMilyArmyZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInRangeHumanZone = new List<CardPrefab>(9);
    private List<CardPrefab> _cardsInRangeBuildZone = new List<CardPrefab>(9);
    private List<GameObject> _objectInBattleZone = new List<GameObject>(9);

    [SerializeField] private DeckController _deckController;
    [SerializeField] private HandCards _handCards;
    [SerializeField] private InitializeObjectToPool _initializeObject;
    [FormerlySerializedAs("_manaManager")] [SerializeField] private ManaController manaController;

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
                 cardPrefab.SetZoneTag(zoneTag);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                manaController.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
                break;
            
            case "RangeSolder":
                _cardsInRangeHumanZone.Add(cardPrefab);
                 cardPrefab.SetZoneTag(zoneTag);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                manaController.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
                break;
            
            case "RangeBuild":
                _cardsInRangeBuildZone.Add(cardPrefab);
                 cardPrefab.SetZoneTag(zoneTag);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                manaController.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
                break;
        }
    }

    public void RemoveCardFromBattleZone(string uniqueID)
    {
        int index = _cardsInMilyArmyZone.FindIndex(card => card.uniqueID == uniqueID);

        if (index >= 0)
        {
            _deckController.DiscardCardFromBattleZone(_cardsInMilyArmyZone[index]);
            _cardsInMilyArmyZone.RemoveAt(index);
            _objectInBattleZone.RemoveAt(index);
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
