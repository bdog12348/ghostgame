using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
{

    List<TrashcanController> trashcans;

    ScoreManager scoreManager;

    [SerializeField] GameObject[] trashCans;
    private Color colorShowing = new Color();
    GameObject indicator;



    // Start is called before the first frame update
    void Start()
    {
        trashcans = new List<TrashcanController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        indicator = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        bool newColor = false;
        foreach (GameObject trashcan in trashCans)
        {
            if(trashcan.GetComponent<PossessedStatus>().ObjectTaken() && trashcan.GetComponent<TrashcanController>().GetCurrentLoad() > 0)
            {
                colorShowing = trashcan.GetComponent<PossessedStatus>().PlayerColor();
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

    public void CollectTrash()
    {
        foreach(TrashcanController trashcan in trashcans)
        {
            float trashAmount = trashcan.GetCurrentLoad();
            trashcan.Empty();
            scoreManager.AddScore(trashAmount * 20);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().PossessingTrash())
        {
            trashcans.Add(other.gameObject.GetComponent<PlayerController>().GetPossessedTrashController());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().PossessingTrash())
        {
            trashcans.Remove(other.gameObject.GetComponent<PlayerController>().GetPossessedTrashController());
        }
    }
}
