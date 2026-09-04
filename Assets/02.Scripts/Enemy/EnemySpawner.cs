using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;
    [SerializeField] private Enemy _enemyPrefab;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            _spawnInterval = Random.Range(1f, 3f);

            Spawn();
        }
    }

    private void Spawn()
    {
        if (_enemyPrefab == null) return;
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity); //깜빡임 현상 방지, 위치와 회전값
        enemy.transform.position = transform.position;
    }
}