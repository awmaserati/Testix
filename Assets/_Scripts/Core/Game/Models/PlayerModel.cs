using System;

namespace Testix.Core
{
    [Serializable]
    public class PlayerModel
    {
        public int HP;
        public float MoveSpeed;
        public float ShootRange;
        public float ShootSpeed;
        public int Damage;
        public float BulletSpeed;

        public PlayerModel(PlayerModel data)
        {
            HP = data.HP;
            MoveSpeed = data.MoveSpeed;
            ShootRange = data.ShootRange;
            ShootSpeed = data.ShootSpeed;
            Damage = data.Damage;
            BulletSpeed = data.BulletSpeed;
        }
    }
}