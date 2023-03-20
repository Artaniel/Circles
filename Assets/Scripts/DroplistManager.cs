using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroplistManager : MonoBehaviour
{
    public GameObject droplistPanel;
    public GameObject[] buttons;
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

    public void Init(DroplistType _type, ReturnDirrection _returnDirection)
    {
        OpenDroplist();
        ClearList();
        droplistType = _type;
        returnDirrection = _returnDirection;
        if (droplistType == DroplistType.anotherCharacters)
        {
            List<Character> anotherChars = Character.allCharLists;
            anotherChars.Remove(Character.player);
            buttons = new GameObject[anotherChars.Count];
            buttonSortedCharacters = new Character[anotherChars.Count];
            int i = 0;
            foreach (Character character in anotherChars)
            {
                buttonSortedCharacters[i] = character;
                buttons[i] = Instantiate(buttonPrefab);
                buttons[i].transform.SetParent(transform);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = character.charName;
                buttons[i].SetActive(true);
                i++;
            }
        }
        else if (droplistType == DroplistType.playerEvidences) {
            List<Evidence> viableEvidences = Character.player.ViableEvidences();
            buttons = new GameObject[viableEvidences.Count];
            buttonSortedEvidences = new Evidence[viableEvidences.Count];
            int i = 0;
            foreach (Evidence evidence in viableEvidences)
            {
                buttonSortedEvidences[i] = evidence;
                buttons[i] = Instantiate(buttonPrefab);
                buttons[i].transform.SetParent(transform);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = 
                    $"{evidence.crime.guilty.charName} {evidence.crime.decription} firmness {evidence.firmnessOfProof}";
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
        if (instance.droplistType == DroplistType.anotherCharacters)
        {
            if (instance.returnDirrection == ReturnDirrection.reserchPhase)
                phaseController.GetComponent<ResearchPhase>().CharacterChoisen(instance.buttonSortedCharacters[i]);
            else if (instance.returnDirrection == ReturnDirrection.negotiationImproveRelations)
                phaseController.GetComponent<NegotiationsPhase>().ImproveRelations(instance.buttonSortedCharacters[i]);
            else if (instance.returnDirrection == ReturnDirrection.negotiationThreat)
                phaseController.GetComponent<NegotiationsPhase>().IncreaseThreat(instance.buttonSortedCharacters[i]);
        }
        else if (instance.droplistType == DroplistType.playerCrimes)
        {
            //todo
        }
        else if (instance.droplistType == DroplistType.playerEvidences)
        {
            if (instance.returnDirrection == ReturnDirrection.negotiationsBlackmailStart)
                phaseController.GetComponent<NegotiationsPhase>().BlackmailStart(instance.buttonSortedEvidences[i]);
            else if (instance.returnDirrection == ReturnDirrection.publishEvidence)
                phaseController.GetComponent<NegotiationsPhase>().PublishEvidence(instance.buttonSortedEvidences[i]);
        }
    }
}
