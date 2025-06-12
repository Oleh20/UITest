using UnityEngine;
using GameInputSystem;

public class CursorVisibilityController : MonoBehaviour
{
    [SerializeField] private bool hideCursorWhenUsingGamepad = true;

    private IInputDeviceDetector _detector;

    public void Initialize(IInputDeviceDetector detector)
    {
        _detector = detector;
        _detector.OnDeviceChanged += OnDeviceChanged;
        OnDeviceChanged(_detector.CurrentInputDeviceType);
    }

    private void OnDestroy()
    {
        if (_detector != null)
            _detector.OnDeviceChanged -= OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDeviceType deviceType)
    {
        bool isGamepad = deviceType != InputDeviceType.KeyboardMouse;

        if (hideCursorWhenUsingGamepad)
        {
            Cursor.visible = !isGamepad;
            Cursor.lockState = isGamepad ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
