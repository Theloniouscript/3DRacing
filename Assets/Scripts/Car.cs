using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{ 
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    [Header("Engine")]    
    [SerializeField] private AnimationCurve engineTorqueCurve; // кривая крутящего момента
    [SerializeField] private float engineTorque; // debug, будет не от 0, а от 800 минимум
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineRpm; // debug
    [SerializeField] private float engineMinRpm; 
    [SerializeField] private float engineMaxRpm;


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

        UpdateEngineTorque();

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        chassis.MotorTorque = ThrottleControl * engineTorque; // имитация педали газа 
        chassis.SteerAngle = SteerControl * maxSteerAngle;
        chassis.BrakeTorque = BrakeControl * maxBrakeTorque;       
    }

    private void UpdateEngineTorque() // симуляция двигателя
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm()) * 3.7f; // берем обороты двигателя из оборотов колес
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm); // ограничиваем, чтобы двигатель не крутился быстрее

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque;
    }
}
