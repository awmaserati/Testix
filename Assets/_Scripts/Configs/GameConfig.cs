using UnityEngine;
using Testix.Core;


namespace Testix.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        internal PlayerModel Player;
    }
}