using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Phase : MonoBehaviour
{
    public TextMeshProUGUI maintext;
    public ChoiseButtonsManager buttonManager;
    public bool isInEndPhase = false;
    public Phase nextPhase;

    virtual public void PhaseRun() {
        isInEndPhase = false;
        buttonManager.OnButtonPressed.AddListener(InputEndPhaseCheck);
    }

    virtual public void EndPhase() {
        isInEndPhase = true;
        buttonManager.Wipe();
        buttonManager.AddReplic(Loc.Get("next")); 
    }

    public void InputEndPhaseCheck()
    {
        if (isInEndPhase)
        {
            buttonManager.OnButtonPressed.RemoveAllListeners();
            isInEndPhase = false;
            nextPhase.PhaseRun();
        }
        else InputParcer();
    }

    abstract protected void InputParcer();  //here should be reaction on pressed button
    abstract public void ButtonPressed(IButtonable item); // return call from UI after button pressed
}
