using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
{

    List<TrashcanController> trashcans;

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        trashcans = new List<TrashcanController>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
            Debug.Log("Trash Added");
            trashcans.Add(other.gameObject.GetComponent<PlayerController>().GetPossessedTrashController());
        }
        else
        {
            Debug.Log($"Something else entered! {other.gameObject.tag}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().PossessingTrash())
        {
            Debug.Log("Trash Removed");
            trashcans.Remove(other.gameObject.GetComponent<PlayerController>().GetPossessedTrashController());
        }
    }
}
