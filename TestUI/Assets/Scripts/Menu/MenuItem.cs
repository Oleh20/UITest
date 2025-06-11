using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public MenuName MenuName => _menuName;

    [SerializeField] private MenuName _menuName;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
