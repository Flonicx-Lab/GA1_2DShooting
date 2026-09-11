using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    // - 생성 위치(총구)
    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    public Transform RightSubFirePointTransform;
    public Transform LeftSubFirePointTransform;

    // - 쿨타이머 (공속)
    [SerializeField] private float _coolTime = 0.5f;
    [SerializeField] private float _coolTimer = 0;
    public float CoolTimeAtkSpeed => _coolTime;
    private float _maxAtkSpeed = 0.1f;

    // - 오토 모드
    public bool AutoFireMode = false;

    private void Start()
    {
        _coolTimer = _coolTime;
    }

    private void Update()
    {
        // 오토 공격 모드 토글
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }

        // 0. 쿨타이머 감소
        _coolTimer -= Time.deltaTime;

        // 1. 쿨타이머가 0초 이하이고 && (스페이스바를 누르거나 || 오토 모드라면)
        if (_coolTimer <= 0 && (Input.GetKeyDown(KeyCode.Space) || AutoFireMode))
        {
            // 2. 발사
            Fire();

            // 3. 쿨타이머 초기화
            _coolTimer = _coolTime;
        }
    }

    public void AtkSpeedUp(float upValue)
    {
        if (upValue <= 0.05f)
        {
            Debug.LogWarning("공격속도 증가량은 0보다 작을 수 없습니다.");
            return;
        }

        _coolTime -= upValue;

        if (_coolTime < _maxAtkSpeed)
        {
            _coolTime = _maxAtkSpeed;
        }
    }

    private void Fire()
    {
        Bullet leftBullet = BulletPool.Instance.GetBullet(BulletType.Main);
        leftBullet.transform.position = LeftFirePoint.position;

        Bullet rightBullet = BulletPool.Instance.GetBullet(BulletType.Main);
        rightBullet.transform.position = RightFirePoint.position;

        Bullet subBulletRight = BulletPool.Instance.GetBullet(BulletType.Sub);
        Bullet subBulletLeft = BulletPool.Instance.GetBullet(BulletType.Sub);

        subBulletRight.transform.position = RightSubFirePointTransform.position;
        subBulletLeft.transform.position = LeftSubFirePointTransform.position;
    }
}