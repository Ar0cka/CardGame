using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class HandlerCardsInBattleZone : AbstractHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region SettingsEmpetyObject
    
    private GameObject _gameCardEmpety;
    private int originalIndex;
    [SerializeField] private RectTransform _settingsEmpetyCard;

    #endregion

    private bool cardsTypes;
    private bool LayerMasksSet;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();
        
        Card = FindObjectOfType<CardZoneController>();

        #region BoolCards

        cardsTypes = cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild;
        #endregion
    }

    private void FixedUpdate()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        int ignoreLayerMask = 1 << LayerMask.NameToLayer("Cards");
        int allLayersExceptIgnore = ~ignoreLayerMask;

        foreach (RaycastResult result in results)
        {
            if (((1 << result.gameObject.layer) & allLayersExceptIgnore) != 0)
            {
                zoneTag = result.gameObject.tag;
                break;
            }
        }

    }
    #region SettingsEmpetyCard

    private void CreateFakeGameObject()
    {
       originalIndex = gameObject.transform.GetSiblingIndex();
       
       _gameCardEmpety = Instantiate(gameObject, _settingsEmpetyCard);
       _gameCardEmpety.transform.SetParent(Card.ReturnCurrentZone(cardPrefab.currentZoneTag)); 
       _gameCardEmpety.transform.SetSiblingIndex(originalIndex);
       
       SetColorSettingsInFakeGameObject(_gameCardEmpety);
    }

    private void SetColorSettingsInFakeGameObject(GameObject _card)
    {
        CanvasRenderer outlineCard = _card.transform.GetChild(0).GetComponent<CanvasRenderer>();
        CanvasRenderer iconCard = _card.transform.GetChild(0).GetChild(0).GetComponent<CanvasRenderer>();

        Color colorCard = Color.white;
        colorCard.a = 0.5f;

        outlineCard.SetColor(colorCard);
        iconCard.SetColor(colorCard);
    }
    #endregion
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Card._countHandler >= 2)
        {
            eventData.pointerDrag = null;
            eventData.dragging = false;
            return;
        }

        isBeginDrag = true;
        CreateFakeGameObject();
        
        if (cardsTypes)
            transform.SetParent(Card._hendlerZone.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (zoneTag != null)
        {
            if (zoneTag == "BattleZone" && cardsTypes)
            {
                Card.ChangeLane(zoneTag, cardPrefab);
                transform.SetParent(Card.battleZone.transform);
                cardPrefab.SetZoneTag(zoneTag);
                
                Card.ChangeCountHandler();
                
                _handlerController.OnHandlerFromBattleZone();
                
                Destroy(_gameCardEmpety);
            }
            
            else
            {
                transform.SetParent(Card.ReturnCurrentZone(cardPrefab.currentZoneTag));
                gameObject.transform.SetSiblingIndex(_gameCardEmpety.transform.GetSiblingIndex());
                Destroy(_gameCardEmpety);
            }
        }
        else
        {
            transform.SetParent(Card.ReturnCurrentZone(cardPrefab.currentZoneTag));
            gameObject.transform.SetSiblingIndex(_gameCardEmpety.transform.GetSiblingIndex());
            Destroy(_gameCardEmpety);
        }

        isBeginDrag = false;
    }
}
                                                                                            