using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    public Crime crime;
    public Footprint footPrint;
    public CharList holder;
    public bool isTypeKnown;
    public bool isCriminalKnown;
    public float firmnessOfProof; //0..100
}
