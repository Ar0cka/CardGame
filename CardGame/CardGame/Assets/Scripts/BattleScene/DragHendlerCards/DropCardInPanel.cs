using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class DropCardInPanel : MonoBehaviour
{
    private List<CardPrefab> _cardsInMilyArmyZone = new List<CardPrefab>();
    private List<CardPrefab> _cardsInRangeHumanZone = new List<CardPrefab>();
    private List<CardPrefab> _cardsInRangeBuildZone = new List<CardPrefab>();
    private List<GameObject> _objectInBattleZone = new List<GameObject>();
    [SerializeField] private DeckController _deckController;
    [SerializeField] private HandCards _handCards;
    [SerializeField] private InitializeObjectToPool _initializeObject;
    [SerializeField] private ManaManager _manaManager;
    
    public RectTransform miliArmyZone;
    public RectTransform rangeArmyZone;
    public RectTransform rangeBuildZone;
    public GameObject _hendlerZone;
    
    public void DropNewCardInPanel(CardPrefab cardPrefab, string zoneTag)
    {
        switch (zoneTag)
        {
            case "MiliArmy":
                _cardsInMilyArmyZone.Add(cardPrefab);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                _manaManager.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
                break;
            
            case "RangeSolder":
                _cardsInRangeHumanZone.Add(cardPrefab);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                _manaManager.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
                break;
            
            case "RangeBuild":
                _cardsInRangeBuildZone.Add(cardPrefab);
                _initializeObject.CreateObjectToBattelZonePool(cardPrefab);
                _handCards.DropCardFromHand(cardPrefab, cardPrefab.uniqueID);
                _manaManager.TakingAwayManaWhenPlayingACard(cardPrefab._cardInfo);
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
}
