using Unity.Mathematics;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float _itemSpeed;
    [SerializeField] float _itemFreezeTime;
    protected GameObject _player;
    private float _timer = 0f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        ItemMove();
    }

    protected void ItemMove()
    {
        _timer += Time.deltaTime;

        if (_timer >= _itemFreezeTime)
        {
            if (_player == null) return;
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            transform.Translate(direction * _itemSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}