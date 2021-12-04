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
    [SerializeField] TrashTruckIndicatorHelper truckIndicatorHelper;

    readonly float TIME_TO_COLLECT_TRASH = 5f;
    float currentTrashTime;
    float trashColletingTime;
    bool setTrashCollectingTime = false;
    bool indicatorOut = false;
    bool truckGoing = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTrashTime = Random.Range(minimumTime, maximumTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Paused) return;

        if (currentTrashTime > 0)
        {
            currentTrashTime -= Time.deltaTime;
            if (currentTrashTime < 5f && !indicatorOut)
            {
                indicatorOut = true;
                trashIndicator.SetActive(true);
                truckIndicatorHelper.StartTimer();
            }
        } else
        {
            if (!truckGoing)
            {
                trashAnimator.SetTrigger("Enter");
                trashColletingTime = TIME_TO_COLLECT_TRASH;
                setTrashCollectingTime = true;
                truckGoing = true;
            }
        }

        if (trashColletingTime > 0)
        {
            trashColletingTime -= Time.deltaTime;
        }
        else
        {
            if (setTrashCollectingTime)
            {
                currentTrashTime = Random.Range(minimumTime, maximumTime) + 5f;
                trashIndicator.SetActive(false);
                trashArea.CollectTrash();
                trashAnimator.SetTrigger("Exit");
                indicatorOut = false;
                setTrashCollectingTime = false;
                truckGoing = false;
            }
        }
    }
}
