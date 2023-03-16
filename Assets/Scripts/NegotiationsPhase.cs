using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiationsPhase : Phase
{
    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("NegotiationsDescription") + "\n";
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
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose person")} \n";
            DroplistManager.instance.Init(DroplistManager.DroplistType.anotherCharacters, DroplistManager.ReturnDirrection.negotiationImproveRelations);
        }
        else if (id == 1) //negotiationsScare
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose person")} \n";
            DroplistManager.instance.Init(DroplistManager.DroplistType.anotherCharacters, DroplistManager.ReturnDirrection.negotiationThreat);
        }
        else if (id == 2) //negotiationsBlackmail
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose evidence")} \n";
            DroplistManager.instance.Init(DroplistManager.DroplistType.playerEvidences, DroplistManager.ReturnDirrection.negotiationsBlackmailStart);
        }
        else if (id == 3) //negotiationsPublishEvidence
        {
            maintext.text += "UnderCoustruction";
        }
        else if (id == 4) //negotiationsPressure
        {
            maintext.text += "UnderCoustruction";
        }
        else if (id == 5) //negotiationsRelifPressure
        {
            maintext.text += "UnderCoustruction";
        }
        EndPhase();
    }

    public void ImproveRelations(Character character)
    {
        maintext.text += $"{Loc.Get("Improve relations")} {character.charName} was {character.relations[Character.player]} {character.threat[Character.player]}";
        Character.player.ChangeRelationsToMe(character, 20);
        Character.player.ChangeThreatToMe(character, -10);
        maintext.text += $" now {character.relations[Character.player]} {character.threat[Character.player]}";
        EndPhase();
    }

    public void IncreaseThreat(Character character)
    {
        maintext.text += $"{Loc.Get("Improve threat")} {character.charName} was {character.relations[Character.player]} {character.threat[Character.player]}";
        Character.player.ChangeThreatToMe(character, 20);
        Character.player.ChangeRelationsToMe(character, -10);
        maintext.text += $" now {character.relations[Character.player]} {character.threat[Character.player]}";
        EndPhase();
    }

    public void BlackmailStart(Evidence evidence) { 
        
    }
}
