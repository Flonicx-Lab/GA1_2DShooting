using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _health = 100;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        // 응집도는 높히고, 결합도는 낮춰라
        // 결합도란 묻는 것
        _health -= damage;
        if (_health <= 0)
        {
            // 너 죽자!
            Destroy(gameObject);
        }
    }
}