using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffects : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaysSlipLimit;
    [SerializeField] private GameObject skidPrefab;  // ссылка на trail

    [SerializeField] private ParticleSystem[] wheelSmoke;

    [SerializeField] private new AudioSource audio;

    private WheelHit wheelHit;
    private Transform[] skidTrail; // чтобы следить за колесом

    private void Start()
    {
        skidTrail = new Transform[wheels.Length];
    }

    private void Update()
    {
        bool isSlip = false; // изначально мы не скользим
                             // алгоритм с флагом

        for(int i = 0; i < wheels.Length; i++) 
        {
            wheels[i].GetGroundHit(out wheelHit);

            if (wheels[i].isGrounded == true)
            {
                if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaysSlipLimit)
                {
                    if (skidTrail[i] == null)
                        skidTrail[i] = Instantiate(skidPrefab).transform;

                    if (audio.isPlaying == false) audio.Play();

                    if (skidTrail[i] != null)
                    {                        
                        skidTrail[i].position = wheelHit.point; // wheelHit.point = wheels[i].transform.position - wheelHit.normal * wheels[i].radius
                                                                // точка внизу в центре колеса
                        skidTrail[i].forward = -wheelHit.normal; // normal - направление колеса

                        wheelSmoke[i].transform.position = skidTrail[i].position; // задаем позицию для частиц дыма, когда колеса проскальзывают
                        wheelSmoke[i].Emit(1); // запуск системы частиц

                        //continue; // чтобы переходил к следующему колесу
                    }

                    isSlip = true;
                    continue; // чтобы переходил к следующему колесу
                }
            }

            skidTrail[i] = null; // как только мы оторвались от Земли и перестали скользить
            wheelSmoke[i].Stop();
        }

        if(isSlip== false)
        {
            audio.Stop(); // если ни одно колесо не проскальзывает
        }
    }
}
