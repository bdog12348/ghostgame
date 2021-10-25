using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    EnemySpotBehaviour[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<EnemySpotBehaviour>();
        foreach(EnemySpotBehaviour i in enemies)
        {
            i.SpottedList.OnValueChangedTo += TargetsChanged;
        }
    }

    void TargetsChanged(List<Transform> playersSpotted)
    {
        Debug.Log("Oh hey the targets changed");
        foreach(Transform t in playersSpotted)
        {
            Debug.Log(t.name);
        }
        if (playersSpotted.Count <= 0) return; // No players seen

        Debug.LogFormat("First player seen: {0}", playersSpotted[0]);
    }
}
