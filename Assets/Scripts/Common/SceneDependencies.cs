using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private TrackPointCircuit trackPointCircuit;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private Car car;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;

    private void Awake()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);
        }
    }

    private void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<TrackPointCircuit>) (mono as IDependency<TrackPointCircuit>).Construct(trackPointCircuit);
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStateTracker);
        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(carInputControl);
        if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(car);
        if (mono is IDependency<CameraController>) (mono as IDependency<CameraController>).Construct(cameraController);
        if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(raceTimeTracker);
    }
}
