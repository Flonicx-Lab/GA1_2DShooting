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
            // 심화 과제 1. 퍼사드 패턴 (패턴: 객체지향에서 자주 일어나는 설계 문제를 잘 풀어내도록 경험에 의해 정리해논 공식)
            // 심화 과제 2. 아이템 종류가 조합에의해 폭발적으로 증가할 경우에는 -> 조합 패턴을 사용해라.
            // 포트폴리오에서 가장 중요한게 게임 구현 완성도 (코드의 완성도는 가장 후순위)
            // - 게임 개발
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