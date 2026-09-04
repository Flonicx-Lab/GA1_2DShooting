using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _health = 100;
    public bool isHit = false;
    [SerializeField] private int _defaultDamage = 30; // 값 자체는 숨기기
    public int Damage => _defaultDamage; // 데미지 자체는 public으로

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ContactPlayer()
    {
        Destroy(gameObject);
    }
}