using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testix.Core
{
    public class PlayerView : MonoBehaviour
    {
        internal event Action<int> OnHPChanged = null;
        internal event Action OnDead = null;

        [SerializeField]
        private Rigidbody2D _rb = null;
        [SerializeField]
        private ShootingAreaView _radar = null;
        [SerializeField]
        private GameObject _bulletPrefab = null;
        [SerializeField]
        private float _spriteBorderOffset = 0.0f;

        private PlayerModel _model;
        private Vector2 _direction;
        private List<EnemyViewBase> _targets = new List<EnemyViewBase>();
        private int _currentTargetIndex = -1;
        private List<BulletView> _bullets = new List<BulletView>();
        private Camera _camera = null;
        private Coroutine _shootCoroutine = null;

        #region UnityMEFs

        private void Start()
        {
            InputController.Instance.OnMove += OnMove;
            _radar.OnEnemyAdded += OnGetTarget;
            _radar.OnEnemyRemoved += OnLoseTarget;
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _direction * _model.MoveSpeed;
            CheckFieldBounds();
        }

        #endregion

        #region MEFs

        internal void Init(PlayerModel model)
        {
            _model = model;
            OnHPChanged?.Invoke(_model.HP);
        }

        internal void Clean()
        {
            StopAllCoroutines();
            _shootCoroutine = null;
            _model.HP = GameSettings.Instance.Config.Player.HP;

            foreach(var bullet in _bullets)
            {
                Destroy(bullet.gameObject);
            }

            _bullets.Clear();
            _targets.Clear();
            _direction = Vector2.zero;
            _currentTargetIndex = -1;
        }

        private void OnMove(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnGetTarget(EnemyViewBase enemy)
        {
            _targets.Add(enemy);

            if(_currentTargetIndex == -1)
            {
                _currentTargetIndex = 0;

                if(_shootCoroutine != null)
                {
                    return;
                }

                _shootCoroutine = StartCoroutine(Shoot());
                return;
            }

            FindNewTarget();
        }

        private void OnLoseTarget(EnemyViewBase enemy) 
        {
            _targets.Remove(enemy);

            if (_targets.Count == 0)
            {
                _currentTargetIndex = -1;

                //if (_shootCoroutine != null)
                //{
                //    StopCoroutine(_shootCoroutine);
                //    _shootCoroutine = null;
                //}
            }

            FindNewTarget();
        }

        private void FindNewTarget()
        {
            var distance = _model.ShootRange + 1.0f;

            for (var i = 0; i < _targets.Count; i++)
            {
                var currentDistance = Vector3.Distance(transform.position, _targets[i].transform.position);

                if (distance > currentDistance)
                {
                    _currentTargetIndex = i;
                    distance = currentDistance;
                }
            }
        }

        private IEnumerator Shoot()
        {
            do
            {
                BulletView nextBullet = null;

                foreach (var bullet in _bullets)
                {
                    if (!bullet.IsReady)
                    {
                        continue;
                    }

                    nextBullet = bullet;
                    nextBullet.transform.position = transform.position;
                    break;
                }

                if(!nextBullet)
                {
                    nextBullet = CreateBullet();
                    _bullets.Add(nextBullet);
                }

                nextBullet.Init(_targets[_currentTargetIndex].transform.position);

                yield return new WaitForSeconds(GameSettings.Instance.Config.Player.ShootSpeed);
            } while (_targets.Count > 0);

            _shootCoroutine = null;
        }

        private BulletView CreateBullet()
        {
            var go = Instantiate(_bulletPrefab, transform.parent);
            go.transform.position = transform.position;
            var view = go.GetComponent<BulletView>();

            return view;
        }

        internal void GetDamage(int damage)
        {
            _model.HP -= damage;
            OnHPChanged?.Invoke(_model.HP);

            if (_model.HP == 0)
            {
                StopAllCoroutines();
                OnDead?.Invoke();
            }
        }

        private void CheckFieldBounds()
        {
            Vector2 positionOnScreen = _camera.WorldToScreenPoint(transform.position);

            if(positionOnScreen.x < _spriteBorderOffset && _rb.velocity.x < 0 ||
                positionOnScreen.x > _camera.pixelWidth - _spriteBorderOffset && _rb.velocity.x > 0) 
            {
                _rb.velocity = new Vector2(0.0f, _rb.velocity.y);
            }

            if(positionOnScreen.y < _spriteBorderOffset && _rb.velocity.y < 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
            }
        }

        #endregion
    }
}