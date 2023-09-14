﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centerOfMass;

    [Header ("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    [Header("DownForce")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor; // коэффициент прижимной силы

    private new Rigidbody rigidbody;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f; // перевод в км/ч

    //DEBUG
    public float MotorTorque; // крутящий момент
    public float SteerAngle;
    public float BrakeTorque;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody>();

        if(centerOfMass != null )
        {
            rigidbody.centerOfMass = centerOfMass.localPosition;
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();
        UpdateDownForce();
        UpdateWheelAxles();
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downForce); // по направлению вниз
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }

            for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            //DEBUG

            wheelAxles[i].ApplyMotorTorque(MotorTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
        }
    }
}
