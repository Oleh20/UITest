using UnityEngine;

public class MenuItem : BaseToggleItem
{
    public MenuName MenuName => _menuName;

    [SerializeField] private MenuName _menuName;
}
