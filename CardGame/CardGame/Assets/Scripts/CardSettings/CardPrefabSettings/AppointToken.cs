using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppointToken : MonoBehaviour
{
    [Inject] private ICheckToken _checkToken;
    [Inject] private ITokenEffectOnFriendlyCards _tokenEffectOnFriendlyCards;
    
    [SerializeField] private CardPrefab cardPrefab;
    [SerializeField] private AppointToken appointToken;

    private CardZoneController _cardZone;

    [SerializeField] private string friendlyCard = "Card";

    private bool _theRaycastCanWork = false;

    private int _cardCountInTable;

    // Конструктор с инъекцией
    [Inject]
    private void Construct(ICheckToken checkToken, ITokenEffectOnFriendlyCards tokenEffectOnFriendlyCards)
    {
        _checkToken = checkToken;
        _tokenEffectOnFriendlyCards = tokenEffectOnFriendlyCards;
    }

    private void Awake()
    {
        Debug.Log("AppointToken Awake called");
        _cardZone = FindObjectOfType<CardZoneController>();
        
        if (_tokenEffectOnFriendlyCards == null)
        {
            Debug.LogError("TokenEffectOnFriendlyCards injection failed in Awake");
        }
        else
        {
            Debug.Log("TokenEffectOnFriendlyCards injection successful in Awake");
        }
    }

    private void Start()
    {
        Debug.Log("AppointToken Start called");
        
        Debug.Log($"_checkToken is null: {_checkToken == null}");
        Debug.Log($"_tokenEffectOnFriendlyCards is null: {_tokenEffectOnFriendlyCards == null}");

        if (_tokenEffectOnFriendlyCards == null)
        {
            Debug.LogError("TokenEffectOnFriendlyCards injection failed in Start");
        }
        else
        {
            Debug.Log("TokenEffectOnFriendlyCards injection successful in Start");
        }
        
        if (cardPrefab._haveFearToken || cardPrefab._haveLifeToken)
        {
            appointToken.enabled = true;
        }
        else
        {
            appointToken.enabled = false;
        }
    }

    private void Update()
    {
        FindCard();
    }
    
    private void FindCard()
    {
        if (_theRaycastCanWork && _cardCountInTable > 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
                
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        var target = hit.collider.gameObject;
                        
                        var card = target.GetComponent<CardPrefab>();

                        if (card != null && CompareTag(friendlyCard))
                        {
                            ChoseTheDesiredMethod(card);
                        }
                    }
                }
            }
        }
    }
    
    public void BeginAppointTokenInCard()
    {
        _cardCountInTable = _cardZone.ReturnQualityCardsInTable();

        if (_cardCountInTable > 1)
        {
            _theRaycastCanWork = true;
            Time.timeScale = 0f;
        }
    }
    
    private void ChoseTheDesiredMethod(CardPrefab target)
    {
        if (Time.timeScale == 0) 
            Time.timeScale = 1f;
        
        if (cardPrefab._haveFearToken)
            AppointFearTokenInCard(target);
    }

    private void AppointFearTokenInCard(CardPrefab targetCardPrefab)
    {
        if (targetCardPrefab == null)
        {
            Debug.LogError("target prefab null");
        }

        if (_tokenEffectOnFriendlyCards == null)
        {
            Debug.LogError("token effect null reference");
        }
        
        try
        {
            _tokenEffectOnFriendlyCards.AddNewFearTokenOnFriendCard(targetCardPrefab, cardPrefab);
            _tokenEffectOnFriendlyCards.ActionFearTokens(); 
        }
        catch (Exception e)
        {
            Debug.Log("Method not working: " + e.Message);
        }
    }
}
