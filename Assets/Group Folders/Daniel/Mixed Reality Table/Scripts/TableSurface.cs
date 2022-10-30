using System.Collections;
using System.Linq;
using UnityEngine;
using Oculus.Interaction.Surfaces;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

public class TableSurface : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _countDownText;
    [SerializeField]
    private Transform _rightFinger;
    [SerializeField]
    private float _handMovementThreshold = 2f;
    private Vector3 _lastHandPosition;
    public float tableSurfaceY
    {
        private set;
        get;
    }
    private bool _surfaceLevelSet;

    void Start()
    {
        Assert.IsNotNull(_rightFinger);
        StartCoroutine(SaveSurfaceCountDown());
    }

    private void Update()
    {
        if (_surfaceLevelSet) return;
        transform.position = _rightFinger.transform.position;
    }

    private bool IsHandMoving()
    {
        return (_rightFinger.transform.position - _lastHandPosition).magnitude > _handMovementThreshold;
    }

    public void SaveTableSurface()
    {
        tableSurfaceY = transform.position.y;
        _surfaceLevelSet = true;
    }

    private IEnumerator SaveSurfaceCountDown()
    {
        float duration = 20f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            _countDownText.text = $"Table Saved in: {Mathf.FloorToInt(normalizedTime * duration)}";
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        SaveTableSurface();
    }
}
