using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    public void ApplyProperty(RaceInfo property)
    {
        if (property == null) return;
        raceInfo = property;

        icon.sprite = raceInfo.Sprite;
        title.text = raceInfo.Title;
    }

    private void Start()
    {
        ApplyProperty(raceInfo);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if(raceInfo == null) return;

        SceneManager.LoadScene(raceInfo.SceneName);
    }
}
