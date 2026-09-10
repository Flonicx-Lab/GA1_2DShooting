using Unity.VisualScripting;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _ignoreDistance = 1.5f;
    private Enemy[] _enemies;
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Enemy target = FindBestTarget();

        if (target != null)
        {
            MoveTowardsTargetX(target.transform.position.x);
        }
    }

    private void MoveTowardsTargetX(float targetX)
    {
        float CurrentX = _player.transform.position.x;
        float newX = Mathf.MoveTowards(CurrentX, targetX, _moveSpeed * Time.deltaTime);

        _player.transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // 최적의 타겟을 찾는다.
    // 타겟의 거리가 너무 가까우면 안된다.
    // 그러나 가장 가까운 타겟을 찾는다.
    // 타겟들의 거리를 계산한다.


    private Enemy FindBestTarget()
    {
        _enemies = FindObjectsOfType<Enemy>();
        Enemy bestTarget = null;
        float distance;
        float minDistance = float.MaxValue;

        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] != null)
            {
                distance = (_player.transform.position - _enemies[i].transform.position).magnitude;

                if (distance <= _ignoreDistance) continue;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestTarget = _enemies[i];
                }
            }
        }

        return bestTarget;
    }
}