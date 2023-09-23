using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ResolutionSetting : Setting // не будет видно в редакторе, но будет видно в билде
{
    [SerializeField]
    private Vector2Int[] availableResolution = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
    };

     private int currentResolutionIndex = 0;

    public override bool IsMinValue { get => currentResolutionIndex == 0; }
    public override bool IsMaxValue { get => currentResolutionIndex == availableResolution.Length - 1; }

    public override void SetNextValue()
    {
       if(IsMaxValue == false)
       {
            currentResolutionIndex++;
       }
    }

    public override void SetPreviousValue()
    {
        if (IsMinValue == false)
        {
            currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return availableResolution[currentResolutionIndex]; // 2 числа из массива Vector2Int
    }

    public override string GetStringValue()
    {
        return  availableResolution[currentResolutionIndex].x + "x" + availableResolution[currentResolutionIndex].y;
    }

    public override void Apply() // по сути весь класс - обертка для этой строчки:
    {
        Screen.SetResolution(availableResolution[currentResolutionIndex].x, availableResolution[currentResolutionIndex].y, true);
        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, availableResolution.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }

}
