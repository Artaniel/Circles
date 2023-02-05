using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    public static int Roll(int dicepool, int difficulty = 6) {
        string s = "";
        int oneRoll;
        int succeses = 0;
        for (int i = 0; i < dicepool; i++)
        {
            oneRoll = Random.Range(1, 10);
            s += oneRoll.ToString() + " ";
            if (oneRoll >= difficulty)
                succeses++;
            else if (oneRoll == 1)
                succeses--;
        }
        Debug.Log(s);

        return succeses;
    }
}
