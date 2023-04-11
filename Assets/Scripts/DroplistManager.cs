using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class DroplistManager : MonoBehaviour
{
    public GameObject droplistPanel;
    public GameObject[] buttons;
    public Character[] buttonSortedCharacters;
    public Evidence[] buttonSortedEvidences;

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

    public void MakeDropDownFormList(List<IButtonable> itemList, Phase sender)
    {
        OpenDroplist();
        ClearList();
        int i = 0;
        buttons = new GameObject[itemList.Count];
        foreach (IButtonable item in itemList) {
            buttons[i] = Instantiate(buttonPrefab);
            buttons[i].transform.SetParent(transform);
            buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[i].GetComponent<Button>().onClick.AddListener(delegate { CloseDroplist(); sender.ButtonPressed(item); }) ;
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = item.GetButtonText();
            buttons[i].SetActive(true);
            i++;
        }
    }
}
