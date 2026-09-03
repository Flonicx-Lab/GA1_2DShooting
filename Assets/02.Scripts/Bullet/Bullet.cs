using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;


    private void Update()
    {
        Vector2 direction = Vector2.up; // new Vector2(1,0);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}