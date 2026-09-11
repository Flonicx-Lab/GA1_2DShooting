using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private BulletType _type;
    public BulletType Type => _type;

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private int _bulletDamage;
    public int BulletDamage => _bulletDamage;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSpawn()
    {
        // 프리팹이 풀에 의해서 활성화 될 때마다
        // 초기화 하는 코드들이 들어간다.
        PlaySound();
    }

    public void PlaySound()
    {
        if (_audioSource != null)
        {
            Debug.Log("Bullet 오디오 활성화");
            _audioSource.pitch = UnityEngine.Random.Range(1f, 2f);
            _audioSource.Play();
        }
    }

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1,0);
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    //트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 친구가 Enemy일때만 죽여보자!
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(_bulletDamage);
        }
    }

    //충돌 관련 이벤트 (Enter -> Stay -> Exit)

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("충돌 중이다!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("충돌 끝났다!");
    }
}