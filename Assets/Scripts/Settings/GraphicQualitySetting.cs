﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GraphicQualitySetting : Setting
{
    private int currentLevelIndex = 0; // индекс элемента массива из Project Settings - Quality

    public override bool IsMinValue { get => currentLevelIndex == 0; }
    public override bool IsMaxValue { get => currentLevelIndex == QualitySettings.names.Length - 1; }

    public override void SetNextValue()
    {
        if(IsMaxValue == false)
        {
            currentLevelIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (IsMinValue == false)
        {
            currentLevelIndex--;
        }
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentLevelIndex]; // пока значение массива - строка из enum, который задается в редакторе
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentLevelIndex);
        Save();
    }

    public override void Load()
    {
        currentLevelIndex = PlayerPrefs.GetInt(title, QualitySettings.names.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentLevelIndex);
    }
}
