using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private Car car;
    [SerializeField] private new Camera camera;
    [SerializeField] private CameraFollow follower;
    [SerializeField] private CameraFovCorrector fovCorrector;
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private CameraPathFollower pathFollower;

    private void Awake()
    {
        follower.SetProperties(car, camera);
        fovCorrector.SetProperties(car, camera);
        shaker.SetProperties(car, camera);
    }

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnCompleted;
        follower.enabled = false;
        pathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnPreparationStarted()
    {
        follower.enabled = true;
        pathFollower.enabled = false;
    }
    private void OnCompleted()
    {
        pathFollower.enabled = true;
        pathFollower.StartMoveToNearestPoint();
        pathFollower.SetLookTarget(car.transform);
        follower.enabled = false;
    }
}
