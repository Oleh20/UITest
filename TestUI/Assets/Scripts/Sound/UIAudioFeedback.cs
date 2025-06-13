using GameInputSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class UIAudioFeedback : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private bool _useClickSound = true;
    [SerializeField] private bool _useHoverSound = true;

    public void OnSelect(BaseEventData eventData)
    {
        if (_useHoverSound)
            PlayHover();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (_useClickSound)
            PlayClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_useHoverSound)
            PlayHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_useClickSound)
            PlayClick();
    }
    public void PlayClick()
    {
        UIAudioManager.Instance.PlayClick();
    }
    public void PlayHover()
    {
        UIAudioManager.Instance.PlayHover();
    }
}
