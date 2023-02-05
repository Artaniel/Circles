using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    public Crime crime;
    public int difficulty;
    public enum FootprintType {witness, camera, weapon, blood, body}
    public FootprintType type;
}
