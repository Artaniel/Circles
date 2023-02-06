using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAI : MonoBehaviour
{
    public CharList charList;
    public GameObject crimePrefab;

    private void Awake()
    {
        if (!charList) charList = GetComponent<CharList>();
        if (!charList) Debug.LogWarning($"No charlist on {gameObject.name}");
    }

    public void CommitCrime() {
        Crime crime = Instantiate(crimePrefab).GetComponent<Crime>();
        crime.gameObject.transform.parent = transform;
        charList.crimeList.Add(crime);
        crime.Init("prototype"); // переделать потомб после индексации footprint-ов
    }
}
