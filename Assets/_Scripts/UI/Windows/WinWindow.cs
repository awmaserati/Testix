using UnityEngine;
using UnityEngine.UI;
using Testix.Core;

namespace Testix.UI
{
    public class WinWindow : WindowBase
    {
        [SerializeField]
        private Button _restartBtn = null;

        #region UntyMEFs

        private void Awake()
        {
            _restartBtn.onClick.AddListener(OnRestartClicked);
        }

        #endregion

        #region MEFs

        internal override void Show()
        {
            base.Show();
        }

        internal override void Hide()
        {
            base.Hide();
        }

        private void OnRestartClicked()
        {
            Game.Instance.RestartGame();
            Hide();
        }

        #endregion
    }
}