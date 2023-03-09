using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroplistManager : MonoBehaviour
{
    public GameObject droplistPanel;
    public GameObject[] buttons;
    public Character[] buttonSortedCharacters;
    public enum DroplistType { anotherCharacters, myCrimes, myEvidences }
    public DroplistType droplistType;
    public enum ReturnDirrectionType { reserchPhase }
    public ReturnDirrectionType returnDirrection;

    public GameObject buttonPrefab;

    public static DroplistManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void CloseDroplist() 
    {
        droplistPanel.SetActive(false);
    }

    public void OpenDroplist()
    {
        droplistPanel.SetActive(true);
    }

    public void Init(DroplistType _type, ReturnDirrectionType _returnDirection)
    {
        OpenDroplist();
        droplistType = _type;
        returnDirrection = _returnDirection;
        if (_type == DroplistType.anotherCharacters) {
            List<Character> anotherChars = Character.allCharLists;
            anotherChars.Remove(Character.player);
            buttons = new GameObject[anotherChars.Count - 1];
            buttonSortedCharacters = new Character[anotherChars.Count - 1];
            int i = 0;
            foreach (Character character in anotherChars) {
                buttonSortedCharacters[i] = character;
                buttons[i] = Instantiate(buttonPrefab);
                buttons[i].transform.parent = transform;
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = character.charName;
            }
        }
    }

    public static void OnButtonPress(GameObject choisenButton)
    {
        int i = 0;
        while (i <= instance.buttons.Length && instance.buttons[i] != choisenButton)
        {
            i++;
        }
        if (instance.droplistType == DroplistType.anotherCharacters && instance.returnDirrection == ReturnDirrectionType.reserchPhase)
        {
            GameObject.FindWithTag("PhaseController").GetComponent<ResearchPhase>().CharacterChoisen(instance.buttonSortedCharacters[i]);
        }
    }
}
