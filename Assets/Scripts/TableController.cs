using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private bool fork;
    private bool knife;
    private bool spoon;
    private bool tablePointsAdded;
    // Start is called before the first frame update
    void Start()
    {
        fork = false;
        knife = false;
        spoon = false;
        tablePointsAdded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tablePointsAdded)
            return;

        if(fork && knife && spoon && !tablePointsAdded)
        {
            FindObjectOfType<ScoreManager>().AddScore(20f);
            tablePointsAdded = true;
        }

        if (other.gameObject.name == "Fork")
        {
            fork = true;
        }
        if (other.gameObject.name == "Knife")
        {
            knife = true;
        }
        if (other.gameObject.name == "Spoon")
        {
            spoon = true;
        }
    }
}
