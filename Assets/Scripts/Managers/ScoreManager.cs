using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] AudioSource pointsGet;

    float score = 0;

    public void AddScore (float amount)
    {
        pointsGet.Play();
        score += amount;
        scoreTMP.text = score.ToString();
    }

    public float GetScore()
    {
        return score;
    }
}
