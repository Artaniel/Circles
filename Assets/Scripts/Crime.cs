using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crime : MonoBehaviour
{
    public Character guilty;
    public Character arbitter;
    public List<Evidence> evidences;
    public List<Footprint> footprintsOfThisCrime;
    public string decription;
    public int gravity = 5; //[0..10]

    public Footprint footprintPrefab;

    public void Init(string desc, Character _guilty) {
        decription = desc;
        Footprint footPrint = GameObject.Instantiate(footprintPrefab).GetComponent<Footprint>();
        footPrint.crime = this;
        footPrint.difficulty = 3;
        footPrint.gameObject.transform.parent = transform;
        guilty = _guilty;

        if (desc == "Во время неудачной охоты вы оставили свидетеля") //надо бы подумать над другой системой индексации
        {
            footPrint.type = Footprint.FootprintType.witness;
        }
        else if (desc == "Во время неудачной охоты вы попали на камеру")
        {
            footPrint.type = Footprint.FootprintType.camera;
        }
        else if (desc == "Во время неудачной охоты вы оставили труп")
        {
            footPrint.type = Footprint.FootprintType.body;
        }
        else if (desc == "prototype")
        {
            footPrint.type = Footprint.FootprintType.unknown;
        }
           
        transform.parent = guilty.transform;
        guilty.crimeList.Add(this);
        footprintsOfThisCrime.Add(footPrint);
        Footprint.footprintsList.Add(footPrint);
    }
}
