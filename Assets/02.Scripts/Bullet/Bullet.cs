using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;
    [SerializeField] private int _bulletDamage;

    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1,0);
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    //트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 나 죽고!
        Destroy(this.gameObject);

        // 충돌한 친구가 Enemy일때만 죽여보자!
        if (other.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(_bulletDamage);
        }
    }

    //충돌 관련 이벤트 (Enter -> Stay -> Exit)

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("충돌 중이다!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("충돌 끝났다!");
    }
}