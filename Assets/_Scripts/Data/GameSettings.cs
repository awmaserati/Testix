using UnityEngine;
using Testix.Utils;
using Testix.Configs;

namespace Testix
{
    public class GameSettings : Singleton<GameSettings>
    {
        [SerializeField]
        internal GameConfig Config;

        #region UnityMEFs

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion
    }
}