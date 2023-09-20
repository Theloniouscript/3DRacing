using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : CameraComponent
{
    [SerializeField] private float shakeAmount;
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizeSpeedShake;

    private void Update()
    {
        if(car.NormalizeLinearVelocity >= normalizeSpeedShake) // при этом условии мы трясемся
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
        }
    }
}
