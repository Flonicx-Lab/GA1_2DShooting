using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _deceleration = 0.5f;

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
            Destroy(other.gameObject);
            Explode();
        }
    }

    private void Explode()
    {
        _moveSpeed = 0f;
        _animator.SetTrigger("Explode");
        GetComponent<CircleCollider2D>().radius = 1.5f;
        Destroy(gameObject, 3f);
    }
}