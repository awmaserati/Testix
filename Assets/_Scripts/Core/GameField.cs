using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Testix.Utils;
using Testix.Configs;

namespace Testix.Core
{
    public class GameField : Singleton<GameField>
    {
        internal event Action<int> OnPlayerHPChanged = null;

        [SerializeField]
        private GameObject _playerPrefab = null;
        [SerializeField]
        private Vector3 _playerStartPosition = Vector3.zero;
        [SerializeField]
        private Transform _fieldParent = null;
        [SerializeField]
        private EnemyFactory _factory = null;
        [SerializeField]
        private List<Transform> _spawnPoints = null;
        [SerializeField]
        private GameflowConfig _config = null;

        private int _enemiesCount = 0;
        private int _lastSPIndex = -1;
        private PlayerView _player = null;
        private List<EnemyViewBase> _enemies = new List<EnemyViewBase>();
        private int _killedEnemiesCount = 0;
        private GameResult _result = GameResult.Default;

        #region UnityMEFs

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion

        #region MEFs

        internal void CreateField()
        {
            _result = GameResult.Default;
            CreatePlayer();
            StartCoroutine(CreateEnemies());
        }

        private void CleanField()
        {
            _lastSPIndex = -1;
            _player.gameObject.transform.localPosition = _playerStartPosition;
            _player.Clean();
            Destroy(_player.gameObject);

            foreach(var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }

            _enemies.Clear();
        }

        private void CreatePlayer()
        {
            var go = Instantiate(_playerPrefab, _fieldParent);
            go.transform.localPosition = _playerStartPosition;
            _player = go.GetComponent<PlayerView>();
            _player.OnDead += GameOver;
            _player.OnHPChanged += OnHPChanged;
            _player.Init(new PlayerModel(GameSettings.Instance.Config.Player));
        }

        private IEnumerator CreateEnemies()
        {
            _enemiesCount = UnityEngine.Random.Range(_config.EnemiesCount.Min, _config.EnemiesCount.Max + 1);
            var enemiesSpawned = 0;

            while(enemiesSpawned < _enemiesCount)
            {
                var typeIndex = UnityEngine.Random.Range(0, _config.EnemyTypes.Count);
                var enemyGO = _factory.GetEnemy(_config.EnemyTypes[typeIndex]);
                var enemyView = enemyGO.GetComponent<EnemyViewBase>();
                enemyView.OnKilled += CheckWin;
                enemyView.OnDead += OnEnemyDeadLine;

                _enemies.Add(enemyView);
                var spIndex = 0;

                do
                {
                    spIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
                    enemyGO.transform.SetParent(_spawnPoints[spIndex]);
                    enemyGO.transform.localPosition = Vector3.zero;
                }
                while (spIndex == _lastSPIndex);

                _lastSPIndex = spIndex;
                var cd = UnityEngine.Random.Range(_config.EnemiesSpawnCD.Min, _config.EnemiesSpawnCD.Max);
                enemiesSpawned++;

                yield return new WaitForSeconds(cd);
            }
        }

        private void GameOver()
        {
            _result = GameResult.Lose;
            StopAllCoroutines();
            CleanField();
            Game.Instance.EndGame(GameResult.Lose);
        }

        private void OnHPChanged(int hp)
        {
            OnPlayerHPChanged?.Invoke(hp);
        }

        private void CheckWin()
        {
            _killedEnemiesCount++;

            if(_killedEnemiesCount == _enemiesCount)
            {
                StopAllCoroutines();
                CleanField();
                Game.Instance.EndGame(GameResult.Win);
            }
        }

        private void OnEnemyDeadLine(int damage)
        {
            _player.GetDamage(damage);

            if(_result == GameResult.Default)
            {
                CheckWin();
            }
        }

        #endregion
    }
}