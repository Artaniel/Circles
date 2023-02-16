using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventPhase : Phase
{
    override public void PhaseRun() {
        // id list
        // 0 - add random relation
        // 1 - substract random relation

        int id = Random.Range(0, 1);
        if (id == 0) ChangeRandomRelation(10);
        else if (id == 1) ChangeRandomRelation(-10);

    }

    private void ChangeRandomRelation(float value) { 
    }

    override protected void InputParcer() { 

    }
}
