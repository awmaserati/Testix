using System.Collections.Generic;
using UnityEngine;

namespace Testix.Core
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _enemyPrefabs = null;

        #region MEFs

        internal GameObject GetEnemy(EnemyType type)
        {
            return Instantiate(_enemyPrefabs[(int)type]);
        }

        #endregion
    }
}