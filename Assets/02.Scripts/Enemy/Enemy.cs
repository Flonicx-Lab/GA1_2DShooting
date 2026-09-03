using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed;
    public float Health = 100;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}