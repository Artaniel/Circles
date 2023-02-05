using System.Collections;
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
        buttonManager.AddReplic(huntTypesNames[huntType.assault]);// id = 0;
        buttonManager.AddReplic(huntTypesNames[huntType.seduce]);// 1
        buttonManager.AddReplic(huntTypesNames[huntType.animals]);// 2
        buttonManager.AddReplic(huntTypesNames[huntType.sleep]);// 3
        isInEndPhase = false;
    }

    override protected void InputParcer()
    {
        int id = buttonManager.currentButtonPressedId;
        if (id == 0)
        { //huntType.assault
            if (RollManager.Roll(CharList.playerCharList.Dex + CharList.playerCharList.stealth, 6) < 1)
            {
                maintext.text += Loc.Get("huntAssaultFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы попали на камеру");
            }
            else if (RollManager.Roll(CharList.playerCharList.Str + CharList.playerCharList.brawl, 6) < 1)
            {
                maintext.text += Loc.Get("huntAssaultFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля");
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 1)
        { //huntType.seduce
            if (RollManager.Roll(CharList.playerCharList.Cha + CharList.playerCharList.persuasion, 6) < 1)
            {
                maintext.text += Loc.Get("huntSeduceFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили труп");
            }
            else if (RollManager.Roll(CharList.playerCharList.Man + CharList.playerCharList.subterfuge, 6) < 1)
            {
                maintext.text += Loc.Get("huntSeduceFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили труп");
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 2)
        { //huntType.animals
            if (RollManager.Roll(CharList.playerCharList.Per + CharList.playerCharList.awareness, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntAnimalsFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.");// уточнить ихсодя из нарратива
            }
            else if (RollManager.Roll(CharList.playerCharList.Res + CharList.playerCharList.survival, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntAnimalsFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.");// уточнить ихсодя из нарратива
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 3)
        { //huntType.sleep
            if (RollManager.Roll(CharList.playerCharList.Res + CharList.playerCharList.streetwise, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntSleepFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.");// уточнить ихсодя из нарратива
            }
            else if (RollManager.Roll(CharList.playerCharList.Man + CharList.playerCharList.streetwise, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntSleepFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.");// уточнить ихсодя из нарратива
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        EndPhase();
    }

}
