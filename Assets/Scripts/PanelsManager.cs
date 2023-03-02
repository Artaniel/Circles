using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public GameObject charSheetPanel;

    public void OpenCharSheet() {
        charSheetPanel.SetActive(true);
        charSheetPanel.GetComponent<CharSheetUI>().Init();
    }
}
