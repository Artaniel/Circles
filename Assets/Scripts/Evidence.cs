using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    public Crime crime;
    public Footprint footPrint;
    public Character holder;
    public bool isTypeKnown;
    public bool isCriminalKnown;
    public float firmnessOfProof = 0; //0..100

    public bool RollForFirmness(Character owner) {
        int sucesses = RollManager.Roll(owner.Per + owner.investigation);
        firmnessOfProof += 10 * sucesses;
        firmnessOfProof = Mathf.Clamp(firmnessOfProof, 0, 100);
        return sucesses > 0;
    }
}
