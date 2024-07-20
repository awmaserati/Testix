using System;
using UnityEngine;

namespace Testix.Core
{
    public class BulletView : MonoBehaviour
    {
        internal event Action<BulletView> OnDisactive = null;

        [SerializeField]
        private Rigidbody2D _rb = null;

        internal bool IsReady { get; private set; }

        internal int Damage = 0;
        private float _speed = 0.0f;
        private Vector2 _direction = Vector2.zero;
        private Camera _camera = null;

#region UnityMEFs

        private void Awake()
        {
            Damage = GameSettings.Instance.Config.Player.Damage;
            _speed = GameSettings.Instance.Config.Player.BulletSpeed;
            _camera = Camera.main;
        }

        private void Update()
        {
            if(IsOutOfScreen())
            {
                Disable();
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity = _direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.gameObject.tag.Equals("enemy"))
            {
                return;
            }

            Disable();
        }

        #endregion

        #region MEFs

        internal void Init(Vector3 target)
        {
            _direction = target - transform.position;
            _direction.Normalize();
            gameObject.SetActive(true);
            IsReady = false;
        }

        private bool IsOutOfScreen()
        {
            var positionOnScreen = _camera.WorldToScreenPoint(transform.position);

            if(positionOnScreen.y > _camera.pixelHeight ||
                positionOnScreen.x < 0 || positionOnScreen.x > _camera.pixelWidth)
            {
                return true;
            }

            return false;
        }

        private void Disable()
        {
            IsReady = true;
            OnDisactive?.Invoke(this);
            gameObject.SetActive(false);
        }

        #endregion
    }
}