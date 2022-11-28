using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using UnityEngine.Networking;

public class ClickTimeTask : MonoBehaviour
{
    public float timer;
    public float timerWithDecimal;

    public int taskNumber;
    public int clicks;

    public bool taskStart;
    private bool _print = false;


    private void Start()
    {
        StartCoroutine(_isStart());
    }

    public void Update()
    {

        if (_print)
        {

            TimeTaskName();
            _print = false;
            Debug.Log("counter ended");
        }

        if (taskStart == true)
        {
            timer += Time.deltaTime;
            clicks = IndexPinchSelector.clicks;
        }

    }
    string path = "https://linguatopmaster.com/P3/php/startCounting.php";
    private IEnumerator _isStart()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (request.downloadHandler.text == "1")
            {
                if (!taskStart)
                {
                    timer = 0;
                    IndexPinchSelector.clicks = 0;
                    Debug.Log("counter started");
                }
                taskStart = true;
            }
            else
            {
                if (taskStart)
                {
                    _print = true;
                }
                taskStart = false;
            }

            StartCoroutine(_isStart());
        }

    }

    public void TimeTaskName()
    {
        timerWithDecimal = Mathf.Round(timer * 100.0f) * 0.01f;
        taskNumber++;
        Debug.Log("Task " + taskNumber + " / Time:" + timerWithDecimal + " / Clicks:" + clicks);
    }
}