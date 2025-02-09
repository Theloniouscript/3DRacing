﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Text text;
    [SerializeField] private Text hintText;
    private Timer countdownTimer;

    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker obj) => raceStateTracker= obj;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
        hintText.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true; // для текста счетчика
        enabled = true; // для таймера
        hintText.enabled = false;
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0"); // F0 - чтобы не было символов после запятой

        if (text.text == "0")
            text.text = "GO!";

        /*if (countdouwnTimer.Value == 0)
            text.text = "GO!";*/
    }

   
}
