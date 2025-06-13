using UnityEngine;

namespace GameInputSystem
{
    public class InputSetupEntryPoint : MonoBehaviour
    {
        [SerializeField] private InputIconChanger[] _inputIconChangers;
        [SerializeField] private InputDeviceConditionalActivator[] _conditionalActivators;
        [SerializeField] private InputUIIconProvider _iconProvider;
        [SerializeField] private CursorVisibilityController _cursorController;
        [SerializeField] private InputDeviceReactive[] _deviceChangeHandlers;

        private InputDeviceDetector _deviceDetector;

        private void Awake()
        {
            _deviceDetector = new InputDeviceDetector();

            foreach (var changer in _inputIconChangers)
                changer.Initialize(_deviceDetector, _iconProvider);

            foreach (var activator in _conditionalActivators)
                activator.Initialize(_deviceDetector);

            foreach (InputDeviceReactive handler in _deviceChangeHandlers)
                handler.Initialize(_deviceDetector);

            _cursorController?.Initialize(_deviceDetector);
        }
    }
}
