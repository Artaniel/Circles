using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUI : MonoBehaviour
{
    public static CharSheetUI instance;

    public TextMeshProUGUI charName;
    public TextMeshProUGUI charRace;
    public TextMeshProUGUI charClan;

    public TextMeshProUGUI Str;
    public TextMeshProUGUI Dex;
    public TextMeshProUGUI Sta;
    public TextMeshProUGUI Cha;
    public TextMeshProUGUI Man;
    public TextMeshProUGUI Com;
    public TextMeshProUGUI Int;
    public TextMeshProUGUI Per;
    public TextMeshProUGUI Res;

    public TextMeshProUGUI athletics;
    public TextMeshProUGUI stealth;
    public TextMeshProUGUI brawl;
    public TextMeshProUGUI firearms;
    public TextMeshProUGUI intimidation;
    public TextMeshProUGUI leadership;
    public TextMeshProUGUI persuasion;
    public TextMeshProUGUI streetwise;
    public TextMeshProUGUI subterfuge;
    public TextMeshProUGUI academics;
    public TextMeshProUGUI awareness;
    public TextMeshProUGUI investigation;
    public TextMeshProUGUI technology;
    public TextMeshProUGUI survival;

    public GameObject relationsPanel;
    public GameObject relationsRecordPrefab;
    public List<GameObject> relationsUIList;

    public GameObject evidencesPanel;
    public GameObject evidenceRecordPrefab;
    public List<GameObject> evidencesUIList;

    public GameObject crimesPanel;
    public GameObject crimeRecordPrefab;
    public List<GameObject> crimeUIList;

    public GameObject blackmailsPanel;
    public GameObject blackmailPrefab;
    public List<GameObject> blackmailUIList;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {   //test only
        //RenderCharSheet(Character.player);
    }

    public void RenderCharSheet(Character character)
    {
        charName.text = $"{Loc.Get("Name:")} {character.charName}";
        charRace.text = $"{Loc.Get("Race:")} {character.race}";
        charClan.text = $"{Loc.Get("Clan:")} {character.clan}";

        Str.text = $"{Loc.Get("Strength")}    {character.Str}";
        Dex.text = $"{Loc.Get("Dexterity")}    {character.Dex}";
        Sta.text = $"{Loc.Get("Stamina")}     {character.Sta}";
        Cha.text = $"{Loc.Get("Charisma")}      {character.Cha}";
        Man.text = $"{Loc.Get("Manipulation")} {character.Man}";
        Com.text = $"{Loc.Get("Composure")}  {character.Com}";
        Int.text = $"{Loc.Get("Intelligence")} {character.Int}";
        Per.text = $"{Loc.Get("Perception")}  {character.Per}";
        Res.text = $"{Loc.Get("Resolve")}      {character.Res}";

        athletics.text =    $"{Loc.Get("athletics")} {character.athletics}";
        stealth.text =      $"{Loc.Get("stealth")} {character.stealth}";
        brawl.text =        $"{Loc.Get("brawl")} {character.brawl}";
        firearms.text =     $"{Loc.Get("firearms")} {character.firearms}";
        intimidation.text = $"{Loc.Get("intimidation")} {character.intimidation}";
        leadership.text =   $"{Loc.Get("leadership")} {character.leadership}";
        persuasion.text =   $"{Loc.Get("persuasion")} {character.persuasion}";
        streetwise.text =   $"{Loc.Get("streetwise")} {character.streetwise}";
        subterfuge.text =   $"{Loc.Get("subterfuge")} {character.subterfuge}";
        academics.text =    $"{Loc.Get("academics")} {character.academics}";
        awareness.text =    $"{Loc.Get("awareness")} {character.awareness}";
        investigation.text = $"{Loc.Get("investigation")} {character.investigation}";
        technology.text =   $"{Loc.Get("technology")} {character.technology}";
        survival.text =     $"{Loc.Get("survival")} {character.survival}";
        RelationsRender(character);
        EvidencesRender(character);
        CrimesRender(character);
        BlackmailsRender(character);
    }

    private void RelationsRender(Character thisCharacter) {
        List<Character> anotherChars = Character.allCharacters;
        anotherChars.Remove(thisCharacter);
        ClearList(relationsUIList);
        GameObject relationsRecord;
        foreach (Character anoterChar in anotherChars)
        {            
            relationsRecord = Instantiate(relationsRecordPrefab);
            relationsRecord.transform.SetParent(relationsPanel.transform);
            relationsRecord.GetComponent<TextMeshProUGUI>().text = $"{anoterChar.charName} {thisCharacter.relations[anoterChar]} {thisCharacter.threat[anoterChar]}";
            relationsUIList.Add(relationsRecord);
        }
    }

    private void EvidencesRender(Character character)
    {
        ClearList(evidencesUIList);
        GameObject evidenceRecord;
        foreach (Evidence evidence in character.evidenceList)
        {
            evidenceRecord = Instantiate(evidenceRecordPrefab);
            evidenceRecord.transform.SetParent(evidencesPanel.transform);
            evidenceRecord.GetComponent<TextMeshProUGUI>().text = $"{evidence.crime.guilty.charName} {evidence.firmnessOfProof}"; // need to find something to define it
            evidencesUIList.Add(evidenceRecord);
        }
    }

    private void CrimesRender(Character character)
    {
        ClearList(crimeUIList);
        GameObject crimeRecord;
        foreach (Crime crime in character.crimeList)
        {
            crimeRecord = Instantiate(crimeRecordPrefab);
            crimeRecord.transform.SetParent(crimesPanel.transform);
            crimeRecord.GetComponent<TextMeshProUGUI>().text = $"{crime.decription}";
            crimeUIList.Add(crimeRecord);
        }
    }

    private void BlackmailsRender(Character character)
    {
        ClearList(blackmailUIList);
        GameObject blackmailRecord;
        Debug.Log(1);
        foreach (Blackmail blackmail in character.blackmailList)
        {
            Debug.Log(blackmail.crime.guilty);
            blackmailRecord = Instantiate(blackmailPrefab);
            blackmailRecord.transform.SetParent(blackmailsPanel.transform);
            blackmailRecord.GetComponent<TextMeshProUGUI>().text = $"blackmail {blackmail.crime.guilty}";
            blackmailUIList.Add(blackmailRecord);
        }
    }

    private void ClearList(List<GameObject> list) {
        while (list.Count > 0)
        {        
            Destroy(list[0]);
            list.Remove(list[0]);
        }
    }

    public void CloseButton() {
        gameObject.SetActive(false);
    }

    public void Init() {
        RenderCharSheet(Character.player);
    }

}
