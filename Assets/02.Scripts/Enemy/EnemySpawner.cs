using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // csv 텍스트 파일 참조
    [SerializeField] private EnemySpawnDataTableSO _enemySpawnDataTable;

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
        if (_enemySpawnDataTable == null || _enemySpawnDataTable.Datas == null ||
            _enemySpawnDataTable.Datas.Length == 0)
        {
            Debug.Log("적 스폰 데이터 테이블이 연결되지 않았습니다.");
            return;
        }
        // 가중치 랜덤 선택 알고리즘
        // 각 아이템에 가중치를 부여하고, 가중치가 클수록 높은 확률로 선택되도록 하는 방식

        // 1. 추첨할 수 있는 모든 가중치를 더한다.
        int totalWeight = 0;
        foreach (EnemySpawnData data in _enemySpawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;
        foreach (EnemySpawnData data in _enemySpawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}