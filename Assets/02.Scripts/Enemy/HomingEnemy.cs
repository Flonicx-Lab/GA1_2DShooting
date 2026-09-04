using UnityEngine;

public class HomingEnemy : Enemy
{
    private GameObject _player;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        _player = GameObject.FindWithTag("Player");
        // 방향 설정
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        // 2. 방향과 속도에 맞게 이동한다.
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}