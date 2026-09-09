using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;

    private float _bombTimer = 0f;

    private void Update()
    {
        _bombTimer -= Time.deltaTime;

        if (_bombTimer < 0f)
        {
            FireBomb();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log($"재사용 대기시간 {_bombTimer}");
            }
        }
    }

    private void FireBomb()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            _bombTimer = 10f;
        }
    }
}