using TMPro;
using Turn.PhaseSettings.BattelPhase.PlayerPhase;
using UnityEngine;

public class ChangePhaseController : MonoBehaviour, IChangePhase
{
    [SerializeField] private TextMeshProUGUI _buttonsEndPhase;
    [SerializeField] private TextMeshProUGUI _phaseTextGUI;
        
    public void ChangePhase(ref bool endPhase, ref bool beginPhase, string buttonText, string PhaseText)
    { 
        _phaseTextGUI.text = PhaseText; 
        endPhase = false;
        beginPhase = true;
        _buttonsEndPhase.text = buttonText;
    }

    public void FirstTurn(ref bool beginPhase, string buttonText, string PhaseText)
    { 
        _phaseTextGUI.text = PhaseText; 
        beginPhase = true; 
        _buttonsEndPhase.text = buttonText;
    }
}
