using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackmail : MonoBehaviour
{
    public CharList ovner;
    public CharList victim;
    public Crime crime;
    public Evidence evidence;

    public float GetPressire()
    {
        return 10; // подумать тут какую то формулу. Возможно исходя из пааметров крайма и эвиденса
    }

}
