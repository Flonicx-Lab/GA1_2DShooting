using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격

    [Header("on/off 스프라이트")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private Image _myImage;
    private Player _player;

    private bool _autoMode = false;

    private void Start()
    {
        _myImage = GetComponent<Image>();
        _player = FindFirstObjectByType<Player>();
        AutoToggle();
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;

        if (_player != null)
        {
            _player.GetComponent<PlayerFire>().SetAuto(_autoMode);
            _player.GetComponent<PlayerMove>().enabled = !_autoMode;
            _player.GetComponent<PlayerAutoMove>().enabled = _autoMode;
        }

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}