using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private float speedThreshold;

    [Header("Offset")]
    [SerializeField] private float viewHeight;
    [SerializeField] private float height;
    [SerializeField] private float distance;


    [Header("Damping")]
    [SerializeField] private float rotationDamping;
    [SerializeField] private float heightDamping;

    private void FixedUpdate()
    {

        Vector3 velocity = rigidbody.velocity;
        Vector3 targetRotation = target.eulerAngles; // Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
                                                    // target.eulerAngles; 
                                                    // направление: из вектора направления получить вектор вращения в эйлеровских углах

        //Rotation
        transform.LookAt(target.position + new Vector3(0, viewHeight, 0));

        //Position
        Vector3 positionOffset = Quaternion.Euler(0, targetRotation.y, 0) * Vector3.back * distance; // один из вариантов получения угла
        transform.position = target.position - positionOffset;
        transform.position = new Vector3(transform.position.x, target.position.y + height, transform.position.z);

        /*if(velocity.magnitude > speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles; 
                            // Quaternion.LookRotation(velocity, Vector3.up).eulerAngles; LookRotation - перевод поворота в вектор
        }

        //Lerp интерполяция
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
        float currentHeight = Mathf.Lerp(transform.position.y, target.position.y + height, heightDamping * Time.fixedDeltaTime);
*//*
        //Position
        Vector3 positionOffset = Quaternion.Euler(0, targetRotation.y, 0) * Vector3.back * distance; // один из вариантов получения угла
        transform.position = target.position - positionOffset;
        transform.position = new Vector3(transform.position.x, target.position.y + height, transform.position.z);

        //Rotation
        transform.LookAt(target.position + new Vector3(0, viewHeight, 0));*/

        

    }

}
