using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private bool fork;
    private bool knife;
    private bool spoon;
    private bool tablePointsAdded;

    [SerializeField] GameObject[] utensils;
    SpriteRenderer tableSprite;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        fork = false;
        knife = false;
        spoon = false;
        tablePointsAdded = false;
        tableSprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        originalColor = tableSprite.color;
    }

    void Update()
    {
        if(!tablePointsAdded)
            ColorObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tablePointsAdded)
            return;

        if(fork && knife && spoon && !tablePointsAdded)
        {
            FindObjectOfType<ScoreManager>().AddScore(20f);
            tablePointsAdded = true;
            tableSprite.color = originalColor;
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

    private void ColorObject()
    {
        // If a player is inhabiting a fork, knife, or spoon, and the points have not been given, highlight
        //  the table based on the player

        bool found = false;
        foreach (GameObject gameObject in utensils)
        {
            if (gameObject.GetComponent<UtensilMovement>().GetPlayerNumber() != -1)
            {
                tableSprite.color = new Color(originalColor.r, originalColor.g, 0f, originalColor.a);
                found = true;
            }
        }
        if (!found)
            tableSprite.color = originalColor;
    }
}
