using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // 업그레이드 관리자: 업그레이드들에 대한 무결성과 생성,조회,수정,삭제 등과 관련한 게임 로직.

    private static UpgradeManager _instance;
    public static UpgradeManager Instance => _instance;

    // 업그레이드 도메인 클래스들
    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    // 업그레이드 UI들
    [SerializeField] private UI_Upgrade[] _uiUpgrades;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        Load();

        RefreshUI();
    }

    public void LevelUp(int index)
    {
        // Todo: 묻지말고 시켜라

        Upgrade upgrade = _upgrades[index];

        // 스코어 매니저와 협력
        if (ScoreManager.Instance.Score < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        upgrade.LevelUp();

        Save();

        RefreshUI();
    }

    // UI 갱신
    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }

    private void Save()
    {
        // 데이터 저장은 유의미한 정보만 저장한다.
        // 그래서 레벨만 저장
        for (int i = 0; i < _upgrades.Length; i++)
        {
            int level = PlayerPrefs.GetInt($"Upgrade.{i}Level", 1);
            _upgrades[i].SetLevel(level);
        }

        PlayerPrefs.Save();
    }

    private void Load()
    {
    }
}