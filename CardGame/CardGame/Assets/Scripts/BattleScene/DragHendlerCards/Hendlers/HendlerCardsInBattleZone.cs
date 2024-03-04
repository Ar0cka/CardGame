using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.XR;


public class HendlerCardsInBattleZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region SettingsEmpetyObject
    
    private GameObject _gameCardEmpety;
    private int originalIndex;
    [SerializeField] private RectTransform _settingsEmpetyCard;

    #endregion
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CardPrefab cardPrefab;
    
    private string zoneTag;

    private HendlerCardsInBattleZone _hendler;

    private DropCardInPanel _dropCard;
    private InitializeObjectToPool _objectToPool;

    private bool cardsTypes;
    private bool LayerMasksSet;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        cardPrefab = GetComponent<CardPrefab>();

        _hendler = GetComponent<HendlerCardsInBattleZone>();
        
        _dropCard = FindObjectOfType<DropCardInPanel>();
        _objectToPool = FindObjectOfType<InitializeObjectToPool>(); 

        #region BoolCards

        cardsTypes = cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.AttackRangeHuman
                     || cardPrefab._cardInfo.subtype == CardInfo.SubtypeCard.DefenseBuild;
        #endregion
        
        _hendler.enabled = false;
    }

    private void FixedUpdate()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        int ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
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
        
       _gameCardEmpety.transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag)); 
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
        CreateFakeGameObject();
        
        if (cardsTypes)
        transform.SetParent(_dropCard._hendlerZone.transform);
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
                _dropCard.ChangeLane(zoneTag, cardPrefab);
                transform.SetParent(_dropCard.battleZone.transform);
                Destroy(_gameCardEmpety);
            }
            
            else
            {
                transform.SetParent(_dropCard.ReturnCurrentZone(cardPrefab.currentZoneTag));
                gameObject.transform.SetSiblingIndex(_gameCardEmpety.transform.GetSiblingIndex());
                Destroy(_gameCardEmpety);
            }
        }

    }
}
                                                                                            