using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed;
    public int Health = 100;

    private void Update()
    {
        EnemyMove();
    }

    public virtual void EnemyMove()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}