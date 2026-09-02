using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리
    // 필요 필드:
    public float Speed;
    
    public float leftBoundary;
    public float rightBoundary;
    public float upBoundary;
    public float downBoundary;

    private void SpeedUp()
    {
        Speed ++;
    }

    private void SpeedDown()
    {
        Speed ++;
    }

    public void Replay()
    {
        
    }

    public void Record()
    {
        
    }
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        Move();
        SpeedChange();
    }

    private void SpeedChange()
    {
        // 스피드업 다운 (E/Q)
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpeedUp();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpeedDown();
        }
    }

    private void Move()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f 
        float v = Input.GetAxisRaw("Vertical"); // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f //Raw 는 더 빠르게 
        
        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 direction = new Vector2(h, v);
        Vector2 normalizedDirection = direction.normalized;
        
        // 3. 방향과 속도에 따라 이동한다.
        Vector2 speed = normalizedDirection * Speed;
        Vector2 nextPosition = (Vector2)transform.position + speed * Time.deltaTime;
        
        // 좌 우 경계에서 등장
        if (nextPosition.x > rightBoundary)
        {
            nextPosition.x = leftBoundary;
        }
        else if (nextPosition.x < leftBoundary)
        {
            nextPosition.x = rightBoundary;
        }
        
        // 위 아래 막아두기
        if (nextPosition.y > downBoundary
            && nextPosition.y < upBoundary)
        {
            transform.position = nextPosition;
        }
    }
}
