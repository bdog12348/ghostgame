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

    // Update is called once per frame
    void Update()
    {

    }

    public void CollectTrash()
    {
        Debug.Log("Collecting Trash");
        foreach(TrashcanController trashcan in trashcans)
        {
            int trashAmount = trashcan.GetCurrentLoad();
            trashcan.Empty();
            scoreManager.AddScore(trashAmount * 20);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            trashcans.Add(other.gameObject.GetComponent<TrashcanController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            trashcans.Remove(other.gameObject.GetComponent<TrashcanController>());
        }
    }
}
