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
    public enum ReturnDirrection { reserchPhase, negotiationImproveRelations, negotiationThreat }
    public ReturnDirrection returnDirrection;

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

    public void ClearList() {
        foreach (GameObject button in buttons) 
            Destroy(button);
    }

    public void Init(DroplistType _type, ReturnDirrection _returnDirection)
    {
        OpenDroplist();
        ClearList();
        droplistType = _type;
        returnDirrection = _returnDirection;
        if (_type == DroplistType.anotherCharacters) {
            List<Character> anotherChars = Character.allCharLists;
            anotherChars.Remove(Character.player);
            buttons = new GameObject[anotherChars.Count];
            buttonSortedCharacters = new Character[anotherChars.Count];
            int i = 0;
            foreach (Character character in anotherChars) {
                buttonSortedCharacters[i] = character;
                buttons[i] = Instantiate(buttonPrefab);
                buttons[i].transform.SetParent(transform);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = character.charName;
                buttons[i].SetActive(true);
                i++;
            }
        }
    }

    public static void OnButtonPress(GameObject choisenButton)
    {
        int i = 0;
        while (i < instance.buttons.Length && instance.buttons[i] != choisenButton)
        {
            i++;
        }
        instance.CloseDroplist();
        if (instance.droplistType == DroplistType.anotherCharacters && instance.returnDirrection == ReturnDirrection.reserchPhase)
        {
            GameObject.FindWithTag("PhaseController").GetComponent<ResearchPhase>().CharacterChoisen(instance.buttonSortedCharacters[i]);
        }
        else if (instance.droplistType == DroplistType.anotherCharacters && instance.returnDirrection == ReturnDirrection.negotiationImproveRelations)
        {
            GameObject.FindWithTag("PhaseController").GetComponent<NegotiationsPhase>().ImproveRelations(instance.buttonSortedCharacters[i]);
        }
        else if (instance.droplistType == DroplistType.anotherCharacters && instance.returnDirrection == ReturnDirrection.negotiationThreat)
        {
            GameObject.FindWithTag("PhaseController").GetComponent<NegotiationsPhase>().IncreaseThreat(instance.buttonSortedCharacters[i]);
        }
    }
}
