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

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio; // передача дифференциала

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;
    // DEBUG
    [SerializeField] private float selectedGear; // выбранная передача из массива gears
    [SerializeField] private float rearGear; // задний ход
    [SerializeField] private int selectedGearIndex;

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
        AutoGearShift();

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        chassis.MotorTorque = ThrottleControl * engineTorque; // имитация педали газа 
        chassis.SteerAngle = SteerControl * maxSteerAngle;
        chassis.BrakeTorque = BrakeControl * maxBrakeTorque;       
    }

    // Gearbox обертка для метода переключения передач ShiftGear()

    private void AutoGearShift()
    {
        if(selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm) UpGear();
        if (engineRpm < downShiftEngineRpm) DownGear();
    }

    public void UpGear() // подднять передачу
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear() // опустить передачу
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear() // задний ход
    {
        selectedGear = rearGear;
    }

    public void ShiftToFirstGear() // переключение на первую передачу
    {
        ShiftGear(0);
    }

    public void ShiftToNeutral()
    {
        selectedGear = 0;
    }
    private void ShiftGear(int gearIndex) // переключение передач
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length- 1);
        selectedGear = gears[gearIndex];

        // Debug какая передача сейчас включена:
        selectedGear = gearIndex;
    }

    private void UpdateEngineTorque() // симуляция двигателя
    {        
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm()) * selectedGear * finalDriveRatio; // берем обороты двигателя из оборотов колес
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm); // ограничиваем, чтобы двигатель не крутился быстрее

        // регулируем задний ход передачи
        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
    }
}
