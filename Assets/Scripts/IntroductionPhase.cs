using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionPhase : Phase
{
    private void Start()
    {
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
}
