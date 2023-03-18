using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackmail : MonoBehaviour
{
    public Character owner;
    public Character victim;
    public Crime crime;
    public Evidence evidence;

    public float GetPressure()
    {
        return crime.gravity;
    }

    public void Init( Evidence _evidence)
    {
        evidence = _evidence;
        owner = evidence.holder;
        crime = evidence.crime;
        victim = crime.guilty;
        transform.SetParent(owner.transform);
    }

}
