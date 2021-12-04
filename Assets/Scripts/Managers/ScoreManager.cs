using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTMP;

    float score = 0;

    public void AddScore (float amount)
    {
        score += amount;
        scoreTMP.text = score.ToString();
    }

    public float GetScore()
    {
        return score;
    }
}
