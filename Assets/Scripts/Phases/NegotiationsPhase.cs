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

        buttonManager.AddReplic(Loc.Get("negotiationsImproveRelations"), ImproveRelations);
        buttonManager.AddReplic(Loc.Get("negotiationsScare"), Scare);
        buttonManager.AddReplic(Loc.Get("negotiationsBlackmail"), Character.player.GetEvidencesByPublished(false,true,true).Count>0, Blackmail);
        buttonManager.AddReplic(Loc.Get("negotiationsPublishEvidence"), Character.player.GetEvidencesByPublished(false, true).Count > 0, PublishEvidence);
        buttonManager.AddReplic(Loc.Get("negotiationsPressure"), AddPressure);
        buttonManager.AddReplic(Loc.Get("negotiationsRelifPressure"), RelifPressure);
    }

    private void ImproveRelations() {
        id = 0;
        buttonManager.Wipe();
        maintext.text += $"{Loc.Get("Choose person")} \n";
        DroplistManager.instance.MakeDropDownFormList(Character.player.GetCaractersExceptMe(), this);
    }

    private void Scare() {
        id = 1;
        buttonManager.Wipe();
        maintext.text += $"{Loc.Get("Choose person")} \n";
        DroplistManager.instance.MakeDropDownFormList(Character.player.GetCaractersExceptMe(), this);
    }

    private void Blackmail() {
        id = 2;
        buttonManager.Wipe();
        maintext.text += $"{Loc.Get("Choose evidence")} \n";
        DroplistManager.instance.MakeDropDownFormList(Character.player.GetEvidencesByPublished(false, true), this);
    }

    private void PublishEvidence() {
        id = 3;
        buttonManager.Wipe();
        maintext.text += $"{Loc.Get("Choose evidence")} \n";
        DroplistManager.instance.MakeDropDownFormList(Character.player.GetEvidencesByPublished(false, true), this);
    }

    private void AddPressure() {
        maintext.text += "UnderCoustruction";
        EndPhase();
    }

    private void RelifPressure() { 
            maintext.text += "UnderCoustruction";
            EndPhase();    
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
        EndPhase();
    }

    public void PublishEvidence(Evidence evidence) {
        evidence.crime.published = true;
        maintext.text += $"Published {evidence.crime.guilty} {evidence.crime.decription}. Now it does almost nothing.";
        //under construction
        EndPhase();
    }
}
