using System.Collections.Generic;
using UnityEngine;

public class BasicPanel : MonoBehaviour
{
    [SerializeField] private List<PanelItem> _panels;
    [SerializeField] private PanelName _openPanel;
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
            }
            else
            {
                panel.Close();
            }
        }
    }
}
