using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Helper Scripts")]
    [SerializeField] TimerHelper timer;
    [Header("Game Variables")]
    [SerializeField] float timeForLevel;
    // Start is called before the first frame update
    void Start()
    {
        timer.StartTimer(timeForLevel);
        TimerHelper.OnTimerEnd += TimeDone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TimeDone()
    {
        Debug.Log("Timer over");
        TimerHelper.OnTimerEnd -= TimeDone;
    }
}
