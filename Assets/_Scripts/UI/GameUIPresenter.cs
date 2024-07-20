using UnityEngine;
using TMPro;
using Testix.Core;

namespace Testix.UI
{
    public class GameUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _playerHP = null;
        [SerializeField]
        private WindowsManager _windowsManager = null;

        #region UnityMEFs

        private void Start()
        {
            GameField.Instance.OnPlayerHPChanged += UpdatePlayerHP;
            Game.Instance.OnGameEnded += ShowEndGameScreen;
        }

        #endregion

        #region MEFs

        private void UpdatePlayerHP(int playerHP) 
        { 
            _playerHP.text = playerHP.ToString();
        }

        private void ShowEndGameScreen(GameResult result)
        {
            if(result == GameResult.Lose)
            {
                _windowsManager.ShowWindow(WindowType.Lose);
            }
            else
            {
                _windowsManager.ShowWindow(WindowType.Win);
            }
        }

        #endregion
    }
}