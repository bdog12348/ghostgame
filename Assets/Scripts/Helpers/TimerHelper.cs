using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerHelper : MonoBehaviour
{
    bool TimerEnabled = false;

    TextMeshProUGUI timerText;
    Image fillImage;

    float remainingTime;
    float initialTime;

    public static Action OnTimerEnd = delegate { };

    private void Awake()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        fillImage = transform.Find("Fill").GetComponent<Image>();
    }

    private void Update()
    {
        if (!TimerEnabled || GameManager.Paused) return;

        timerText.text = remainingTime.ToString("0");
        fillImage.fillAmount = remainingTime / initialTime;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            OnTimerEnd();
        }
    }

    public void SetTime(float time)
    {
        initialTime = time;
        remainingTime = initialTime;
        timerText.text = initialTime.ToString("0");
    }

    public void StartTimer()
    {
        TimerEnabled = true;
    }
}
