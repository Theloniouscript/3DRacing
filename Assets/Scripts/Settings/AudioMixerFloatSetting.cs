using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParameter;
    
    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;

    [SerializeField] private float virtualStep; // шаг от 0 до 80, а пользователю удобнее работать с числами от 0 до 100, значит, надо конвертироват
    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    private float currentValue = 0;

    public override bool IsMinValue { get => currentValue == minRealValue; }
    public override bool IsMaxValue { get => currentValue == maxRealValue; }

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs((maxRealValue - minRealValue) / virtualStep));
    }

    public override void SetPreviousValue()
    {
        AddValue( - Mathf.Abs((maxRealValue - minRealValue) / virtualStep));
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
        //  (currentValue - minRealValue) = либо 0, либо 1; (maxRealValue - minRealValue) - конкретное значение
    }

    public override object GetValue() // нужно получить число от 0 до -80, преобразованное в от 0 до 100
    {
        return currentValue; // float-значение
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue); // чтобы вычислить реальный шаг в SetNextValue()
    }

    public override void Apply()
    {
        audioMixer.SetFloat(nameParameter, currentValue);
        Save();
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(title, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentValue);
    }

}
