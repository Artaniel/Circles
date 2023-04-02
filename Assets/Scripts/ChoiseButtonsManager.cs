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
    public UnityEvent OnButtonPressed;
    
    private void Awake()
    {
        if (OnButtonPressed == null) OnButtonPressed = new UnityEvent();
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

    public int AddReplic(string text, bool isAvaible) {
        if (isAvaible)
            return AddReplic(text);
        else
        {
            int buttonID = AddReplic(text);
            buttons[buttonID].GetComponent<Button>().interactable = false;
            return buttonID;
        }
    }

    public int AddReplic(string text) { //returns button ID
        buttons[activeButtons].SetActive(true);
        buttonTexts[activeButtons].text = text;
        activeButtons++;
        return activeButtons-1;
    }

    public void OnButtonPress(GameObject button)
    {
        for (int i = 0; i < activeButtons; i++)
            if (button == buttons[i])
            {
                currentButtonPressedId = i;
                OnButtonPressed.Invoke();
            }
    }
}
