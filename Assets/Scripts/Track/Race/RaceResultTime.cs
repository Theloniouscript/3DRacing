using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    public event UnityAction ResultUpdated; // событие: рекорд обновлен

    [SerializeField] private float goldTime; // время трассы
    public float GoldTime => goldTime;

    [SerializeField] private float playerRecordTime;
    public float PlayerRecordTime => playerRecordTime;

    private float currentTime;
    public float CurrentTime => currentTime;

    public bool RecordWasSet => playerRecordTime != 0; // рекорд был установлен

    public static string SaveMark = "_player_best_time";

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted; // зафиксировать текущее время
    }
    private void OnRaceCompleted()
    {
       float absoluteRecord = GetAbsoluteRecord();

       if(raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
        {
            playerRecordTime = raceTimeTracker.CurrentTime;
            Save();
        }

       currentTime = raceTimeTracker.CurrentTime;

       ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord() // если побили gold или свой же рекорд
    {
        if (playerRecordTime < goldTime && playerRecordTime != 0)
            return playerRecordTime;
        
        else
            return goldTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
    }
}
