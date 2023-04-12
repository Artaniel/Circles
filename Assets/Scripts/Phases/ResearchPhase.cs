using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchPhase : Phase
{
    //public ChoiseButtonsManager buttonManager;
    public GameObject evidencePrefab;

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("researchDesc");
        buttonManager.Wipe();
        buttonManager.AddReplic(Loc.Get("researchRandom"), Character.player.GetAvailableForPlayerFootprints().Count > 0);     //0
        buttonManager.AddReplic(Loc.Get("researchEvidence"), Character.player.ViableEvidences(false).Count > 0);   //1
        buttonManager.AddReplic(Loc.Get("researchChar"));       //2
        buttonManager.AddReplic(Loc.Get("researchSelf"), Character.player.GetCrimes(true).Count > 0);       //3
    }

    override protected void InputParcer()
    {
        int id = buttonManager.currentButtonPressedId;
        if (id == 0) //researchRandom
        {
            List<Footprint> availableFootprints = Character.player.GetAvailableForPlayerFootprints();
            if (availableFootprints.Count > 0)
            {
                Footprint choisenFootPrint = availableFootprints[Random.Range(0, availableFootprints.Count)]; // get random from list, needed to be coisen by player later                
                Evidence newEvidence = Instantiate(evidencePrefab).GetComponent<Evidence>();
                newEvidence.Init(choisenFootPrint, Character.player);

                newEvidence.RollForFirmness(newEvidence.holder);

                maintext.text += Loc.Get("evidenceFound");
                maintext.text += newEvidence.crime.guilty.charName + "\n";
                maintext.text += "firmness=" + newEvidence.firmnessOfProof + "\n";
            }
            else
            {
                maintext.text += Loc.Get("noEvidence");
            }
            EndPhase();
        }
        else if (id == 1) //researchEvidence
        {
            buttonManager.Wipe();
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetEvidencesByFirmness(false, true), this);
        }
        else if (id == 2) { //researchChar
            buttonManager.Wipe();
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetCaractersExceptMe(), this);
        }
        else if (id == 3) //researchSelf
        {
            buttonManager.Wipe();
            DroplistManager.instance.MakeDropDownFormList(Character.player.GetCrimes(true), this);
        }
        else
        {
            maintext.text += "Placeholder\n"; 
            EndPhase();
        }
    }

    public void DigUnderCharacter(Character character) {
        List<Footprint> viableFootprints = new List<Footprint>();
        foreach (Crime crime in character.crimeList)
            foreach (Footprint footprint in crime.footprintsOfThisCrime)
                viableFootprints.Add(footprint);

        foreach (Evidence evidence in Character.player.evidenceList) {
            if (viableFootprints.Contains(evidence.footPrint))
                if (evidence.firmnessOfProof == 100) // exclude solved crimes
                    viableFootprints.Remove(evidence.footPrint);
        }
        if (viableFootprints.Count != 0)
        {
            Footprint choisenFootprint = viableFootprints[Random.Range(0, viableFootprints.Count)];
            int roll = RollManager.Roll(Character.player.Int + Character.player.investigation);
            if (roll > 0)
            {
                bool isKnown = false;
                Evidence choisenEvidence = null;
                foreach (Evidence evidence in Character.player.evidenceList)
                    if (evidence.footPrint == choisenFootprint)
                    {
                        isKnown = true;
                        choisenEvidence = evidence;
                    }

                if (isKnown)
                {
                    choisenEvidence.firmnessOfProof += roll * 10;
                }
                else
                {
                    choisenEvidence = Instantiate(evidencePrefab).GetComponent<Evidence>();
                    choisenEvidence.Init(choisenFootprint, Character.player);
                }
                if (choisenEvidence.firmnessOfProof > 100)
                    choisenEvidence.firmnessOfProof = 100;

                maintext.text += $"Found {choisenEvidence.crime.guilty.charName} firmness = {choisenEvidence.firmnessOfProof}";
            }
        }
        else
            maintext.text += Loc.Get("noEvidence");
        EndPhase();
    }

    public void ResearchEvidence(Evidence evidence)
    {
        maintext.text += $"was {evidence.firmnessOfProof}\n";
        evidence.firmnessOfProof += RollManager.Roll(Character.player.Per + Character.player.investigation);
        maintext.text += $"now {evidence.firmnessOfProof}\n";
        EndPhase();
    }

    private void CoverCrime(Crime crime)
    {
        maintext.text += $"was {crime.footprintsOfThisCrime[0].difficulty}\n";
        crime.footprintsOfThisCrime[0].difficulty += RollManager.Roll(Character.player.Cha + Character.player.intimidation); // подумать еще про статы
        maintext.text += $"now {crime.footprintsOfThisCrime[0].difficulty}\n";
        EndPhase();
    }

    override public void ButtonPressed(IButtonable item)
    {
        if (item is Character)
            DigUnderCharacter((Character)item);
        else if (item is Evidence)
            ResearchEvidence((Evidence)item);
        else if (item is Crime)
            CoverCrime((Crime)item);
    }
}
