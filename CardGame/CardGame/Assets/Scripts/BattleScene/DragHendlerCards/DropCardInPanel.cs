using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class DropCardInPanel : MonoBehaviour
{
    private List<CardPrefab> _cardsInBattleZone = new List<CardPrefab>();
    private List<GameObject> _objectInBattleZone = new List<GameObject>();
    [SerializeField] private DeckController _deckController;
    [SerializeField] private Transform _positionBattelZone;

    public void DropNewCardInPanel(CardPrefab cardPrefab)
    {
        _cardsInBattleZone.Add(cardPrefab);
        var card = Instantiate(cardPrefab.gameObject, _positionBattelZone);
        _objectInBattleZone.Add(card);
    }

    public void RemoveCardFromBattleZone(string uniqueID)
    {
        int index = _cardsInBattleZone.FindIndex(card => card.uniqueID == uniqueID);

        if (index >= 0)
        {
            _deckController.DiscardCardFromBattleZone(_cardsInBattleZone[index]);
            _cardsInBattleZone.RemoveAt(index);
            Destroy(_objectInBattleZone[index]);
            _objectInBattleZone.RemoveAt(index);
        }
    }
}
