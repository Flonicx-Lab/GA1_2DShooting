using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 _originalPosition;

    [SerializeField] private static float _cameraDuration = 0.15f;
    public static float CameraDuration => _cameraDuration;

    [SerializeField] private static float _cameraMagnitude = 0.2f;
    public static float CameraMagnitude => _cameraMagnitude;

    private void Awake()
    {
        Instance = this;
        _originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = _originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = _originalPosition.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, _originalPosition.z); //카메라라서 z축이 필수

            elapsed += Time.unscaledDeltaTime; // unscaled는 실제 현실시간 (timescale에 영향받지 않음)
            yield return null;
        }

        transform.localPosition = _originalPosition;
    }
}