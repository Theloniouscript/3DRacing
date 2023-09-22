using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip hover;
    [SerializeField] private AudioClip click;
    private new AudioSource audio;

    private UIButton[] uiButtons;

    private void Start()
    {
        audio= GetComponent<AudioSource>();
        uiButtons= GetComponentsInChildren<UIButton>(true); // мы должны пройтись по всем, даже неактивным игровым объектам

        for(int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter += OnPointerEnter;
            uiButtons[i].PointerClick += OnPointerClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter -= OnPointerEnter;
            uiButtons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerEnter(UIButton arg0)
    {
        audio.PlayOneShot(hover);
    }

    private void OnPointerClicked(UIButton arg0)
    {
        audio.PlayOneShot(click);
    }
}
