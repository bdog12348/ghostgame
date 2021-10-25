using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

[RequireComponent(typeof(MoreMountains.Tools.MMConeOfVision))]
public class EnemySpotBehaviour : MonoBehaviour
{
    public MMObservable<List<Transform>> SpottedList = new MMObservable<List<Transform>>();
    MMConeOfVision vision;

    // Start is called before the first frame update
    void Start()
    {
        vision = GetComponent<MMConeOfVision>();
    }

    // Update is called once per frame
    void Update()
    {
        SpottedList.Value = vision.VisibleTargets;
    }
}
