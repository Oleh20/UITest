using GameInputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputUIIconProvider
{
    Sprite GetIconForAction(InputActionReference actionRef, InputDeviceType deviceType);
}
