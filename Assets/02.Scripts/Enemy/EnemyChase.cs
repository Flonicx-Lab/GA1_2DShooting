using UnityEngine;

public class EnemyChase : Enemy
{
    public Transform PlayerTransform;

    private void Update()
    {
        EnemyMove();
    }

    public override void EnemyMove()
    {
        Vector2 direction = (PlayerTransform.position - transform.position).normalized;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}