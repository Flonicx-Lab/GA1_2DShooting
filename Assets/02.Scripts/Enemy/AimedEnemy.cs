using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class AimedEnemy : Enemy
{
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("플레이어 태그를 찾지 못했습니다.");
        }

        // 1. 플레이어 방향 각도 계산
        float dx = _player.transform.position.x - transform.position.x;
        float dy = _player.transform.position.y - transform.position.y;
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // 분자 먼저 인자로 받음 , 라디안 -> 각도로 변경

        // 2. 머리가 플레이어를 향하도록 회전 (아래를 향하고있어 기준 3시를 맞추기 위해 +90도 보정)
        transform.rotation = Quaternion.Euler(0f, 0f, degree + 90f); // 2D는 Z축 회전만 있음
    }

    protected override void Move()
    {
        if (_player == null) return;

        // 3. 이동처리
        transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime, Space.Self);
    }
}