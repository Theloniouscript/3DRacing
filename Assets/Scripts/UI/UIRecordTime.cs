using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    /*[SerializeField] private GameObject finalRecordObject;*/

    [SerializeField] private Text goldRecordTime;
    [SerializeField] private Text playerRecordTime;

   /* [SerializeField] private Text recordTimeText;
    [SerializeField] private Text currentTimeText;*/

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordObject.SetActive(false); 
        playerRecordObject.SetActive(false);
        /*finalRecordObject.SetActive(false);*/
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceStarted()
    {
        /*finalRecordObject.SetActive(false);*/

        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordObject.SetActive(true);

            goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if (raceResultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);

            playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            
        }
    }

    private void OnRaceCompleted()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);

        /*finalRecordObject.SetActive(true);*/

        /*recordTimeText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        currentTimeText.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);*/
    }
}
