using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPhase : Phase
{
    //public ChoiseButtonsManager buttonManager;

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("researchDesc");
        buttonManager.Wipe();
        buttonManager.AddReplic(Loc.Get("researchRandom"));     //0
        buttonManager.AddReplic(Loc.Get("researchEvidence"));   //1
        buttonManager.AddReplic(Loc.Get("researchChar"));       //2
        buttonManager.AddReplic(Loc.Get("researchSelf"));       //3
    }

    override protected void InputParcer()
    {
        maintext.text += "Placeholder";
        EndPhase();
    }
}
