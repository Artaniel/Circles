using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Convert {
    public GameObject button;
    public delegate void returnLink();
}


public class DroplistManager : MonoBehaviour
{
    public GameObject droplistPanel;
    public GameObject[] buttons;
    private Convert[] buttonRecords;
    public Character[] buttonSortedCharacters;
    public Evidence[] buttonSortedEvidences;
    public enum DroplistType { anotherCharacters, playerCrimes, playerEvidences }
    public DroplistType droplistType;
    public enum ReturnDirrection { reserchPhase, negotiationImproveRelations, negotiationThreat, negotiationsBlackmailStart, publishEvidence }
    public ReturnDirrection returnDirrection;

    public GameObject buttonPrefab;

    private static GameObject phaseController;
    public static DroplistManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        phaseController = GameObject.FindWithTag("PhaseController");
        if (!phaseController) Debug.LogWarning("Cant find phase controller");
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
