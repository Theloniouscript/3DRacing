using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class EngineIndicatorColor
{
    public float MaxRpm;
    public Color color;
}
public class CarEngineIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Image image;
    [SerializeField] EngineIndicatorColor[] colors;

    private void Update()
    {
        image.fillAmount = car.EngineRpm / car.EngineMaxRpm;

        for (int i = 0; i < colors.Length; i ++)
        {
            Debug.Log("ColorsUpdate");
            if(car.EngineRpm <= colors[i].MaxRpm)
            {
                image.color = colors[i].color;
                Debug.Log(colors[i].color);
                break; // чтобы не был всегда красный цвет 
            }
        }

    }
}
