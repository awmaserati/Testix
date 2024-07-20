﻿using UnityEngine;

namespace Testix.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        internal virtual void Show()
        {
            gameObject.SetActive(true);
        }

        internal virtual void Hide() 
        { 
            gameObject.SetActive(false);
        }
    }
}