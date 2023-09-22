using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : MonoBehaviour
{
    [SerializeField] private Setting setting;

    [SerializeField] private Text titleText;
    [SerializeField] private Text valueText;

    // ссылки на кнопки вправо или влево, чтобы можно было их вкл-выкл
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    private void Start()
    {
        ApplyProperty(setting);
    }

    public void SetNextValueSetting()
    {
        setting?.SetNextValue(); // + проверка на null
        setting?.Apply();
        UpdateInfo();
    }
        
    public void SetPreviousValueSetting()
    {
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();
    }       

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.IsMinValue; // кнопка выключена, если нет еще минимального значения
        nextImage.enabled = !setting.IsMaxValue;
    }

    public void ApplyProperty(Setting property)
    {
        if (property == null) return;
        setting = property;

        UpdateInfo();
    }
}
