using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리
    // 필요 필드:
    public float Speed;
    
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수는 별다른 설정이 없을 경우 가능한 많이
    private void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f 
        float v = Input.GetAxisRaw("Vertical"); // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f //Raw 는 더 빠르게 
        
        Debug.Log($"h:{h}, v:{v}");
        
        // 2. 키보드 입력에 따라 방향을 구한다.
        // 게임에는 벡터라는 타입이 있다. 벡터는(크기와 방향을 의미한다.)
        Vector2 direction = new Vector2(h, v);
        
        // 3. 방향과 속도에 따라 이동한다.
        // 속도 = 방향 * 속력                        // 매직 넘버란: 보는 사람에 따라 의미가 달라질 수 있는 헷갈리는 숫자

        Vector2 normalizedSpeed = (direction * Speed).normalized; // 벡터 길이를 1로 만들어주는 것(방향은 유지)
        
        //transform.Translate(normalizedSpeed * Time.deltaTime);
        // deltaTime : 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 MS로 반환 (시간 / 프레임)
        
        // 새로운 위치 = 현재 위치 + v * t
        // transform.position +=  (Vector2)direction* normalizedSpeed * Time.deltaTime);

        Vector2 nextPosition = (Vector2)transform.position + normalizedSpeed * Time.deltaTime;

        if (nextPosition.x >= -2.36
            && nextPosition.x < 2.36
            && nextPosition.y >= -5.25
            && nextPosition.y < -0.6)
        {
            transform.position = nextPosition;
        }



    }
}
