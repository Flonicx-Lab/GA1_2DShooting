using UnityEngine;

public class EnemyThrough : Enemy
{
    public Transform PlayerTransform;
    private Vector2 _direction;

    private void Start()
    {
        _direction = (PlayerTransform.position - transform.position).normalized;
    }

    private void Update()
    {
        EnemyMove();
    }

    public override void EnemyMove()
    {
        transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }
}