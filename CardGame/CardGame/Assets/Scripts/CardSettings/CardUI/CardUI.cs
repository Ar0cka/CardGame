using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _hpAndDamageCard;
   [SerializeField] private CardPrefab _card;

   public void UpdateHpAndDamageUI()
   {
      _hpAndDamageCard.text = $"{_card._cardInfo.damage}/{_card._currentHitPoint}";
   }


}
