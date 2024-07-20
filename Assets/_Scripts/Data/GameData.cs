using System;

namespace Testix
{
    [Serializable]
    public struct MinMaxInt
    {
        public int Min;
        public int Max;
    }

    [Serializable]
    public struct MinMaxFloat
    {
        public float Min;
        public float Max;
    }

    [Serializable]
    public enum EnemyType
    {
        WalkingDead = 0,
    }

    [Serializable]
    public enum WeaponType
    {
        Phaser = 0,
    }

    public enum GameResult
    {
        Default = 0,
        Lose = 1,
        Win = 2,
    }

    public enum WindowType
    {
        Lose = 0,
        Win = 1,
    }
}
