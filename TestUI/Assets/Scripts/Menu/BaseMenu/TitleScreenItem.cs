using DG.Tweening;
using GameInputSystem;
using System;
using UnityEngine;

public class TitleScreenItem : MenuItem
{
    public static event Action OnTitleScreenFinished;

    [SerializeField] private CanvasGroup _logoGroup;
    [SerializeField] private CanvasGroup _buttonGroup;
    [SerializeField] private float _logoFadeTime = 0.6f;
    [SerializeField] private float _buttonFadeTime = 0.4f;
    [SerializeField] private float _delayBeforeButton = 0.5f;
    private void Start()
    {
        Open();
    }
    private void Initialize()
    {
        InputReader.Instance.OnUISubmit += Submit;
    }
    private void Submit()
    {
        OnTitleScreenFinished?.Invoke();
    }
    private void OnDisable()
    {
        InputReader.Instance.OnUISubmit -= Submit;
    }
    public override void Open()
    {
        base.Open();

        _logoGroup.alpha = 0;
        _logoGroup.transform.localScale = Vector3.one * 0.8f;
        _logoGroup.transform.localPosition = new Vector3(
            _logoGroup.transform.localPosition.x,
            _logoGroup.transform.localPosition.y - 30f,
            _logoGroup.transform.localPosition.z
        );
        _logoGroup.gameObject.SetActive(true);

        _buttonGroup.alpha = 0;
        _buttonGroup.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence().SetUpdate(true);

        seq.Append(_logoGroup.DOFade(1, _logoFadeTime).SetEase(Ease.OutQuad))
            .Join(_logoGroup.transform.DOScale(1f, _logoFadeTime).SetEase(Ease.OutBack))
            .Join(_logoGroup.transform.DOLocalMoveY(_logoGroup.transform.localPosition.y + 30f, _logoFadeTime).SetEase(Ease.OutQuad))
            .AppendInterval(_delayBeforeButton)
            .Append(_buttonGroup.DOFade(1, _buttonFadeTime).SetEase(Ease.OutQuad))
            .OnComplete(() =>
            {
                _buttonGroup.DOFade(0.5f, 0.8f)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);

                Initialize();
            });
    }

}
