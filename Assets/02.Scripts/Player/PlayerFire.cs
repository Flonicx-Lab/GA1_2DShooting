using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    
    // - 생성 위치(총구)
    public Transform RightFirePointTransform;
    public Transform LeftFirePointTransform;
    public Transform RightSubFirePointTransform;
    public Transform LeftSubFirePointTransform;
    
    // 타이머
    public float FireCooltime;
    private float fireTimer = 0f;
    
    // 자동 토글
    public bool AutoFireMode = false;

    private void Update()
    {
        Fire();
        ToggleAutoFireMode();
    }
    
    // 오토 Fire 토글
    public void ToggleAutoFireMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AutoFireMode = !AutoFireMode;
        }
    }
    
    // 총알 발사
    private void Fire()
    {
        fireTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) || AutoFireMode)
        {
            if (fireTimer > FireCooltime)
            {
                // 2. 총알 프리팹을 생성한다.
                // Instantiate는 프리팹을 복사해서 게임 오브젝트를 생성하고 씬에 넣어주는 기능
                GameObject bulletRight = Instantiate(BulletPrefab);
                GameObject bulletLeft = Instantiate(BulletPrefab);
                GameObject subBulletRight = Instantiate(SubBulletPrefab);
                GameObject subBulletLeft = Instantiate(SubBulletPrefab);
            
                
                // 생성한 총알의 위치를 나(플레이어)의 위치로
                bulletRight.transform.position = RightFirePointTransform.position;
                bulletLeft.transform.position = LeftFirePointTransform.position;
                subBulletRight.transform.position = RightSubFirePointTransform.position;
                subBulletLeft.transform.position = LeftSubFirePointTransform.position;
                
                // 타이머 초기화
                fireTimer = 0f;
            }
            
        }
    }
}
