using UnityEngine;

public class DownwardEnemy : Enemy
{
    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (_player != null) return;

        Vector2 direction = Vector2.down;
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}