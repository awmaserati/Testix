using System;

namespace Testix.Core
{
    [Serializable]
    public abstract class EnemyModelBase
    {
        public EnemyType Type;
        public int HP;
        public float MoveSpeed;
    }
}