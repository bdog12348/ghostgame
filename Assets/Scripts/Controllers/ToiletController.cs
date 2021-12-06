using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletController : MonoBehaviour
{
    [SerializeField] GameObject[] plungerObjects;
    private bool plunger;
    private bool pointsAdded;
    private GameObject indicator;
    private Color colorShowing = new Color();



    // Start is called before the first frame update
    void Start()
    {
        plunger = false;
        pointsAdded = false;
        indicator = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(pointsAdded)
        {
            indicator.SetActive(false);
            return;
        }
        bool newColor = false;
        foreach (GameObject plunger in plungerObjects)
        {
            if(plunger.GetComponent<PossessedStatus>().ObjectTaken())
            {
                colorShowing = plunger.GetComponent<PossessedStatus>().PlayerColor();
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
