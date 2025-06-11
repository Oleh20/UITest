using GameInputSystem;
using System;

public interface IInputDeviceDetector
{
    InputDeviceType CurrentInputDeviceType { get; }
    event Action<InputDeviceType> OnDeviceChanged;
}
