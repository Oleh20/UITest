using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInputSystem
{
    [CreateAssetMenu(fileName = "InputUIIconProvider", menuName = "InputSystem/UIIconProvider")]
    public class InputUIIconProvider : ScriptableObject, IInputUIIconProvider
    {
        [System.Serializable]
        public class IconSet
        {
            public InputDeviceType deviceType;
            public List<ActionIcon> icons;
        }

        [System.Serializable]
        public class ActionIcon
        {
            public InputActionReference action;
            public Sprite sprite;
        }

        [SerializeField] private List<IconSet> _iconSets;

        public Sprite GetIconForAction(InputActionReference actionRef, InputDeviceType deviceType)
        {
            var set = _iconSets.Find(s => s.deviceType == deviceType);
            var icon = set?.icons.Find(i => i.action == actionRef);
            return icon?.sprite;
        }
    }
}
