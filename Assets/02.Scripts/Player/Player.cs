using UnityEngine;

public class Player : MonoBehaviour
{
    // 캡슐화
    // 데이터 은닉
    // 메서드를 통한 상태 변경
    [SerializeField] private int _health = 100;
    [SerializeField] private GameObject _deatheffectPrefab;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hitSound;
    private AudioSource _audioSource;

    // 프로퍼티 문법

    public int Health
    {
        get => _health;
        set => _health = value;
    } // 람다식 문법을 활용한 읽기 전용 프로퍼티
    // public int Health
    // {
    //     get { return _health; }
    // }
    // Set은 외부에서 수정해버리니 잘 안쓴다 메서드로 만들어 쓴다.
    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드

    // getter/setter : 특정 데이터를 get/set 해주는 메서드
    public int GetHealth()
    {
        return _health;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deatheffectPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position);
    }


    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("대미지는 음수일 수 없습니다.");
            return;
        }

        _health -= damage;
        if (_health <= 0)
        {
            SpawnDeathSound();
            SpawnDeathEffect();
            Destroy(gameObject);
        }

        else
        {
            if (_audioSource != null && _hitSound != null)
            {
                _audioSource.PlayOneShot(_hitSound); // PlayOneShot
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("힐량은 음수일 수 없습니다.");
            return;
        }

        _health += healAmount;
    }
}