using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountdownTimer : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Timer countdouwnTimer;
    [SerializeField] private RaceStateTracker raceStateTracker;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true; // для текста
        enabled = true; // для таймера
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = countdouwnTimer.Value.ToString("F0"); // F0 - чтобы не было символов после запятой

        if (text.text == "0")
            text.text = "GO!";

        /*if (countdouwnTimer.Value == 0)
            text.text = "GO!";*/
    }


}
