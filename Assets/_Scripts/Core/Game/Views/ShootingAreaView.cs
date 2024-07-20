using System;
using UnityEngine;

namespace Testix.Core
{
    public class ShootingAreaView : MonoBehaviour
    {
        internal event Action<EnemyViewBase> OnEnemyAdded = null;
        internal event Action<EnemyViewBase> OnEnemyRemoved = null;

        [SerializeField]
        private CircleCollider2D _collider = null;
        [SerializeField]
        private Transform _viewArea = null;

        private void Awake()
        {
            _collider.radius = GameSettings.Instance.Config.Player.ShootRange / transform.parent.localScale.x;
            _viewArea.localScale = new Vector3(GameSettings.Instance.Config.Player.ShootRange * 4.0f,
                GameSettings.Instance.Config.Player.ShootRange * 4.0f, 0.0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.gameObject.tag.Equals("enemy"))
            {
                return;
            }

            var enemyView = collision.gameObject.GetComponent<EnemyViewBase>();
            OnEnemyAdded?.Invoke(enemyView);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.tag.Equals("enemy"))
            {
                return;
            }

            var enemyView = collision.gameObject.GetComponent<EnemyViewBase>();
            OnEnemyRemoved?.Invoke(enemyView);
        }
    }
}