using System;
using UnityEngine.InputSystem;
namespace GameInputSystem
{
    public enum InputDeviceType
    {
        KeyboardMouse,
        Xbox,
        DualSense,
        OtherGamepad
    }
    public class InputReader : Singleton<InputReader>, GameInput.IUIActions
    {
        public GameInput _gameInput;
        public Action OnUISubmit;
        public Action OnUICancel;
        public Action NavigateLeft;
        public Action NavigateRight;


        protected override void Awake()
        {
            _gameInput ??= new GameInput();
        }

        private void Start()
        {
            InitializeInput();
            UIEnable(true);
        }

        private void OnDestroy()
        {
            _gameInput?.Dispose();
            _gameInput?.Disable();
            _gameInput = null;
        }

        private void InitializeInput()
        {
            _gameInput.UI.SetCallbacks(this);
        }

        private void SetInputEnabled(InputActionMap actionMap, bool enable)
        {
            if (enable)
                actionMap.Enable();
            else
                actionMap.Disable();
        }

        private void UIEnable(bool enable) => SetInputEnabled(_gameInput.UI, enable);

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OnUISubmit?.Invoke();
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                OnUICancel?.Invoke();
            }
        }
        public void OnNavigateLeft(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                NavigateLeft?.Invoke();
        }

        public void OnNavigateRight(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                NavigateRight?.Invoke();
        }
        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

    }
}