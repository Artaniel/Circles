using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour, IButtonable
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

    public void Init(Footprint _footprint, Character _holder)
    {
        footPrint = _footprint;
        holder = _holder;
        transform.SetParent(holder.transform);
        holder.evidenceList.Add(this);
        crime = footPrint.crime;
    }

    public bool IsValidForBlackmail() {
        return (firmnessOfProof == 100 && !crime.published); // change it later mb, need some GD ideas
    }

    public string GetButtonText() {
        return $"{crime.guilty.charName} {crime.decription} firmness {firmnessOfProof}";
    }
}
