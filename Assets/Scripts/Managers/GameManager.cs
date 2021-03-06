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
    [Header("GameObjects")]
    [SerializeField] GameObject readyImage;
    [SerializeField] GameObject goImage;
    [SerializeField] GameObject timeUpImage;
    [SerializeField] GameObject scoreScreenGO;
    [SerializeField] AudioSource whistle;

    public static bool Paused = false;

    float readyTimer;
    float goTimer;
    float timeUpTimer;
    bool readyTimerSet, goTimerSet, timeUpTimerSet, scoreScreenShowing;

    // Start is called before the first frame update
    void Start()
    {
        timer.SetTime(timeForLevel);
        TimerHelper.OnTimerEnd += TimeDone;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(scoreScreenShowing && Input.GetKeyDown(KeyCode.R))
            Restart();

        HandleTimers();
    }

    void HandleTimers()
    {
        if (readyTimer > 0)
        {
            readyTimer -= Time.deltaTime;
        } else
        {
            if (readyTimerSet)
            {
                readyImage.SetActive(false);
                goImage.SetActive(true);
                whistle.Play();
                goTimer = .75f;
                goTimerSet = true;
                readyTimerSet = false;
            }
        }

        if (goTimer > 0)
        {
            goTimer -= Time.deltaTime;
        }else
        {
            if (goTimerSet)
            {
                goImage.SetActive(false);

                Paused = false;
                goTimerSet = false;
            }
        }

        if (timeUpTimer > 0)
        {
            timeUpTimer -= Time.deltaTime;
        }else
        {
            if (timeUpTimerSet)
            {
                timeUpImage.SetActive(false);
                scoreScreenGO.SetActive(true);
                scoreScreenShowing = true;
                GetComponent<ScoreScreenManager>().LoadScores();
                timeUpTimerSet = false;
            }
        }
    }

    public void StartTimer()
    {
        timer.StartTimer();
    }

    public void StartGame()
    {
        readyImage.SetActive(true);
        readyTimer = 1.5f;
        readyTimerSet = true;
    }

    public void Pause()
    {
        Paused = true;
    }

    public void Unpause()
    {
        Paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowIndicators() //TODO: Implement
    {

    }

    void TimeDone()
    {
        TimerHelper.OnTimerEnd -= TimeDone;
        whistle.Play();
        timeUpImage.SetActive(true);
        timeUpTimer = 1.25f;
        timeUpTimerSet = true;
    }
}
