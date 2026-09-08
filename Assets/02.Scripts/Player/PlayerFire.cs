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
        // 2. 총알 프리팹을 생성한다.
        // Instantiate는 프리팹을 복사해서 (Monobehaviour를 상속받는)게임 오브젝트를 생성하고 씬에 넣어주는 기능
        GameObject leftBullet = Instantiate(BulletPrefab);
        leftBullet.transform.position = LeftFirePoint.position; // 생성한 총알의 위치를 총구의 위치로

        GameObject rightBullet = Instantiate(BulletPrefab);
        rightBullet.transform.position = RightFirePoint.position; // 생성한 총알의 위치를 총구의 위치로

        GameObject subBulletRight = Instantiate(SubBulletPrefab);
        GameObject subBulletLeft = Instantiate(SubBulletPrefab);

        subBulletRight.transform.position = RightSubFirePointTransform.position;
        subBulletLeft.transform.position = LeftSubFirePointTransform.position;
    }
}