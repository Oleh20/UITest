using GameInputSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabNavigator : MonoBehaviour
{
    [SerializeField] private List<Selectable> _tabs;
    private int _currentTabIndex;

    private void OnEnable()
    {
        InputReader.Instance.NavigateLeft += NavigateLeft;
        InputReader.Instance.NavigateRight += NavigateRight;

        if (_tabs.Count > 0)
        {
            _currentTabIndex = 0;
            SelectTab(_currentTabIndex);
        }
    }

    private void OnDisable()
    {
        InputReader.Instance.NavigateLeft -= NavigateLeft;
        InputReader.Instance.NavigateRight -= NavigateRight;
    }

    private void NavigateLeft()
    {
        _currentTabIndex--;
        if (_currentTabIndex < 0)
            _currentTabIndex = _tabs.Count - 1;

        SelectTab(_currentTabIndex);
    }

    private void NavigateRight()
    {
        _currentTabIndex++;
        if (_currentTabIndex >= _tabs.Count)
            _currentTabIndex = 0;

        SelectTab(_currentTabIndex);
    }

    private void SelectTab(int index)
    {
        if (_tabs.Count == 0) return;

        EventSystem.current.SetSelectedGameObject(_tabs[index].gameObject);

        if (_tabs[index] is Button button)
        {
            button.onClick.Invoke();
        }
    }

}
