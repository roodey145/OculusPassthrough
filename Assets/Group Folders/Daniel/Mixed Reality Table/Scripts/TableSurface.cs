using System;
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
    private Vector3 _lastHandPosition;
    public float tableSurfaceY
    {
        private set;
        get;
    }
    public bool surfaceLevelSet
    {
        private set;
        get;
    }

    void Start()
    {
        StartCoroutine(SaveSurfaceCountDown());
    }

    private void Update()
    {
        if (surfaceLevelSet) return;
        tableSurfaceY = m_rightFinger.transform.position.y;
    }

    private void SaveTableSurface()
    {
        tableSurfaceY = transform.position.y;
        surfaceLevelSet = true;
        m_countDownText.gameObject.SetActive(false);
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
