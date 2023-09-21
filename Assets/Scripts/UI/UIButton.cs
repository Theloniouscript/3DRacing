using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool Interactable;

    private bool focus = false;
    public bool Focus => focus;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;

    public virtual void SetFocus()
    {
        if (Interactable == false) return;
        focus = true;
    }

    public virtual void SetUnfocus()
    {
        if (Interactable == false) return;
        focus = false;
    }
    public virtual void OnPointerClick(PointerEventData eventData) // событие эдитора
    {
        if (Interactable == false) return;
        PointerClick?.Invoke(this);
        OnClick?.Invoke();
        Debug.Log("Clicked!");
    }

    public virtual void OnPointerEnter(PointerEventData eventData) // будет вызываться каждый раз при наведении курсора
    {
        if (Interactable == false) return;
        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false) return;
        PointerExit?.Invoke(this);
    }
}
