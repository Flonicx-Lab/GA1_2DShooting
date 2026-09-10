using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 -> 전역적으로 접근 가능, 단 하나의 인스턴스
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성,읽기,수정,삭제 등과 관련된 로직

    private int _bestScore;
    private int _currentScore = 0;
    private bool _isscoreDirty = false; // 더티 플래그 선언

    // 저장 키
    private const string SaveKey = "BestScore";

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

        _instance = this;
    }

    private void Start()
    {
        // 입력: Input.
        // 저장/불러오기: PlayerPrefs
        // Get,Set,Has,Save 시리즈만 외우면 됨
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }

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

        _isscoreDirty = true;
    }

    private void LateUpdate() // 마무리청소!
    {
        if (_isscoreDirty)
        {
            // 텍스트 갱신
            Refresh();

            // 디스크 세이브
            // 저장: Set~ 시리즈를 이용해서 int/float/string을 저장 가능하다.
            // 내 컴퓨터 어딘가에 저장이 된다.
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();

            // 깃발 내림 (무거운 연산 끝)
            _isscoreDirty = false;
        }
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"Best Score: {_bestScore}";
        _currentScoreTextUI.text = $"Score: {_currentScore}";
    }
}