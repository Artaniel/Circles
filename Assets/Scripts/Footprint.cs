using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public Crime crime;
    public int difficulty;
    public enum FootprintType {witness, camera, weapon, blood, body, unknown}
    public FootprintType type;
    public List<Evidence> evidences;

    public static List<Footprint> footprintsList = new List<Footprint>();

}
