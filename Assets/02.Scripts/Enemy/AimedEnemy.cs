using UnityEngine;

public class AimedEnemy : Enemy
{
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player != null)
        {
            Debug.Log("플레이어 태그를 찾지 못했습니다.");
        }

        _direction = _player.transform.position - transform.position;
        _direction.Normalize();
    }

    protected override void Move()
    {
        if (_player != null)
            // 2. 방향과 속도에 맞게 이동한다.
            transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}