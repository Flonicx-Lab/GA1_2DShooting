using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject _player;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _health = 100;
    [SerializeField] protected int _defaultDamage = 30; // 값 자체는 숨기기
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

    private void ONTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();

        if (player != null) return;

        player.TakeDamage(_defaultDamage);
        Destroy(gameObject);
    }
}