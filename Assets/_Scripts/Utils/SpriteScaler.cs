using UnityEngine;

namespace Testix.Utils
{
    public class SpriteScaler : MonoBehaviour
    {
        [SerializeField]
        private bool _width = false;
        [SerializeField]
        private bool _height = false;

        private Camera _camera = null;

        #region UnityMEFs

        void Start()
        {
            _camera = Camera.main;
            var width = _width ? GetScreenToWorldWidth() : transform.localScale.x;
            var height = _height ? GetScreenToWorldHeight() : transform.localScale.y;
            transform.localScale = new Vector3(width, height, 1.0f);
        }

        #endregion

        #region MEFs

        private float GetScreenToWorldHeight()
        {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var height = edgeVector.y * 2;

                return height;
        }

        private float GetScreenToWorldWidth()
        {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = _camera.ViewportToWorldPoint(topRightCorner);
                var width = edgeVector.x * 2;

                return width;
        }

        #endregion
    }
}