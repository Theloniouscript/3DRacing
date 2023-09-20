using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>, IDependency<CarInputControl>
{
    private Car car;
    public void Construct(Car obj) => car = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private CarInputControl carControl;
    public void Construct(CarInputControl obj) => carControl = obj;

    [SerializeField] private float respawnHeight;
    private TrackPoint respawnTrackPoint; // место респавна

    private void Start()
    {
        raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        respawnTrackPoint = point;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (null == respawnTrackPoint) return;
        if (raceStateTracker.State != RaceState.Race) return;

        car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * respawnHeight, 
                    respawnTrackPoint.transform.rotation);

       
        carControl.Reset();
    }
}
