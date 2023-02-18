using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventPhase : Phase
{
    public GameObject evidencePrefab;

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = "";
        buttonManager.Wipe();

        // id list
        // 0 - add random relation
        // 1 - substract random relation 
        // 2 - random evidence

        int id = Random.Range(0, 3);
        if (id == 0) ChangeRandomRelation(10);
        else if (id == 1) ChangeRandomRelation(-10);
        else if (id == 2) GetRandomEvidence();
        EndPhase();
    }

    private void ChangeRandomRelation(float ammount) {
        List<CharSheet> charSheets = CharSheet.allCharLists;
        charSheets.Remove(CharSheet.player);
        CharSheet target = charSheets[Random.Range(0, charSheets.Count)];
        CharSheet.player.ChangeRelationsToMe(target, ammount);
        maintext.text += $"Changed relations with {target.name} for {ammount}\n";
    }

    private void GetRandomEvidence() {
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

            newEvidence.firmnessOfProof = 10f;

            maintext.text += Loc.Get("evidenceFound");
            maintext.text += newEvidence.crime.guilty.charName + "\n";
            maintext.text += "firmness=" + newEvidence.firmnessOfProof + "\n";
        }
        else
        {
            maintext.text += Loc.Get("noEvidence");
        }
    }

    override protected void InputParcer() { 

    }
}
