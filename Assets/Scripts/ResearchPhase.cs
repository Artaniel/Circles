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
        buttonManager.AddReplic(Loc.Get("researchEvidence"), Character.player.ViableEvidences().Count > 0);   //1
        buttonManager.AddReplic(Loc.Get("researchChar"));       //2
        buttonManager.AddReplic(Loc.Get("researchSelf"));       //3
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
            if (Character.player.evidenceList.Count > 0)
            {
                Evidence evidence = Character.player.evidenceList[Random.Range(0, Character.player.evidenceList.Count)];
                bool rollIsSucessful = evidence.RollForFirmness(Character.player);
                if (rollIsSucessful)
                    maintext.text += Loc.Get("evidenceReserchSucess");
                else
                    maintext.text += Loc.Get("evidenceReserchFail");

                maintext.text += evidence.crime.guilty.charName + "\n";
                maintext.text += "firmness=" + evidence.firmnessOfProof + "\n";
            }
            else
            {
                maintext.text += Loc.Get("evidenceReserchFail");
            }
            EndPhase();
        }
        else if (id == 2) { //researchChar
            buttonManager.Wipe();
            DroplistManager.instance.Init(DroplistManager.DroplistType.anotherCharacters, DroplistManager.ReturnDirrection.reserchPhase);
        }
        else if (id == 3) //researchSelf
        {
            List<Footprint> availableFootprints = new List<Footprint>();
            foreach (Crime crime in Character.player.crimeList)
                foreach (Footprint footprint in crime.footprintsOfThisCrime)
                    if (footprint.difficulty < 10)
                        availableFootprints.Add(footprint);
            if (availableFootprints.Count > 0)
            {
                Footprint choisenFootprint = availableFootprints[Random.Range(0, availableFootprints.Count)];
                //надо както придумать выбор скиллов в зависимости от типа футпринта. Пока пусть будет Cha+intimidation
                maintext.text += "was " + choisenFootprint.difficulty + "\n";
                choisenFootprint.difficulty += RollManager.Roll(Character.player.Cha + Character.player.intimidation);
                maintext.text += "now " + choisenFootprint.difficulty + "\n";
            }
            else
            {
                maintext.text += Loc.Get("evidenceReserchFail");
            }
            EndPhase();
        }
        else
        {
            maintext.text += "Placeholder\n"; 
            EndPhase();
        }
    }

    public void CharacterChoisen(Character character) {
        Debug.Log($"Choisen char name {character.charName}");
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
}
