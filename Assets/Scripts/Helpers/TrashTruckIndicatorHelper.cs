using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashTruckIndicatorHelper : MonoBehaviour
{
    [SerializeField] TrashTruckManager truckManager;
    [SerializeField] Image indicatorFill;

    float timeToWait = 5f;
    float currentTime = 0f;

    bool timerStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            if (currentTime < timeToWait)
            {
                currentTime += Time.deltaTime;
            } else
            {
                timerStarted = false;
            }
        }

        indicatorFill.fillAmount = currentTime / timeToWait;
    }

    public void StartTimer()
    {
        timerStarted = true;
        currentTime = 0f;
    }
}
