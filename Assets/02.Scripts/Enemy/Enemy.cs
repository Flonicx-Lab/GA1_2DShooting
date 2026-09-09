using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject _player;
    private Animator _animator;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _health = 100;
    [SerializeField] protected int _defaultDamage = 30; // 값 자체는 숨기기
    public int Damage => _defaultDamage; // 데미지 자체는 public으로

    [SerializeField] protected Item[] _itemPrefabs;

    // 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deatheffectPrefab;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
            EnemyDie();
        }
        else
        {
            if (_animator != null)
            {
                _animator.SetTrigger("isHit");
            }
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deatheffectPrefab, transform.position, Quaternion.identity); // death 이펙트
    }

    private void SpawnItem()
    {
        if (Random.Range(0, 100) > 30) return;

        Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position,
            Quaternion.identity);
    }

    public void EnemyDie()
    {
        SpawnItem();
        SpawnDeathEffect();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();

        if (player == null)
        {
            Debug.Log("플레이어가 null입니다.");
            return;
        }

        player.TakeDamage(_defaultDamage);
        Destroy(gameObject);
    }
}