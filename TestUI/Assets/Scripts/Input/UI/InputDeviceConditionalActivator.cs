using UnityEngine;
using GameInputSystem;

public class InputDeviceConditionalActivator : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool enableOnGamepad = true;
    [SerializeField] private bool invert = false;

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
        bool shouldEnable = enableOnGamepad ? isGamepad : !isGamepad;
        if (invert) shouldEnable = !shouldEnable;

        if (targetObject != null)
            targetObject.SetActive(shouldEnable);
    }
}
