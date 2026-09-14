using UnityEngine;
using UnityEngine.Pool; //유니티 공식 Pool

public class ItemPool : MonoBehaviour
{
    private static ItemPool _instance;
    public static ItemPool Instance => _instance;

    [Header("아이템 프리팹 3종류 (Heal=0, MoveSpeedUp=1, FireRateUp=2)")]
    [SerializeField] private Item[] _itemPrefabs;

    // 종류별 공식 풀 배열
    private IObjectPool<Item>[] _pools;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 1. 프리팹 종류 수만큼 풀 보관함 배열 생성
        _pools = new IObjectPool<Item>[_itemPrefabs.Length];

        // 2. 종류별로 순회하며 전용 공식 풀 생성 후 배열에 등록
        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            Item targetPrefab = _itemPrefabs[i];

            _pools[i] = new ObjectPool<Item>(
                createFunc: () => Instantiate(targetPrefab, transform),
                actionOnGet: (item) => item.gameObject.SetActive(true),
                actionOnRelease: (item) => item.gameObject.SetActive(false),
                actionOnDestroy: (item) =>
                {
                    if (item != null) Destroy(item.gameObject);
                },
                collectionCheck: true,
                defaultCapacity: 5,
                maxSize: 20
            );
        }
    }

    // 이넘을 인덱스 번호(0, 1, 2)로 변환하여 꺼내기
    public Item GetItem(ItemType type)
    {
        int index = (int)type;

        if (index >= 0 && index < _pools.Length)
        {
            return _pools[index].Get();
        }

        return null;
    }

    // 이넘을 인덱스 번호(0, 1, 2)로 변환하여 반납하기
    public void ReleaseItem(Item bullet)
    {
        int index = (int)bullet.Type;

        if (index >= 0 && index < _pools.Length)
        {
            _pools[index].Release(bullet);
        }
    }
}