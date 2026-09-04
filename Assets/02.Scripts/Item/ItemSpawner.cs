using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;


    public void SpawnItem(Vector2 pos)
    {
        // 30% 랜덤생성
        int prob = Random.Range(0, 100);
        int itemPrefabIndex = 0;

        if (prob < 33)
        {
            itemPrefabIndex = 0;
        }
        else if (prob < 66)
        {
            itemPrefabIndex = 1;
        }
        else
        {
            itemPrefabIndex = 2;
        }

        Item item = Instantiate(_itemPrefabs[itemPrefabIndex], pos, Quaternion.identity);
    }
}