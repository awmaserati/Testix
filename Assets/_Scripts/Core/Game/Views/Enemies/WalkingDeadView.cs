using System;
using UnityEngine;

namespace Testix.Core
{
    public class WalkingDeadView : EnemyViewBase
    {
        internal override event Action OnKilled = null;
        internal override event Action<int> OnDead = null;
        
        [SerializeField]
        private Rigidbody2D _rb = null;

        public WalkingDeadModel Model;

        #region UnityMEFs

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.down * Model.MoveSpeed;
        }

        #endregion

        #region MEFs

        protected override void GetDamage(int damage)
        {
            Model.HP -= damage;
            
            if(Model.HP == 0)
            {
                Die();
            }
        }

        protected override void Die(bool isKilled = true)
        {
            gameObject.SetActive(false);

            if(isKilled)
            {
                OnKilled?.Invoke();
                return;
            }

            OnDead?.Invoke(Model.Damage);
        }

        #endregion
    }
}