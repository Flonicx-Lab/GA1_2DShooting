using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBalanceDataTable", menuName = "Scriptable Objects/EnemyBalanceDataTable")]
public class EnemyBalanceDataTableSO : ScriptableObject
{
    public EnemyBalanceData[] Datas;
}