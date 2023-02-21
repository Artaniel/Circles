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

    override protected void InputParcer()
    {

    }

    private void CharacterInit() {
        foreach (Character charList in Character.allCharLists)
            if (!charList.isPlayer)
            {
                charList.GetComponent<CharAI>().CommitCrime();
                charList.GetComponent<CharAI>().CommitCrime();
                charList.GetComponent<CharAI>().CommitCrime();
            }

    }
}
