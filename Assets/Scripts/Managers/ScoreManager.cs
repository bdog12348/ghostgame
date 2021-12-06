using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] AudioSource pointsGet;
    [SerializeField] AudioSource pointsLost;

    float score = 0;

    public void AddScore (float amount)
    {
        if(amount > 0f)
            pointsGet.Play();
        else
            pointsLost.Play();
        
        score += amount;
        scoreTMP.text = score.ToString();
    }

    public float GetScore()
    {
        return score;
    }
}
