using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTruckManager : MonoBehaviour
{
    [Header("Numbers to Play With")]
    [SerializeField] float minimumTime = 15f;
    [SerializeField] float maximumTime = 25f;

    [Header("Inspector Fields")]
    [SerializeField] Animator trashAnimator;
    [SerializeField] GameObject trashIndicator;
    [SerializeField] TrashCollection trashArea;

    float currentTrashTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTrashTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartTrashTimer()
    {
        currentTrashTime = Random.Range(minimumTime, maximumTime);

        yield return new WaitForSeconds(currentTrashTime - 5f);
        trashIndicator.SetActive(true);
        StartCoroutine(StartLastFiveSeconds());
    }

    IEnumerator StartLastFiveSeconds()
    {
        trashAnimator.SetTrigger("Enter");
        yield return new WaitForSeconds(5f);
        trashIndicator.SetActive(false);
        trashArea.CollectTrash();
        trashAnimator.SetTrigger("Exit");
        StartCoroutine(StartTrashTimer());
    }
}
