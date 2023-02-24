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


    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);        
    }

    private void Start()
    {   //test only
        RenderCharSheet(Character.player);
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

        athletics.text = $"{Loc.Get("athletics")} {character.athletics}";
        stealth.text = $"{Loc.Get("stealth")} {character.stealth}";
        brawl.text = $"{Loc.Get("brawl")} {character.brawl}";
        firearms.text = $"{Loc.Get("firearms")} {character.firearms}";
        intimidation.text = $"{Loc.Get("intimidation")} {character.intimidation}";
        leadership.text = $"{Loc.Get("leadership")} {character.leadership}";
        persuasion.text = $"{Loc.Get("persuasion")} {character.persuasion}";
        streetwise.text = $"{Loc.Get("streetwise")} {character.streetwise}";
        subterfuge.text = $"{Loc.Get("subterfuge")} {character.subterfuge}";
        academics.text = $"{Loc.Get("academics")} {character.academics}";
        awareness.text = $"{Loc.Get("awareness")} {character.awareness}";
        investigation.text = $"{Loc.Get("investigation")} {character.investigation}";
        technology.text = $"{Loc.Get("technology")} {character.technology}";
        survival.text = $"{Loc.Get("survival")} {character.survival}";
        RelationsRender(character);
    }

    private void RelationsRender(Character thisCharacter) {
        List<Character> anotherChars = Character.allCharLists;
        anotherChars.Remove(thisCharacter);

        GameObject relationsRecord;
        foreach (Character anoterChar in anotherChars)
        {
            relationsRecord = Instantiate(relationsRecordPrefab);
            relationsRecord.transform.SetParent(relationsPanel.transform);
            relationsRecord.GetComponent<TextMeshProUGUI>().text = $"{anoterChar.charName} {thisCharacter.relations[anoterChar]} {thisCharacter.threat[anoterChar]}";
        }
    }

}
