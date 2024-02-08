using System;
using Unity.VisualScripting;
using UnityEngine;


public class ManagerAbility : MonoBehaviour
{ 
    public event Action<CardInfo> OnCardDroped;
    
    public void DropCard(CardInfo cardInfo)
    {
        OnCardDroped?.Invoke(cardInfo);
    }
}
