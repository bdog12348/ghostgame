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

    public Color PlayerColor()
    {
        for(int i = 0; i < DataHolder.PlayerMaps.Count; i++)
        {
            if(DataHolder.Characters[i].ghostNumber == movementMode.GetPlayerNumber())
                return DataHolder.Characters[i].characterColor;
        }
        return new Color();
    }
}
