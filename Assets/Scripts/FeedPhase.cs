﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedPhase : Phase
{    
    private enum huntType { assault, seduce, animals, sleep}
    private Dictionary<huntType, string> huntTypesNames;
    //public ChoiseButtonsManager buttonManager;
    //bool nextPhase = false;

    public GameObject crimePrefab;

    private void Awake()
    {
        huntTypesNames = new Dictionary<huntType, string>();
        huntTypesNames[huntType.assault] = Loc.Get("feedNameAssault");
        huntTypesNames[huntType.seduce] = Loc.Get("feedNameSeduce");
        huntTypesNames[huntType.animals] = Loc.Get("feedNameAnimals");
        huntTypesNames[huntType.sleep] = Loc.Get("feedNameSleep");
    }

    override public void PhaseRun()
    {
        base.PhaseRun();
        maintext.text = Loc.Get("feedDesc");
        buttonManager.Wipe();
        buttonManager.AddReplic(huntTypesNames[huntType.assault], PlayerChoiseAssault);
        buttonManager.AddReplic(huntTypesNames[huntType.seduce], PlayerChoiseSeduce);
        buttonManager.AddReplic(huntTypesNames[huntType.animals], PlayerChoiseAnimals);
        buttonManager.AddReplic(huntTypesNames[huntType.sleep], PlayerChoiseSleep);
        isInEndPhase = false;
    }

    private void PlayerChoiseAssault() {
        if (RollManager.Roll(Character.player.Dex + Character.player.stealth, 6) < 1)
        {
            maintext.text += Loc.Get("huntAssaultFail1");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы попали на камеру", Character.player);
        }
        else if (RollManager.Roll(Character.player.Str + Character.player.brawl, 6) < 1)
        {
            maintext.text += Loc.Get("huntAssaultFail2");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили свидетеля", Character.player);
        }
        else
            maintext.text += Loc.Get("huntSuccess");
        EndPhase();
    }

    private void PlayerChoiseSeduce() {
        if (RollManager.Roll(Character.player.Cha + Character.player.persuasion, 6) < 1)
        {
            maintext.text += Loc.Get("huntSeduceFail1");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили труп", Character.player);
        }
        else if (RollManager.Roll(Character.player.Man + Character.player.subterfuge, 6) < 1)
        {
            maintext.text += Loc.Get("huntSeduceFail2");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили труп", Character.player);
        }
        else
            maintext.text += Loc.Get("huntSuccess");
        EndPhase();
    }

    private void PlayerChoiseAnimals() {
        if (RollManager.Roll(Character.player.Per + Character.player.awareness, 6) < 1)// уточнить ихсодя из нарратива
        {
            maintext.text += Loc.Get("huntAnimalsFail1");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили свидетеля.", Character.player);// уточнить ихсодя из нарратива
        }
        else if (RollManager.Roll(Character.player.Res + Character.player.survival, 6) < 1)// уточнить ихсодя из нарратива
        {
            maintext.text += Loc.Get("huntAnimalsFail1");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили свидетеля.", Character.player);// уточнить ихсодя из нарратива
        }
        else
            maintext.text += Loc.Get("huntSuccess");
        EndPhase();
    }

    private void PlayerChoiseSleep()
    {
        if (RollManager.Roll(Character.player.Res + Character.player.streetwise, 6) < 1)// уточнить ихсодя из нарратива
        {
            maintext.text += Loc.Get("huntSleepFail1");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили свидетеля.", Character.player);// уточнить ихсодя из нарратива
        }
        else if (RollManager.Roll(Character.player.Man + Character.player.streetwise, 6) < 1)// уточнить ихсодя из нарратива
        {
            maintext.text += Loc.Get("huntSleepFail2");
            Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
            crime.Init("Во время неудачной охоты вы оставили свидетеля.", Character.player);// уточнить ихсодя из нарратива
        }
        else
            maintext.text += Loc.Get("huntSuccess");
        EndPhase();
    }

    override public void ButtonPressed(IButtonable item) { }
}
