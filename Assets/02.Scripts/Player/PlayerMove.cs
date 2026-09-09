using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적: 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.

    // 필요 필드:
    private Animator _animator;

    [SerializeField] private float _speed;
    public float Speed => _speed; // 프로퍼티 문법
    public float MaxPositionY;
    public float MinPositionY;
    public float MaxPositionX;
    public float MinPositionX;

    [SerializeField] private float _maxMoveSpeed;

    private TrailRenderer[] _trailRenderers;

    // 객체가 생성될 때 한 번 실행 된다.
    private void Awake()
    {
        // 애니메이터 컴포넌트에 대한 참조를 가져와서 할당한다.
        _animator = GetComponent<Animator>();
        _trailRenderers = GetComponentsInChildren<TrailRenderer>();
    }

    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는: 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        Move();

        SpeedChange();
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SpeedUp(float upValue)
    {
        if (upValue < 0)
        {
            Debug.LogWarning("속도 증가량은 0보다 작을 수 없습니다.");
            return;
        }

        _speed += upValue;

        if (_speed > _maxMoveSpeed)
        {
            _speed = _maxMoveSpeed;
        }
    }

    private void SpeedChange()
    {
        // 7. Q/E 버튼 입력을 통한 스피드 업/다운
        if (Input.GetKeyDown(KeyCode.E))
        {
            _speed++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _speed--;
        }
    }

    private void Move()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        // 
        _animator.SetInteger("x", (int)normalizedDirection.x);


        // 3. 방향과 속력에 따라 이동한다.
        Vector2 newPosition = transform.position + (Vector3)normalizedDirection * _speed * Time.deltaTime;

        // 4. 위치 y에 제한이 있다.
        float minY = Mathf.Min(MinPositionY, MaxPositionY);
        float maxY = Mathf.Max(MinPositionY, MaxPositionY);
        if (newPosition.y > maxY)
        {
            newPosition.y = maxY;
        }
        else if (newPosition.y < minY)
        {
            newPosition.y = minY;
        }

        // 5. 양 옆 끝으로 가면 반대쪽 방향으로 순간이동 (워프)
        float minX = Mathf.Min(MinPositionX, MaxPositionX);
        float maxX = Mathf.Max(MinPositionX, MaxPositionX);
        if (newPosition.x > maxX)
        {
            transform.position = new Vector2(minX, newPosition.y);
            ClearAllTrails();
            return;
        }
        else if (newPosition.x < minX)
        {
            transform.position = new Vector2(maxX, newPosition.y);
            ClearAllTrails();
            return;
        }

        transform.position = newPosition;
    }

    // 트레일 경계면 늘어짐 버그 수정용
    private void ClearAllTrails()
    {
        for (int i = 0; i < _trailRenderers.Length; i++)
        {
            _trailRenderers[i].Clear();
        }
    }
}