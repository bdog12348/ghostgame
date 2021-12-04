using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField] StarChanger[] stars;
    [SerializeField] TextMeshProUGUI evaluationText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject failImage;
    [SerializeField] GameObject successImage;
    [SerializeField] ScoreManager scoreManager;

    public float[] scoreThresholds;

    // Start is called before the first frame update
    public void LoadScores()
    {
        for(int i = 0; i < 3; i++)
        {
            stars[i].SetText(scoreThresholds[i]);
            if (scoreManager.GetScore() >= stars[i].GetThreshold())
            {
                stars[i].SetFilled();
            }else
            {
                stars[i].SetEmpty();
            }
        }

        if (scoreManager.GetScore() < scoreThresholds[0])
        {
            evaluationText.text = "Failed!";
            failImage.SetActive(true);
        }
        else
        {
            evaluationText.text = "Cleared!";
            successImage.SetActive(true);
        }

        scoreText.text = scoreManager.GetScore().ToString();
    }
}
