using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testix.UI
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _parent = null;
        [SerializeField]
        private GameObject _winWindowPrefab = null;
        [SerializeField]
        private GameObject _loseWindowPrefab = null;

        private Dictionary<WindowType, WindowBase> _windows = new Dictionary<WindowType, WindowBase>();

        #region MEFs

        internal void ShowWindow(WindowType type)
        {
            WindowBase window = null;

            if(_windows.TryGetValue(type, out window)) 
            { 
                window.Show();
                return;
            }

            switch(type)
            {
                case WindowType.Win:
                    window = CreateWindow(_winWindowPrefab);
                    break;
                case WindowType.Lose:
                    window = CreateWindow(_loseWindowPrefab);
                    break;
            }

            _windows.Add(type, window);
            window.Show();
        }

        private WindowBase CreateWindow(GameObject prefab)
        {
            var go = Instantiate(prefab, _parent);

            return go.GetComponent<WindowBase>();
        }

        #endregion
    }
}