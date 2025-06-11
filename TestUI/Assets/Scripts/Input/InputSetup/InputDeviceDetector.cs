using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using GameInputSystem;

public class InputDeviceDetector : IInputDeviceDetector, IDisposable
{
    public InputDeviceType CurrentInputDeviceType { get; private set; } = InputDeviceType.KeyboardMouse;
    public event Action<InputDeviceType> OnDeviceChanged;

    public InputDeviceDetector()
    {
        InputSystem.onEvent += OnInputEvent;
        InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Removed:
            case InputDeviceChange.Added:
            case InputDeviceChange.Disconnected:
                UpdateDeviceTypeFromConnectedDevices();
                break;
        }
    }
    private void UpdateDeviceTypeFromConnectedDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad)
            {
                var newDeviceType = DetermineDeviceType(device);
                if (newDeviceType != CurrentInputDeviceType)
                {
                    CurrentInputDeviceType = newDeviceType;
                    OnDeviceChanged?.Invoke(CurrentInputDeviceType);
                }
                return;
            }
        }
        if (CurrentInputDeviceType != InputDeviceType.KeyboardMouse)
        {
            CurrentInputDeviceType = InputDeviceType.KeyboardMouse;
            OnDeviceChanged?.Invoke(CurrentInputDeviceType);
        }
    }
    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        if (!IsSignificantInput(eventPtr, device))
            return;

        var newDeviceType = DetermineDeviceType(device);
        if (newDeviceType != CurrentInputDeviceType)
        {
            CurrentInputDeviceType = newDeviceType;
            OnDeviceChanged?.Invoke(CurrentInputDeviceType);
        }
    }

    private static InputDeviceType DetermineDeviceType(InputDevice device)
    {
        if (device is Gamepad gamepad)
        {
            var name = gamepad.name.ToLowerInvariant();
            if (name.Contains("xbox")) return InputDeviceType.Xbox;
            if (name.Contains("dual") || name.Contains("ps5") || name.Contains("playstation")) return InputDeviceType.DualSense;
            return InputDeviceType.OtherGamepad;
        }

        if (device is Keyboard || device is Mouse)
            return InputDeviceType.KeyboardMouse;

        return InputDeviceType.KeyboardMouse;
    }

    private static bool IsSignificantInput(InputEventPtr eventPtr, InputDevice device)
    {
        foreach (var control in device.allControls)
        {
            if (control.synthetic || control.noisy)
                continue;

            switch (control)
            {
                case ButtonControl button:
                    if (button.ReadValueFromEvent(eventPtr) > 0.5f)
                        return true;
                    break;

                case AxisControl axis:
                    if (Mathf.Abs(axis.ReadValueFromEvent(eventPtr)) > 0.25f)
                        return true;
                    break;

                case Vector2Control vector2 when control.name == "delta":
                    if (vector2.ReadValueFromEvent(eventPtr).sqrMagnitude > 0.01f)
                        return true;
                    break;
            }
        }

        return false;
    }

    public void Dispose()
    {
        InputSystem.onEvent -= OnInputEvent;
    }
}
