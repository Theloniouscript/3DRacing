using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public TrackPoint Next;
    public bool IsFirst;
    public bool IsLast;

    public event UnityAction<TrackPoint> Triggered; // Если автомобиль проехал до точки, вызов события

    protected bool isTarget;
    public bool IsTarget => isTarget;

    public void Passed()
    {
        isTarget= false;
        OnPassed();
    }

    public void AssignAsTarget()
    {
        isTarget = true;
        OnAssignAsTarget();
    }

    protected virtual void OnPassed() { }

    protected virtual void OnAssignAsTarget() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;

        Triggered?.Invoke(this);
    }

    public void Reset() // сброс массива
    {
        Next = null;
        IsLast = false;
        IsFirst= false;
    }
}
