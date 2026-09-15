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
            if (SimpleInput.GetButton("Bomb"))
            {
                Debug.Log($"재사용 대기시간 {_bombTimer}");
            }
        }
    }

    private void FireBomb()
    {
        if (SimpleInput.GetButton("Bomb"))
        {
            Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            _bombTimer = 10f;
        }
    }
}