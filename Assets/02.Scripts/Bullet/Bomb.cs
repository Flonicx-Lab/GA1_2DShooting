using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _deceleration = 0.5f;
    [SerializeField] private float _hitStopTime;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_moveSpeed > 0f)
        {
            _moveSpeed -= Time.deltaTime * _deceleration;

            if (_moveSpeed <= 0f)
            {
                _moveSpeed = 0f;
                Explode();
            }

            Vector2 direction = Vector2.up;
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("충돌 시점 속도: " + _moveSpeed + " / 정지시간: " + _hitStopTime);
            // 직격타만 역경직 발동
            if (_moveSpeed > 0f)
            {
                StartCoroutine(HitStopRoutine(_hitStopTime));
            }

            Destroy(other.gameObject);
            Explode();
        }
    }

    private void Explode()
    {
        _moveSpeed = 0f;
        _animator.SetTrigger("Explode");
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(CameraShake.CameraDuration, CameraShake.CameraMagnitude);
        }

        GetComponent<CircleCollider2D>().radius = 1.5f;
        Destroy(gameObject, 3f);
    }

    // 시간 정지 및 복구를 담당하는 코루틴 함수
    private IEnumerator HitStopRoutine(float duration)
    {
        Time.timeScale = 0f; // 엔진 전체의 시간정지
        yield return new WaitForSecondsRealtime(duration); // 현실 세계 초 측정
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}