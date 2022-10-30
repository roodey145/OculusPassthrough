using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class TableSurface : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_countDownText;
    [SerializeField]
    private Transform m_rightFinger;
    [SerializeField]
    private float m_handMovementThreshold = 2f;
    private Vector3 _lastHandPosition;
    public float tableSurfaceY
    {
        private set;
        get;
    }
    private bool _surfaceLevelSet;

    void Start()
    {
#if !UNITY_EDITOR
        Assert.IsNotNull(m_rightFinger);
        StartCoroutine(SaveSurfaceCountDown());
#endif
#if UNITY_EDITOR
        _surfaceLevelSet = true;
        tableSurfaceY = 5;
#endif
    }

    private void Update()
    {
        if (_surfaceLevelSet) return;
        transform.position = m_rightFinger.transform.position;
    }

    private void SaveTableSurface()
    {
        tableSurfaceY = transform.position.y;
        _surfaceLevelSet = true;
    }

    private IEnumerator SaveSurfaceCountDown()
    {
        const float duration = 20f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            m_countDownText.text = $"Table Saved in: {Mathf.FloorToInt(normalizedTime * duration)}";
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        SaveTableSurface();
    }
}
