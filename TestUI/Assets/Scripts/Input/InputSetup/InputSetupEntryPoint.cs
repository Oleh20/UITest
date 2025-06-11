using UnityEngine;

namespace GameInputSystem
{
    public class InputSetupEntryPoint : MonoBehaviour
    {
        [SerializeField] private InputIconChanger[] _inputIconChangers;
        [SerializeField] private InputUIIconProvider _iconProvider;

        private InputDeviceDetector _deviceDetector;

        private void Awake()
        {
            _deviceDetector = new InputDeviceDetector();

            foreach (var changer in _inputIconChangers)
                changer.Initialize(_deviceDetector, _iconProvider);
        }
    }
}
