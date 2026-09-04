using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;
    [SerializeField] private Enemy _enemyDownwardPrefab;
    [SerializeField] private Enemy _enemyAimedPrefab;
    [SerializeField] private Enemy _enemyHomingPrefab;

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
        float random = Random.Range(0f, 1f);

        Enemy enemyPrefab;

        if (random > 0.5f)
        {
            enemyPrefab = _enemyDownwardPrefab;
        }
        else if (random < 0.3f)
        {
            enemyPrefab = _enemyAimedPrefab;
        }
        else
        {
            enemyPrefab = _enemyHomingPrefab;
        }

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}