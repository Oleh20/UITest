using UnityEngine;
using GameInputSystem;
using UnityEditor.Hardware;

public abstract class InputDeviceReactive : MonoBehaviour, IDeviceChangeHandler
{
    private InputDeviceDetector _deviceDetector;
    public void Initialize(InputDeviceDetector inputDeviceDetector)
    {
        _deviceDetector = inputDeviceDetector;
        _deviceDetector.OnDeviceChanged += OnDeviceChanged;
    }
    protected virtual void OnDestroy()
    {
        if (_deviceDetector != null)
            _deviceDetector.OnDeviceChanged -= OnDeviceChanged;
    }
    protected bool CheckOnDevice(InputDeviceType newDevice)
    {
        return newDevice == InputDeviceType.Xbox || newDevice == InputDeviceType.DualSense || newDevice == InputDeviceType.OtherGamepad;
    } 
    public abstract void OnDeviceChanged(InputDeviceType newDevice);
}
