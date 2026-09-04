using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // 나와 충돌한 다른 게임 오브젝트는 파괴
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}