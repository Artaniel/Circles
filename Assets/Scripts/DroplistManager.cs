using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroplistManager : MonoBehaviour
{
    public GameObject droplistPanel;
    public GameObject[] buttons;
    public Character[] buttonSortedCharacters;
    public enum DroplistType {anotherCharacters, myCrimes, myEvidences}
    public GameObject buttonPrefab;

    public void CloseDroplist() 
    {
        droplistPanel.SetActive(false);
    }

    public void OpenDroplist()
    {
        droplistPanel.SetActive(true);
    }

    public void Init(DroplistType type)
    {
        OpenDroplist();
        if (type == DroplistType.anotherCharacters) {
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

    public void OnButtonPress() { 
    
    }

}
