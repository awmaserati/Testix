using System.Collections.Generic;
using UnityEngine;

namespace Testix.Configs
{
    [CreateAssetMenu(fileName = "GameflowConfig", menuName = "Configs/Gameflow")]
    public class GameflowConfig : ScriptableObject
    {
        [SerializeField]
        public MinMaxInt EnemiesCount;
        [SerializeField]
        public MinMaxFloat EnemiesSpawnCD;
        [SerializeField]
        public List<EnemyType> EnemyTypes;
    }
}