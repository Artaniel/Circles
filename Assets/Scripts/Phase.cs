using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// todo
//перевести локализацию на struct
//перевести локализацию на подгрузку из файла
//сделать фазу рандомного эвента. Пока один эвент - открытие случайного эвиденса
//сделать фазу переговоров. Пока только кнопка закончить.
//  сделать модель персонажа
//      модель должна хранить эвиденсы
//      функция добавления эвиденса персонажу исходя из футпринта
//      сделать функцию определящую list футпринтов которые можно случайно найти

// done
//tmpro
//сделать единственную кнопку рандомного поиска в фазе исследования
//баги с лишними строками удачной охоты

public abstract class Phase : MonoBehaviour
{
    public TextMeshProUGUI maintext;
    public ChoiseButtonsManager buttonManager;
    public bool isInEndPhase = false;
    public Phase nextPhase;

    virtual public void PhaseRun() {
        isInEndPhase = false;
        buttonManager.OnButtonPressed.AddListener(InputEndPhaseCheck);
    }

    virtual public void EndPhase() {
        Debug.Log("Endphase");
        isInEndPhase = true;
        buttonManager.Wipe();
        buttonManager.AddReplic(Loc.Get("next")); 
    }

    public void InputEndPhaseCheck()
    {
        if (isInEndPhase)
        {
            buttonManager.OnButtonPressed.RemoveAllListeners();
            isInEndPhase = false;
            nextPhase.PhaseRun();
        }
        else InputParcer();
    }

    abstract protected void InputParcer();  //here should be reaction on pressed button
}
