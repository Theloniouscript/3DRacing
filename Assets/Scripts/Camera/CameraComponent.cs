using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public abstract class CameraComponent : MonoBehaviour
{
    protected Car car;
    protected new Camera camera;

    public virtual void SetProperties(Car car, Camera camera)
    {
        this.car = car;
        this.camera = camera;
    }
}

