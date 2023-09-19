using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparation,
    Countdown, // обратный отсчет
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour // обертка для системы заездов
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackPointCircuit trackPointCircuit;
    [SerializeField] private int lapsToComplete;
    [SerializeField] private Timer countdownTimer;

    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    private void Start()
    {
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;
        countdownTimer.Finished += OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }
    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }
    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnCountdownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if(trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace(); // вызов TrackPointPassed
        }

        if(trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }

    public void LaunchPreparationStart() // должен вызываться всего 1 раз
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.Countdown);

        countdownTimer.enabled = true; // запуск таймера
        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.Countdown) return;

        StartState(RaceState.Race);
        Started?.Invoke();
    }   

    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);
        Completed?.Invoke();
    }

    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
}
