using System;
using Testix.Utils;

namespace Testix.Core
{
    public class Game : Singleton<Game>
    {
        internal event Action<GameResult> OnGameEnded = null;

        #region UnityMEFs

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            StartGame();
        }

        #endregion

        #region MEFs

        internal void StartGame()
        {
            GameField.Instance.CreateField();
        }

        internal void EndGame(GameResult result)
        {
            OnGameEnded?.Invoke(result);
        }

        internal void RestartGame()
        {
            GameField.Instance.CreateField();
        }

        #endregion
    }
}
