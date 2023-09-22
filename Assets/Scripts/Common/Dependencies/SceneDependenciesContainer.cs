using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDependenciesContainer : Dependency // содержит все зависимости для сцены
{
    [SerializeField] private TrackPointCircuit trackPointCircuit;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private Car car;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;

    protected override void BindAll(MonoBehaviour monobehaviourInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monobehaviourInScene);
        Bind<TrackPointCircuit>(trackPointCircuit, monobehaviourInScene);
        Bind<CarInputControl>(carInputControl, monobehaviourInScene);
        Bind<Car>(car, monobehaviourInScene);
        Bind<CameraController>(cameraController, monobehaviourInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monobehaviourInScene);
        Bind<RaceResultTime>(raceResultTime, monobehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjectToBind();
    }
}
