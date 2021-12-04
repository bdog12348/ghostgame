using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BroomPowerHelper : MonoBehaviour
{
    [Header("Inspector Fields")]
    [SerializeField] GameObject sliderGO;
    Slider slider;

    float maxPower = 1f;
    float currentPower = 0.005f;
    float deltaAmount = .5f;
    float multipier = 2f;

    bool detectPower = false;

    // Start is called before the first frame update
    void Start()
    {
        slider = sliderGO.GetComponent<Slider>();
        slider.maxValue = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPower)
        {
            if (currentPower >= maxPower || currentPower <= 0)
            {
                if (currentPower < 0)
                    currentPower = 0;
                else if (currentPower > maxPower)
                    currentPower = maxPower;

                multipier *= -1f;
            }

            currentPower += deltaAmount * multipier * Time.deltaTime;
            slider.value = currentPower;
        }
    }

    public float GetPower()
    {
        return currentPower;
    }

    public void Activate()
    {
        sliderGO.SetActive(true);
        detectPower = true;
    }

    public float DeactivateWithReturn()
    {
        float cachedPower = currentPower;
        Deactivate();
        return cachedPower;
    }

    public void Deactivate()
    {
        sliderGO.SetActive(false);
        detectPower = false;
        currentPower = 0f;
        multipier = 2f;
    }
}
