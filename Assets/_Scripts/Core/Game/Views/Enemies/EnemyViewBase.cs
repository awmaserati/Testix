using System;
using UnityEngine;

namespace Testix.Core
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        internal virtual event Action OnKilled = null;
        internal virtual event Action<int> OnDead = null;
        
        protected abstract void GetDamage(int damage);
        protected abstract void Die(bool isKilled = true);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag.Equals("bullet"))
            {
                GetDamage(collision.gameObject.GetComponent<BulletView>().Damage);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("finish line"))
            {
                Die(false);
            }
        }
    }
}