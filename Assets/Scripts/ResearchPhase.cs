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
        buttonManager.AddReplic(Loc.Get("researchRandom"));     //0
        buttonManager.AddReplic(Loc.Get("researchEvidence"));   //1
        buttonManager.AddReplic(Loc.Get("researchChar"));       //2
        buttonManager.AddReplic(Loc.Get("researchSelf"));       //3
    }

    override protected void InputParcer()
    {
        int id = buttonManager.currentButtonPressedId;
        if (id == 0) //researchRandom
        {
            List<Footprint> availableFootprints = CharSheet.player.GetAvailableForPlayerFootprints();
            if (availableFootprints.Count > 0)
            {
                Footprint choisenFootPrint = availableFootprints[Random.Range(0, availableFootprints.Count)]; // get random from list, needed to be coisen by player later
                Evidence newEvidence = Instantiate(evidencePrefab).GetComponent<Evidence>();
                newEvidence.footPrint = choisenFootPrint;
                newEvidence.transform.parent = CharSheet.player.transform;
                CharSheet.player.evidenceList.Add(newEvidence);
                newEvidence.holder = CharSheet.player;
                newEvidence.crime = choisenFootPrint.crime;

                newEvidence.RollForFirmness(newEvidence.holder);

                maintext.text += Loc.Get("evidenceFound");
                maintext.text += newEvidence.crime.guilty.charName + "\n";
                maintext.text += "firmness=" + newEvidence.firmnessOfProof + "\n";
            }
            else
            {
                maintext.text += Loc.Get("noEvidence");
            }
        }
        else if (id == 1) //researchEvidence
        {
            if (CharSheet.player.evidenceList.Count > 0)
            {
                Evidence evidence = CharSheet.player.evidenceList[Random.Range(0, CharSheet.player.evidenceList.Count)];
                bool rollIsSucessful = evidence.RollForFirmness(CharSheet.player);
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
        }
        else if (id == 3) //researchSelf
        {
            List<Footprint> availableFootprints = new List<Footprint>();
            foreach (Crime crime in CharSheet.player.crimeList)
                foreach (Footprint footprint in crime.footprintsOfThisCrime)
                    if (footprint.difficulty < 10)
                        availableFootprints.Add(footprint);
            Debug.Log(CharSheet.player.crimeList);
            Debug.Log(CharSheet.player.crimeList[0].footprintsOfThisCrime.Count);
            Debug.Log(CharSheet.player.crimeList[0].footprintsOfThisCrime[0]);

            Debug.Log(availableFootprints.Count);
            if (availableFootprints.Count > 0) {
                Footprint choisenFootprint = availableFootprints[Random.Range(0, availableFootprints.Count)];
                //надо както придумать выбор скиллов в зависимости от типа футпринта. Пока пусть будет Cha+intimidation
                maintext.text += "was " + choisenFootprint.difficulty + "\n";
                choisenFootprint.difficulty += RollManager.Roll(CharSheet.player.Cha + CharSheet.player.intimidation);
                maintext.text += "now " + choisenFootprint.difficulty + "\n";
            }
            else
            {
                maintext.text += Loc.Get("evidenceReserchFail");
            }

        }
        else
        {
            maintext.text += "Placeholder\n";
        }
        EndPhase();
    }
}
