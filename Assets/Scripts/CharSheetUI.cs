using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUI : MonoBehaviour
{
    public static CharSheetUI instance;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Race;
    public TextMeshProUGUI Clan;

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

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }



}
