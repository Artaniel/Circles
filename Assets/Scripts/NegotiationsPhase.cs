using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiationsPhase : Phase
{
    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = "";
        buttonManager.Wipe();

        buttonManager.AddReplic(Loc.Get("negotiationsImproveRelations"));   //0
        buttonManager.AddReplic(Loc.Get("negotiationsScare"));              //1
        buttonManager.AddReplic(Loc.Get("negotiationsBlackmail"));          //2
        buttonManager.AddReplic(Loc.Get("negotiationsPublishEvidence"));    //3
        buttonManager.AddReplic(Loc.Get("negotiationsPressure"));           //4
        buttonManager.AddReplic(Loc.Get("negotiationsRelifPressure"));      //5

    }

    override protected void InputParcer()
    {
        int id = buttonManager.currentButtonPressedId;
        if (id == 0) //negotiationsImproveRelations
        {
            maintext.text += "UnderCoustruction";
        }
        else if (id == 1) //negotiationsScare
        {
            maintext.text += "UnderCoustruction";
        }
        else if(id == 2) //negotiationsBlackmail
        {
            maintext.text += "UnderCoustruction";
        }
        else if(id == 3) //negotiationsPublishEvidence
        {
            maintext.text += "UnderCoustruction";
        }
        else if(id == 4) //negotiationsPressure
        {
            maintext.text += "UnderCoustruction";
        }
        else if(id == 5) //negotiationsRelifPressure
        {
            maintext.text += "UnderCoustruction";
        }
        EndPhase();
    }
}
