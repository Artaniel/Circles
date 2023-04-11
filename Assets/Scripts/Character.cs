using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Character : MonoBehaviour, IButtonable
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
    public List<Blackmail> blackmailList;

    public static Character player = null;
    public bool isPlayer = false; 
    public static List<Character> allCharacters = new List<Character>();

    private void Awake()
    {
        if (isPlayer)
            player = this;
        allCharacters.Add(this);
    }

    private void Start()
    {
        relations = new Dictionary<Character, float>();
        foreach (Character charSheet in allCharacters)
        {
            if (charSheet != this)
            {
                relations.Add(charSheet, 0);
            }
        }
        threat = new Dictionary<Character, float>();
        foreach (Character charSheet in allCharacters)
        {
            if (charSheet != this)
            {
                threat.Add(charSheet, 0);
            }
        }
    }

    public List<Footprint> GetAvailableForPlayerFootprints() {
        List<Footprint> result = new List<Footprint>();
        List<Footprint> knownFootprints = new List<Footprint>();
        foreach (Evidence evidence in evidenceList) {
            knownFootprints.Add(evidence.footPrint);
        }

        foreach (Footprint footprint in Footprint.footprintsList) {
            if (!knownFootprints.Contains(footprint) && footprint.crime.guilty != player)
                result.Add(footprint);
        }

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

    public List<Evidence> ViableEvidences(bool isFullFirmness) {
        List<Evidence> result = new List<Evidence>();
        foreach (Evidence evidence in evidenceList)
            if (evidence.IsValidForBlackmail() ^ !isFullFirmness)
                result.Add(evidence);
        return result;
    }

    public List<Evidence> BlackmailNonViableEvidences()
    {
        List<Evidence> result = new List<Evidence>();
        foreach (Evidence evidence in evidenceList)
            if (!evidence.IsValidForBlackmail())
                result.Add(evidence);
        return result;
    }

    public string GetButtonText() {
        return charName;
    }

    public List<IButtonable> GetCaractersExceptMe() {
        List<IButtonable> result = allCharacters.Cast<IButtonable>().ToList();
        result.Remove(this);
        return result;
    }

    public List<IButtonable> GetEvidencesByFirmness(bool addFullFirmnes, bool addNonFullFirmness)
    {
        List<IButtonable> result = new List<IButtonable>();
        foreach (Evidence evidence in evidenceList) {
            if (addFullFirmnes && evidence.firmnessOfProof == 100)
                result.Add(evidence);
            if (addNonFullFirmness && evidence.firmnessOfProof < 100)
                result.Add(evidence);
        }
        return result; 
    }

    public List<IButtonable> GetEvidencesByPublished(bool addPublished, bool addNotPublished) {
        List<IButtonable> result = new List<IButtonable>();
        foreach (Evidence evidence in evidenceList)
        {
            if (evidence.firmnessOfProof == 100)
            {
                if (addPublished && evidence.crime.published)
                    result.Add(evidence);
                if (addNotPublished && !evidence.crime.published)
                    result.Add(evidence);
            }
        }
        return result;
    }

    public List<IButtonable> GetCrimes(bool onlyNotCleared) {
        List<IButtonable> result = new List<IButtonable>();
        foreach (Crime crime in crimeList)
        {
            if (!(crime.footprintsOfThisCrime[0].difficulty >= 10 && onlyNotCleared)) // пересмотреть если футпринтов всеже будет много
                result.Add(crime);
        }
        return result;
    }

}
