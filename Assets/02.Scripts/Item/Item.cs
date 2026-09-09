using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _healValue;
    [SerializeField] private float _moveSpeedUpValue;
    [SerializeField] private float _atkSpeedUpValue;

    private const float WaitTime = 0.5f;
    private float _waitTimer = 0f;
    private const float MoveSpeed = 5f;

    private Player _player;
    private AudioSource _itemAudioSource;

    [SerializeField] private GameObject _itemEffectPrefab;

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.GetComponent<Player>();
            _itemAudioSource = this.GetComponent<AudioSource>();
        }


        if (_player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다.");
            return;
        }
    }

    private void Update()
    {
        _waitTimer += Time.deltaTime;
        if (_waitTimer >= WaitTime)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }
    }

    private void SpawnItemEffect(Vector2 spawnPosition)
    {
        Instantiate(_itemEffectPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogWarning("플레이어 태그 오브젝트에 플레이어 컴포넌트가 없습니다.");
            return;
        }

        switch (_type)
        {
            case ItemType.Heal:
                {
                    player.Heal((int)(_healValue));
                    SpawnItemEffect(other.transform.position);
                    Debug.Log($"플레이어 체력: {player.Health}");
                    break;
                }

            case ItemType.MoveSpeedUp:
                {
                    PlayerMove playerMove = other.GetComponent<PlayerMove>();
                    playerMove.SpeedUp(_moveSpeedUpValue);
                    SpawnItemEffect(other.transform.position);
                    Debug.Log($"플레이어 이동속도 : {playerMove.Speed}");
                    break;
                }

            case ItemType.FireRateUp:
                {
                    PlayerFire playerFire = other.GetComponent<PlayerFire>();
                    playerFire.AtkSpeedUp(_atkSpeedUpValue);
                    SpawnItemEffect(other.transform.position);
                    Debug.Log($"플레이어 공속 : {playerFire.CoolTimeAtkSpeed}");
                    break;
                }
        }

        Destroy(gameObject);
    }
}