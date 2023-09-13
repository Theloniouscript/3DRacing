using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float steerAngle;
    [SerializeField] private Transform[] wheelMeshs;
    private void Update()
    {
        for(int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * brakeTorque;

            Vector3 position;
            Quaternion rotation;
            
            wheelColliders[i].GetWorldPose(out position, out rotation);

            wheelMeshs[i].position = position;
            wheelMeshs[i].rotation = rotation;

        }

        wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
    }
}
