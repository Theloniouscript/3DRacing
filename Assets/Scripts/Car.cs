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

    

    [SerializeField] private AnimationCurve engineTorqueCurve; // кривая крутящего момента
    [SerializeField] private int maxSpeed;

    private CarChassis chassis;

    public float LinearVelocity => chassis.LinearVelocity; // скорость всей машины
    public float WheelSpeed => chassis.GetWheelSpeed(); // скорость колес

    public float MaxSpeed => maxSpeed;

    //DEBUG
    [SerializeField] private float linearVelocity;
    public float ThrottleControl; // педаль газа
    public float SteerControl;
    public float BrakeControl;
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void Update()
    {
        linearVelocity = LinearVelocity; // будет отсчитываться автоматически

        //  крутящий момент
        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque; // изменится от 0 до 1

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        chassis.MotorTorque = ThrottleControl * engineTorque; 
        chassis.SteerAngle = SteerControl * maxSteerAngle;
        chassis.BrakeTorque = BrakeControl * maxBrakeTorque;       
    }
}
