using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GameInputSystem
{
    public class InputIconChanger : MonoBehaviour
    {
        [SerializeField] private InputActionReference _action;
        [SerializeField] private Image _icon;

        private IInputDeviceDetector _detector;
        private IInputUIIconProvider _iconProvider;

        public void Initialize(IInputDeviceDetector detector, IInputUIIconProvider iconProvider)
        {
            _detector = detector;
            _iconProvider = iconProvider;

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
            if (_action == null || _icon == null) return;

            _icon.sprite = _iconProvider.GetIconForAction(_action, deviceType);
        }
    }
}
