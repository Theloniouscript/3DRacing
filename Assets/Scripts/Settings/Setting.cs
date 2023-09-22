using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ПРИМЕР ИСПОЛЬЗОВАНИЯ:
 * 
 * public class Difficulty : Setting
{
    public enum DifficultyType
    { }

    public DifficultyType GetDifficulty()
    {
        return (DifficultyType)GetValue(); // вызывающий код обращается к Difficulty
    }
}*/

public abstract class Setting : ScriptableObject // чтобы класс кнопок настроек был один
{
    [SerializeField] protected string title; // название настройки

    public string Title => title;

    // минимальное и максимальное значения, чтобя зря не листать лишнее
    public virtual bool IsMinValue { get; }
    public virtual bool IsMaxValue { get; }


    // переключение значения настроек вперед-назад
    public virtual void SetNextValue() { }
    public virtual void SetPreviousValue() { }

    // применение значения
    public virtual object GetValue() { return default(object); } // может быть настройка по геймплею

    // отображение значения
    public virtual string GetStringValue() { return string.Empty; } // возвращает строку

    public virtual void Apply() { }


}
