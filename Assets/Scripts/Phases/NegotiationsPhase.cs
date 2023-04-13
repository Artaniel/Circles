using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiationsPhase : Phase
{
    public GameObject blackmailPrefab;
    private int id;

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("NegotiationsDescription") + "\n";
        buttonManager.Wipe();

        buttonManager.AddReplic(Loc.Get("negotiationsImproveRelations"));   //0
        buttonManager.AddReplic(Loc.Get("negotiationsScare"));              //1
        buttonManager.AddReplic(Loc.Get("negotiationsBlackmail"), Character.player.GetEvidencesByPublished(false,true).Count>0);          //2
        buttonManager.AddReplic(Loc.Get("negotiationsPublishEvidence"), Character.player.GetEvidencesByPublished(false, true).Count > 0);    //3
        buttonManager.AddReplic(Loc.Get("negotiationsPressure"));           //4
        buttonManager.AddReplic(Loc.Get("negotiationsRelifPressure"));      //5
    }

    override protected void InputParcer()
    {
        id = buttonManager.currentButtonPressedId;
        if (id == 0) //negotiationsImproveRelations
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose person")} \n";
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetCaractersExceptMe(), this);
        }
        else if (id == 1) //negotiationsScare
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose person")} \n";
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetCaractersExceptMe(), this);
        }
        else if (id == 2) //negotiationsBlackmail
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose evidence")} \n";
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetEvidencesByPublished(false, true), this);            
        }
        else if (id == 3) //negotiationsPublishEvidence
        {
            buttonManager.Wipe();
            maintext.text += $"{Loc.Get("Choose evidence")} \n";
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetEvidencesByPublished(false, true), this);
        }
        else if (id == 4) //negotiationsPressure
        {
            maintext.text += "UnderCoustruction";
            EndPhase();
        }
        else if (id == 5) //negotiationsRelifPressure
        {
            maintext.text += "UnderCoustruction";
            EndPhase();
        }
    }

    override public void ButtonPressed(IButtonable item)
    {
        switch (id)
        {
            case 0:
                ImproveRelations((Character)item);
                break;
            case 1:
                IncreaseThreat((Character)item);
                break;
            case 2:
                BlackmailStart((Evidence)item);
                break;
            case 3:
                PublishEvidence((Evidence)item);
                break;
        }
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
        Blackmail blackmail = Instantiate(blackmailPrefab).GetComponent<Blackmail>();
        blackmail.Init(evidence);        
    }

    public void PublishEvidence(Evidence evidence) {
        evidence.crime.published = true;
        maintext.text += $"Published {evidence.crime.guilty} {evidence.crime.decription}. Now it does almost nothing.";
        //under construction
        EndPhase();
    }
}
