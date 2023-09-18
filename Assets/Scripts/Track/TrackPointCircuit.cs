using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackPointCircuit : MonoBehaviour
{
    public event UnityAction<int> LapCompleted;
    public event UnityAction<TrackPoint> TrackPointTriggered;
    [SerializeField] private TrackType type;
    private TrackPoint[] points;

    private int lapsCompleted = -1;

    private void Awake()
    {
        BuildCircuit();
    }

    private void Start()
    {
        LapCompleted += (t) => Debug.Log("Lap completed"); // Debug only
        
        for(int i = 0; i < points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }

        points[0].AssignAsTarget();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.Next?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast == true)
        {
            lapsCompleted++;

            if(type == TrackType.Sprint)             
                LapCompleted?.Invoke(lapsCompleted);
            

            if(type == TrackType.Circular)
            {
                if (lapsCompleted > 0)
                    LapCompleted?.Invoke(lapsCompleted);
            }
        }
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit() // автоматизация сборки путевых точек
    {
        points = TrackCircuitBuilder.Build(transform, type);
    }
}
