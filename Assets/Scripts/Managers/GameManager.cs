using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Helper Scripts")]
    [SerializeField] TimerHelper timer;
    [Header("Game Variables")]
    [SerializeField] float timeForLevel;
    // Start is called before the first frame update
    void Start()
    {
        Pause();
        timer.StartTimer(timeForLevel);
        TimerHelper.OnTimerEnd += TimeDone;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }

    public void ShowIndicators() //TODO: Implement
    {

    }

    void TimeDone()
    {
        Debug.Log("Timer over");
        TimerHelper.OnTimerEnd -= TimeDone;
    }
}
