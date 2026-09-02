using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    
    // - 생성 위치(총구)
    public Transform FirePoint1;
    public Transform FirePoint2;
    public float FireCooltime;
    private float fireTimer = 0f;
    

    private void Update()
    {
        Fire();
        
    }

    private void Fire()
    {
        fireTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fireTimer > FireCooltime)
            {
                // 2. 총알 프리팹을 생성한다.
                // Instantiate는 프리팹을 복사해서 게임 오브젝트를 생성하고 씬에 넣어주는 기능
                GameObject bulletRight = Instantiate(BulletPrefab);
                GameObject bulletLeft = Instantiate(BulletPrefab);
            
                bulletRight.transform.position = FirePoint1.position; // 생성한 총알의 위치를 나(플레이어)의 위치로
                bulletLeft.transform.position = FirePoint2.position;

                fireTimer = 0f;
            }
            
        }
    }
}
