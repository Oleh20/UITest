using GameInputSystem;
using System.Collections.Generic;
using UnityEngine;
public enum MenuName
{
    MainMenu,
    Settings,
    TitleScreen,
    CreditsScreen
}
public class BasicMenu : MonoBehaviour
{
    [SerializeField] protected List<MenuItem> _menus;
    [SerializeField] protected MenuName _startScreen;

    private void Start()
    {
        OpenMenu(_startScreen);
        InputReader.Instance.OnUISubmit += SkipTitleScreen;
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
                menu.Open();
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
                menu.Open();
            }
            else
            {
                menu.Close();
            }
        }
    }
}
