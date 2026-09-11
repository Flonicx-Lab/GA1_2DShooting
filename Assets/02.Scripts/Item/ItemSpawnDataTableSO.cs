using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpawnDataTableSO", menuName = "Scriptable Objects/ItemSpawnDataTableSO")]
public class ItemSpawnDataTableSO : ScriptableObject
{
    [SerializeField] private ItemSpawnData[] _datas;
    public ItemSpawnData[] Datas => _datas;
}