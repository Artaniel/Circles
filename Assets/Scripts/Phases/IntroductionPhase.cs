using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionPhase : Phase
{
    private void Start()
    {
        CharacterInit();
        PhaseRun();
    }

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("introduction");
        EndPhase();
    }

    private void CharacterInit() {
        foreach (Character charList in Character.allCharacters)
            if (!charList.isPlayer)
            {
                charList.GetComponent<CharAI>().CommitCrime();
                charList.GetComponent<CharAI>().CommitCrime();
                charList.GetComponent<CharAI>().CommitCrime();
            }

    }

    override public void ButtonPressed(IButtonable item) { }
}
