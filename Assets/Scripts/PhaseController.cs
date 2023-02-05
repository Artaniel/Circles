using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public static PhaseController instance;
    public enum Phase {feed, reserch, randomEvent, politics};
    public Phase phase = Phase.feed;
    public GameObject feedPhasePanel;
    public GameObject researchPhasePanel;
    public GameObject randomEventPhasePanel;
    public GameObject politicsPhasePanel;

    private void Awake()
    {
        if (!instance) 
            instance = this;
        else 
            Debug.LogError("Phasecontroller inst duplicate");
    }

    private void Start()
    {
        phase = Phase.politics;
        NextPhase();
    }

    public void NextPhase() {
        if (phase == Phase.feed)
        {
            phase = Phase.reserch;
            feedPhasePanel.SetActive(false);
            researchPhasePanel.SetActive(true);
        }
        if (phase == Phase.reserch)
        {
            phase = Phase.randomEvent;
            researchPhasePanel.SetActive(false);
            randomEventPhasePanel.SetActive(true);
        }
        if (phase == Phase.randomEvent)
        {
            phase = Phase.politics;
            randomEventPhasePanel.SetActive(false);
            politicsPhasePanel.SetActive(true);
        }
        if (phase == Phase.politics)
        {
            phase = Phase.feed;
            politicsPhasePanel.SetActive(false);
            feedPhasePanel.SetActive(true);
            feedPhasePanel.GetComponent<FeedPhase>().PhaseRun();
        }
    }
}
