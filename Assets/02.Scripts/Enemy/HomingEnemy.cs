using UnityEngine;

public class HomingEnemy : Enemy
{
    // 캐싱 : 자주 쓸법한 데이터(객체)를 가져온 곳에 지정해두고 쓰는 것

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        if (_player == null) return;

        Vector2 diff = _player.transform.position - transform.position;
        float degree = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, degree + 90);

        Vector2 _direction = new Vector2(diff.x, diff.y).normalized;
        transform.Translate(_direction * _moveSpeed * Time.deltaTime, Space.World);
    }
}