using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    [Header("총알 프리팹들")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize;

    private Bullet[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 창고를 창고 크기만큼 만든다.
        _pool = new Bullet[_bulletPrefabs.Length, _poolSize];

        // 창고 크기만큼 총알을 미리 만들어서 집어넣는다.
        for (int i = 0; i < _pool.GetLength(0); i++)
        {
            Bullet bulletPrefab = _bulletPrefabs[i];

            for (int j = 0; j < _pool.GetLength(1); j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, gameObject.transform);
                bullet.gameObject.SetActive(false); // 당장 쓸것이 아니니 비활성화
                _pool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _pool.GetLength(0); i++)
        {
            if (_pool[i, 0].Type != bulletType)
            {
                continue;
            }

            for (int j = 0; j < _pool.GetLength(1); j++)
            {
                Bullet bullet = _pool[i, j];

                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}