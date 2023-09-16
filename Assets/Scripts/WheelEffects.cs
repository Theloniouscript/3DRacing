using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffects : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaysSlipLimit;
    [SerializeField] private GameObject skidPrefab;  // ссылка на trail

    private WheelHit wheelHit;
    private Transform[] skidTrail; // чтобы следить за колесом

    private void Start()
    {
        skidTrail = new Transform[wheels.Length];
    }

    private void Update()
    {
        for(int i = 0; i < wheels.Length; i++) 
        {
            wheels[i].GetGroundHit(out wheelHit);

            if (wheels[i].isGrounded == true)
            {
                if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaysSlipLimit)
                {
                    if (skidTrail[i] == null)
                        skidTrail[i] = Instantiate(skidPrefab).transform;

                    if (skidTrail[i] != null)
                    {
                        skidTrail[i].position = wheelHit.point; // wheelHit.point = wheels[i].transform.position - wheelHit.normal * wheels[i].radius
                                                                // точка внизу в центре колеса
                        skidTrail[i].forward = -wheelHit.normal; // normal - направление колеса
                    }

                    continue; // чтобы переходил к следующему колесу
                }
            }

            skidTrail[i] = null; // как только мы оторвались от Земли и перестали скользить
        }
    }
}
