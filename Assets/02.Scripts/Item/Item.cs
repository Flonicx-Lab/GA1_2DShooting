using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private float _healValue;
    [SerializeField] private float _moveSpeedUpValue;
    [SerializeField] private float _atkSpeedUpValue;

    private const float WaitTime = 2f;
    private float _waitTimer = 0f;
    private const float MoveSpeed = 5f;

    private Player _player = null;


    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player == null)
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
        if (_player == null)
        {
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            transform.Translate(direction * _moveSpeedUpValue * Time.deltaTime);
        }
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
                    break;
                }

            case ItemType.MoveSpeedUp:
                {
                    player.GetComponent<PlayerMove>().SpeedUp(_moveSpeedUpValue);
                    break;
                }

            case ItemType.FireRateUp:
                {
                    // todo: 속성을 직접 수정하는게 아니라 메서드를 통한 수정
                    player.GetComponent<PlayerFire>().AtkSpeedUp(_atkSpeedUpValue);
                    break;
                }
        }

        Destroy(gameObject);
    }
}