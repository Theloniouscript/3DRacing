using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{ 
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private CarChassis chassis;

    //DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void Update()
    {
        chassis.MotorTorque = ThrottleControl * maxMotorTorque;
        chassis.SteerAngle = SteerControl * maxSteerAngle;
        chassis.BrakeTorque = BrakeControl * maxBrakeTorque;       
    }
}
