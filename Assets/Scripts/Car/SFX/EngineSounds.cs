using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class EngineSounds : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AudioSource engineAudioSource;


    // коэффициенты для разных градаций
    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;


    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSource.pitch = basePitch + pitchModifier * (car.EngineRpm / car.EngineMaxRpm) * rpmModifier; // нормализованное значение момента от 0 до 1
        engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
    }
}
