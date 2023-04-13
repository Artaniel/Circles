using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventPhase : Phase
{
    public GameObject evidencePrefab;
    public float blackmainPressureMultiplier = 0.1f;

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

        foreach (Blackmail blackmail in Character.player.blackmailList)
        {
            Character.player.pressure[blackmail.crime.guilty] += (100 - Character.player.pressure[blackmail.crime.guilty]) * blackmainPressureMultiplier; 
        }

        EndPhase();
    }

    private void ChangeRandomRelation(float ammount) {
        List<Character> charSheets = Character.allCharacters;
        charSheets.Remove(Character.player);
        Character target = charSheets[Random.Range(0, charSheets.Count)];
        Character.player.ChangeRelationsToMe(target, ammount);
        maintext.text += $"Changed relations with {target.name} for {ammount}\n";
    }

    private void GetRandomEvidence() {
        List<Footprint> availableFootprints = Character.player.GetAvailableForPlayerFootprints();
        if (availableFootprints.Count > 0)
        {
            Footprint choisenFootPrint = availableFootprints[Random.Range(0, availableFootprints.Count)]; // get random from list, needed to be coisen by player later
            Evidence newEvidence = Instantiate(evidencePrefab).GetComponent<Evidence>();
            newEvidence.footPrint = choisenFootPrint;
            newEvidence.transform.parent = Character.player.transform;
            Character.player.evidenceList.Add(newEvidence);
            newEvidence.holder = Character.player;
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

    override protected void InputParcer() {}
    override public void ButtonPressed(IButtonable item) {}
}
