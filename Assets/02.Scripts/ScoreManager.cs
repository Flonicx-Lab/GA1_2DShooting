using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 -> 전역적으로 접근 가능, 단 하나의 인스턴스
    public static ScoreManager Instance { get; private set; }

    // 관리 : 특정 데이터에 대한 무결성과 생성,읽기,수정,삭제 등과 관련된 로직

    private int _bestScore;
    private int _currentScore;

    // UI 책임 추가
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;


    private void Awake()
    {
        // 하나 이상은 금지
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return; // 방어코드 (매개변수를 받을 때)

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"Best Score: {_bestScore}";
        _currentScoreTextUI.text = $"Score: {_currentScore}";
    }
}