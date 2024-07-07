using System;
using CardSettings.Tokens.EffectOnFriendlyCards;
using UnityEngine;
using UnityEngine.Windows.WebCam;
using Zenject;

namespace CardSettings.Appoint
{
    public class AppointToken : MonoBehaviour
    {
        private IEffectOnFriendlyCard _tokenEffectOnFriendlyCard = new TokenEffectOnFriendlyCards();
        
        [SerializeField] private CardPrefab cardPrefab;
        [SerializeField] private AppointToken appointToken;

        private CardZoneController _cardZone;
        [SerializeField] private string friendlyCard = "Card";

        private bool _theRaycastCanWork = false;
        private int _cardCountInTable;

        private void Awake()
        {
            _cardZone = FindObjectOfType<CardZoneController>();

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
            if (_theRaycastCanWork && _cardCountInTable > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                    foreach (RaycastHit2D hit in hits )
                    {
                        if (hit.collider != null)
                        {
                            Debug.Log(hit.collider);
                            
                            var target = hit.collider.gameObject;
                            var card = target.GetComponent<CardPrefab>();
                            
                            if(card != null)
                            AddNewToken(card);
                        }
                    }
                }
            }
        }

        public void BeginAppointToken()
        {
            if(_tokenEffectOnFriendlyCard == null)
                Debug.LogError("token effect == null");
            
            _cardCountInTable = _cardZone.ReturnCountInCardOnTable();
            _theRaycastCanWork = true;
            Time.timeScale = 0f;
        }
        
        private void AddNewToken(CardPrefab _targetCardPrefab)
        {
            if (_tokenEffectOnFriendlyCard != null)
            {
                _tokenEffectOnFriendlyCard.AddNewTokenInFriendCard(_targetCardPrefab);
                _tokenEffectOnFriendlyCard.ActionFearToken();
            }
            else
            {
                Debug.LogError("_tokenEffectOnFriendlyCard = null");
            }
            _theRaycastCanWork = false;
            Time.timeScale = 1f;
        }
    }
}
                        
                    
                
            
        
    
    
    