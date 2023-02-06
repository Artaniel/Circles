using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharList : MonoBehaviour
{
    public string charName;
    public string race;
    public int Str;
    public int Dex;
    public int Sta;
    public int Cha;
    public int Man;
    public int Com;
    public int Int;
    public int Per;
    public int Res;

    public int athletics;
    public int stealth;
    public int brawl;
    public int firearms;
    public int intimidation;
    public int leadership;
    public int persuasion;
    public int streetwise;
    public int subterfuge;
    public int academics;
    public int awareness;
    public int investigation;
    public int technology;
    public int survival;
    //public Dictionary<string, int> additions;

    public List<Crime> crimeList;

    public static CharList playerCharList = null;

    private void Awake()
    {
        Init();
    }

    private void Init() {

        if (!playerCharList)
            if (name == "PlayerCharList")
                playerCharList = this;
    }
}
