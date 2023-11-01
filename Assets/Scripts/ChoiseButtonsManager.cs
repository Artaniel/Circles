using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ChoiseButtonsManager : MonoBehaviour
{
    public GameObject[] buttons;
    public TextMeshProUGUI[] buttonTexts;
    private int activeButtons = 0;
    public int currentButtonPressedId = 0;

    public delegate void Callback();

    private void Awake()
    {
        Wipe();
    }

    public void Wipe()
    {
        foreach (TextMeshProUGUI buttonText in buttonTexts)
            buttonText.text = "";
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
            button.GetComponent<Button>().interactable = true;
        }
        activeButtons = 0;
        currentButtonPressedId = 0;
    }

    public int AddReplic(string text, bool isAvaible, Callback callback) {
        if (isAvaible)
            return AddReplic(text, callback);
        else
        {
            int buttonID = AddReplic(text, callback);
            buttons[buttonID].GetComponent<Button>().interactable = false;
            return buttonID;
        }
    }

    public int AddReplic(string text, Callback callback) { //returns button ID
        buttons[activeButtons].SetActive(true);
        buttonTexts[activeButtons].text = text;
        buttons[activeButtons].GetComponent<Button>().onClick.RemoveAllListeners();
        buttons[activeButtons].GetComponent<Button>().onClick.AddListener(delegate {callback(); });
        activeButtons++;
        return activeButtons-1;
    }
}
