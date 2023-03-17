using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;
    public string race;
    public string clan; // replace with Enum
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
    public List<Evidence> evidenceList;
    public Dictionary<Character, float> relations;
    public Dictionary<Character, float> threat;
    public Dictionary<Character, float> pressure;

    public static Character player = null;
    public bool isPlayer = false; 
    public static List<Character> allCharLists = new List<Character>();

    private void Awake()
    {
        Init();
        allCharLists.Add(this);
    }

    private void Start()
    {
        relations = new Dictionary<Character, float>();
        foreach (Character charSheet in allCharLists)
        {
            if (charSheet != this)
            {
                relations.Add(charSheet, 0);
            }
        }
        threat = new Dictionary<Character, float>();
        foreach (Character charSheet in allCharLists)
        {
            if (charSheet != this)
            {
                threat.Add(charSheet, 0);
            }
        }
    }

    private void Init() {
        if (isPlayer)
            player = this;
    }

    public List<Footprint> GetAvailableForPlayerFootprints() {
        List<Footprint> result = new List<Footprint>();
        List<Footprint> knownFootprints = new List<Footprint>();
        foreach (Evidence evidence in evidenceList) {
            knownFootprints.Add(evidence.footPrint);
        }
        Debug.Log(knownFootprints.Count);
        Debug.Log(Footprint.footprintsList.Count);

        foreach (Footprint footprint in Footprint.footprintsList) {
            if (!knownFootprints.Contains(footprint) && footprint.crime.guilty != player)
                result.Add(footprint);
        }
        Debug.Log(result.Count);

        return result;   
    }

    public void ChangeRelationsToMe(Character subject, float ammount) {
        subject.relations[this] += ammount;
        Mathf.Clamp(subject.relations[this], -100f, 100f);
    }

    public void ChangeThreatToMe(Character subject, float ammount)
    {
        subject.threat[this] += ammount;
        Mathf.Clamp(subject.threat[this], -100f, 100f);
    }

    public float GetPresureBalance(Character target) {
        return pressure[target] - target.pressure[this];
    }

    public List<Evidence> ViableEvidences() {
        List<Evidence> result = new List<Evidence>();
        foreach (Evidence evidence in evidenceList)
            if (evidence.IsValidForBlackmail())
                result.Add(evidence);
        return result;
    }

}
