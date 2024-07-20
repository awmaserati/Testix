using System;
using UnityEngine;
using Testix.Utils;
using UnityEngine.InputSystem;

namespace Testix
{
    public class InputController : Singleton<InputController>
    {
        internal event Action<Vector2> OnMove = null;

        [SerializeField]
        private InputAction _controls = null;

        private Vector2 _direction;

        #region UnityMEFs

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            _direction = _controls.ReadValue<Vector2>();
            OnMove?.Invoke(_direction);
        }

        #endregion
    }
}