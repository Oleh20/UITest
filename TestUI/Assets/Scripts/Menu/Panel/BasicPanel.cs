using GameInputSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicPanel : InputDeviceReactive
{
    [SerializeField] private List<PanelItem> _panels;
    [SerializeField] private PanelName _openPanel;

    private PanelItem _currentPanel;
    public void OpenPanel(PanelName name)
    {
        foreach (var panel in _panels)
        {
            if (panel.PanelName == name)
                panel.Open();
            else
                panel.Close();
        }
    }
    public void OpenStartPanel()
    {
        OpenPanel(_openPanel);
    }
    public void OpenPanel(string name)
    {
        foreach (var panel in _panels)
        {
            if (panel.PanelName.ToString() == name)
            {
                panel.Open();
                _currentPanel = panel;
            }
            else
            {
                panel.Close();
            }
        }
    }
    public override void OnDeviceChanged(InputDeviceType newDevice)
    {
        if (_currentPanel != null && CheckOnDevice(newDevice) && EventSystem.current.currentSelectedGameObject == null)
        {
            (_currentPanel as BaseToggleItem)?.ReselectFirst();
        }
    }
}
