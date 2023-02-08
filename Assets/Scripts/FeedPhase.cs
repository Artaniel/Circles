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
            if (RollManager.Roll(CharSheet.playerCharSheet.Dex + CharSheet.playerCharSheet.stealth, 6) < 1)
            {
                maintext.text += Loc.Get("huntAssaultFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы попали на камеру", CharSheet.playerCharSheet);
            }
            else if (RollManager.Roll(CharSheet.playerCharSheet.Str + CharSheet.playerCharSheet.brawl, 6) < 1)
            {
                maintext.text += Loc.Get("huntAssaultFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля", CharSheet.playerCharSheet);
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 1)
        { //huntType.seduce
            if (RollManager.Roll(CharSheet.playerCharSheet.Cha + CharSheet.playerCharSheet.persuasion, 6) < 1)
            {
                maintext.text += Loc.Get("huntSeduceFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили труп", CharSheet.playerCharSheet);
            }
            else if (RollManager.Roll(CharSheet.playerCharSheet.Man + CharSheet.playerCharSheet.subterfuge, 6) < 1)
            {
                maintext.text += Loc.Get("huntSeduceFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили труп", CharSheet.playerCharSheet);
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 2)
        { //huntType.animals
            if (RollManager.Roll(CharSheet.playerCharSheet.Per + CharSheet.playerCharSheet.awareness, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntAnimalsFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.", CharSheet.playerCharSheet);// уточнить ихсодя из нарратива
            }
            else if (RollManager.Roll(CharSheet.playerCharSheet.Res + CharSheet.playerCharSheet.survival, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntAnimalsFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.", CharSheet.playerCharSheet);// уточнить ихсодя из нарратива
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        else if (id == 3)
        { //huntType.sleep
            if (RollManager.Roll(CharSheet.playerCharSheet.Res + CharSheet.playerCharSheet.streetwise, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntSleepFail1");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.", CharSheet.playerCharSheet);// уточнить ихсодя из нарратива
            }
            else if (RollManager.Roll(CharSheet.playerCharSheet.Man + CharSheet.playerCharSheet.streetwise, 6) < 1)// уточнить ихсодя из нарратива
            {
                maintext.text += Loc.Get("huntSleepFail2");
                Crime crime = GameObject.Instantiate(crimePrefab).GetComponent<Crime>();
                crime.Init("Во время неудачной охоты вы оставили свидетеля.", CharSheet.playerCharSheet);// уточнить ихсодя из нарратива
            }
            else
                maintext.text += Loc.Get("huntSuccess");
        }
        EndPhase();
    }

}
