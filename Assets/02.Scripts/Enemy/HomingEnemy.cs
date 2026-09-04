using UnityEngine;

public class HomingEnemy : Enemy
{
    // 캐싱 : 자주 쓸법한 데이터(객체)를 가져온 곳에 지정해두고 쓰는 것
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        // 방향 설정
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        // 2. 방향과 속도에 맞게 이동한다.
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}