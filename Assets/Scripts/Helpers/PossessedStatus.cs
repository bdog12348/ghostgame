using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedStatus : MonoBehaviour
{
    [SerializeField] MovementMode movementMode;

    public bool ObjectTaken()
    {
        return movementMode.ObjectTaken();
    }
}
