using UnityEngine;

public class DownwardEnemy : Enemy
{
    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}