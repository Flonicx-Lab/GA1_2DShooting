using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;
    [SerializeField] private Enemy[] _enemyPrefabs;

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
        // 50% [0] Downward
        // 30% [1] Aimed
        // 20% [2] Homing

        int enemyPrefabIndex = 0;
        int randomPercent = UnityEngine.Random.Range(0, 100);

        //Todo: Scriptable Object를 사용해서 리팩토링
        // 이유 1: 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
        // 이유 2: 각 애너미 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵다.
        if (randomPercent < 50)
        {
            enemyPrefabIndex = 0;
        }
        else if (randomPercent < 80)
        {
            enemyPrefabIndex = 1;
        }
        else
        {
            enemyPrefabIndex = 2;
        }

        Enemy enemy = Instantiate(_enemyPrefabs[enemyPrefabIndex], transform.position, Quaternion.identity);
    }
}