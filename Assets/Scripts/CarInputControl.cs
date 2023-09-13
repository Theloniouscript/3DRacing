using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;

    private void Update()
    {
        car.ThrottleControl = Input.GetAxis("Vertical");
        car.BrakeControl = Input.GetAxis("Jump");
        car.SteerControl = Input.GetAxis("Horizontal");
    }
}
