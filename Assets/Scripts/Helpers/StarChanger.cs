using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarChanger : MonoBehaviour
{
    [SerializeField] Image filledRenderer;
    [SerializeField] Image emptyRenderer;
    [SerializeField] TextMeshProUGUI scoreText;

    float threshold;

    public void SetFilled()
    {
        filledRenderer.enabled = true;
        emptyRenderer.enabled = false;
    }

    public void SetEmpty()
    {
        emptyRenderer.enabled = true;
        filledRenderer.enabled = false;
    }

    public void SetText(float score)
    {
        scoreText.text = score.ToString();
        threshold = score;
    }

    public float GetThreshold()
    {
        return threshold;
    }
}
