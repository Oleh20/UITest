using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup))]
public class BaseToggleItem : MonoBehaviour
{
    [SerializeField] protected GameObject _firstSelected;
    [SerializeField] private float _fadeDuration = 0.3f;

    private CanvasGroup _canvasGroup;
    private Tween _fadeTween;
    private CanvasGroup TryGetCanvasGroup() => _canvasGroup ??= GetComponent<CanvasGroup>();

    public virtual void Open()
    {
        gameObject.SetActive(true);

        _fadeTween?.Kill();
        TryGetCanvasGroup().alpha = 0;
        TryGetCanvasGroup().interactable = true;
        TryGetCanvasGroup().blocksRaycasts = true;

        _fadeTween = TryGetCanvasGroup().DOFade(1, _fadeDuration).SetUpdate(true).OnComplete(SetSelectedObject);
    }
    public virtual void Close()
    {
        _fadeTween?.Kill();

        TryGetCanvasGroup().interactable = false;
        TryGetCanvasGroup().blocksRaycasts = false;

        _fadeTween = TryGetCanvasGroup().DOFade(0, _fadeDuration)
            .SetUpdate(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
    private void SetSelectedObject()
    {
        if (_firstSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }
    }
}
