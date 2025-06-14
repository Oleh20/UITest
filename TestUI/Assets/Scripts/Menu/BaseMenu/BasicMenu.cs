using GameInputSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum MenuName
{
    MainMenu,
    Settings,
    TitleScreen,
    CreditsScreen,
    LoadGameScreen
}
public class BasicMenu : InputDeviceReactive, IDeviceChangeHandler
{
    [SerializeField] protected List<MenuItem> _menus;
    [SerializeField] protected MenuName _startScreen;

    private MenuItem _currentMenu;
    private void OnEnable()
    {
        TitleScreenItem.OnTitleScreenFinished += InitializeMenu;
    }

    private void OnDisable()
    {
        TitleScreenItem.OnTitleScreenFinished -= InitializeMenu;
    }
    private void InitializeMenu()
    {
        OpenMenu(_startScreen);
        InputReader.Instance.OnUISubmit += SkipTitleScreen;
        InputReader.Instance.OnUICancel += CancelEvent;
    }
    public override void OnDeviceChanged(InputDeviceType newDevice)
    {
        if (_currentMenu != null && CheckOnDevice(newDevice) && EventSystem.current.currentSelectedGameObject == null)
        {
            (_currentMenu as BaseToggleItem)?.ReselectFirst();
        }
    }
    private void CancelEvent()
    {
        OpenMenu(MenuName.MainMenu);
    }
    private void SkipTitleScreen()
    {
        OpenMenu(MenuName.MainMenu);
        InputReader.Instance.OnUISubmit -= SkipTitleScreen;
    }
    public void OpenMenu(MenuName menuName)
    {
        foreach (MenuItem menu in _menus)
        {
            if (menu.MenuName == menuName)
            {
                if (_currentMenu == menu)
                    return;

                menu.Open();
                _currentMenu = menu;
            }
            else
            {
                menu.Close();
            }
        }
    }
    public void OpenMenu(string menuName)
    {
        foreach (MenuItem menu in _menus)
        {
            if (menu.MenuName.ToString() == menuName)
            {
                if (_currentMenu == menu)
                    return;

                menu.Open();
                _currentMenu = menu;
            }
            else
            {
                menu.Close();
            }
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
