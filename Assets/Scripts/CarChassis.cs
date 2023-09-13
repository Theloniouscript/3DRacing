using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;

    //DEBUG
    public float MotorTorque;
    public float SteerAngle;
    public float BrakeTorque;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
        for(int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            //DEBUG

            wheelAxles[i].ApplyMotorTorque(MotorTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }
}
