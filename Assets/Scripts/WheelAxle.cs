using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelAxle // Физика колесной оси
{
    [SerializeField] private WheelCollider leftWheelCollider;
    [SerializeField] private WheelCollider rightWheelCollider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;


    [SerializeField] private float wheelWidth;
    [SerializeField] private float antiRollForce;

    [SerializeField] private float baseForwardStiffness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;
    [SerializeField] private float baseSidewaysStiffness = 2.0f;
    [SerializeField] private float stabilitySidewaysFactor = 1.0f;

    [SerializeField] private float additionalWheelDownForce; // дополнительная сила

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;


    //Public API

    public void Update()
    {
        UpdateWheelHits();
        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffness();

        SyncMeshTransform();
    }

    private void UpdateWheelHits()
    {
        leftWheelCollider.GetGroundHit(out leftWheelHit);
        rightWheelCollider.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffness() // общая сила трения, проскальзывание колеса
    {
        // Кривые прямолинейного трения
        WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

        // Кривые бокового трения
        WheelFrictionCurve leftSideways = leftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

        leftSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewaysFactor;
        rightSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewaysFactor;

        leftWheelCollider.forwardFriction = leftForward;
        rightWheelCollider.forwardFriction = rightForward;
        leftWheelCollider.sidewaysFriction = leftSideways;
        rightWheelCollider.sidewaysFriction = rightSideways;
    }

    private void ApplyDownForce() // прижимная сила для колес
    {
        if (leftWheelCollider.isGrounded == true)
            leftWheelCollider.attachedRigidbody.AddForceAtPosition
                (leftWheelHit.normal * -additionalWheelDownForce * leftWheelCollider.attachedRigidbody.velocity.magnitude, 
                leftWheelCollider.transform.position); // normal = transform.up

        if (rightWheelCollider.isGrounded == true)
            rightWheelCollider.attachedRigidbody.AddForceAtPosition
                (rightWheelHit.normal * -additionalWheelDownForce * rightWheelCollider.attachedRigidbody.velocity.magnitude,
                rightWheelCollider.transform.position); 
    }

    private void ApplyAntiRoll() // стабилизатор поперечной устойчивости
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if(leftWheelCollider.isGrounded == true)
        {
            travelL = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y 
                       - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;
        }

        if (rightWheelCollider.isGrounded == true)
        {
            travelL = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y
                       - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;
        }

        float forceDir = (travelL - travelR); // знак, показывает, движение влево или вправо

        if (leftWheelCollider.isGrounded == true)
        {
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * -forceDir * antiRollForce, 
                                                                   leftWheelCollider.transform.position);
        }

        if (rightWheelCollider.isGrounded == true)
        {
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * forceDir * antiRollForce, 
                                                                    rightWheelCollider.transform.position);
        }



    }

    public void ApplySteerAngle(float steerAngle, float wheelBaseLength) // угол поворота // угол Аккермана
    {
        if (isSteer == false) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
        float angleSign = Mathf.Sign(steerAngle);

        Debug.Log("radius " + radius);

        if(steerAngle > 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSign;

            Debug.Log("steerAngle > 0" + steerAngle);
        }
        else if(steerAngle < 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSign;

            Debug.Log("steerAngle < 0" + steerAngle);

        }
        else
        {
            leftWheelCollider.steerAngle = 0;
            rightWheelCollider.steerAngle = 0;

            Debug.Log("steerAngle = 0" + steerAngle);
        }       
        
    }

    public void ApplyMotorTorque(float motorTorque) // крутящий момент
    {
        if (isMotor == false) return;
        leftWheelCollider.motorTorque = motorTorque;
        rightWheelCollider.motorTorque = motorTorque;
    }

    public void ApplyBrakeTorque(float brakeTorque)
    {        
        leftWheelCollider.brakeTorque = brakeTorque;
        rightWheelCollider.brakeTorque = brakeTorque;
    }

    //private
    private void SyncMeshTransform() // синхронизирует коллайдеры и трансформ
    {
        UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
        UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
