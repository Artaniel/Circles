using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAI : MonoBehaviour
{
    public CharSheet charSheet;
    public GameObject crimePrefab;    

    private void Awake()
    {
        if (!charSheet) charSheet = GetComponent<CharSheet>();
        if (!charSheet) Debug.LogWarning($"No charlist on {gameObject.name}");
    }

    public void CommitCrime() {
        Crime crime = Instantiate(crimePrefab).GetComponent<Crime>();
        crime.gameObject.transform.parent = transform;
        charSheet.crimeList.Add(crime);
        crime.Init("prototype", charSheet); // переделать потомб после индексации footprint-ов
    }
}
