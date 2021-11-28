using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletController : MonoBehaviour
{
    private bool plunger;
    private bool pointsAdded;
    // Start is called before the first frame update
    void Start()
    {
        plunger = false;
        pointsAdded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(pointsAdded)
            return;
            
        if(plunger && !pointsAdded)
        {
            FindObjectOfType<ScoreManager>().AddScore(20f);
            pointsAdded = true;
        }

        if (other.gameObject.name == "Plunger")
        {
            plunger = true;
        }
    }
}
