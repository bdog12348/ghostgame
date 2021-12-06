using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private bool fork;
    private bool knife;
    private bool spoon;
    private bool tablePointsAdded;

    private Color colorShowing = new Color();

    [SerializeField] GameObject[] utensils;
    GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        fork = false;
        knife = false;
        spoon = false;
        tablePointsAdded = false;
        indicator = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(tablePointsAdded)
        {
            indicator.SetActive(false);
            return;
        }
        bool newColor = false;
        foreach (GameObject utensil in utensils)
        {
            if(utensil.GetComponent<PossessedStatus>().ObjectTaken())
            {
                colorShowing = utensil.GetComponent<PossessedStatus>().PlayerColor();
                newColor = true;
            }
        }
        if(newColor)
        {
            indicator.SetActive(true);
            indicator.GetComponent<SpriteRenderer>().color = colorShowing;
        }
        else
        {
            indicator.SetActive(false);
        }
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
