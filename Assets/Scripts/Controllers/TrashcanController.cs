using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    float currentLoad = 0;
    [SerializeField] float maxLoad = 3;

    [SerializeField] SpriteChanger spriteChanger;
    bool possessed = false;

    /// <summary>
    /// Adds specified amount of trash to current load and returns left over amount, if any
    /// </summary>
    /// <param name="amount">Amount of trash to add to trashcan</param>
    /// <returns>Amount of trash that could not be stored</returns>
    public float AddTrash(float amount)
    {
        float leftoverAmount = 0;

        if (currentLoad + amount > maxLoad)
        {
            leftoverAmount = (currentLoad + amount) - maxLoad;
            currentLoad = maxLoad;
        }else
        {
            currentLoad = currentLoad + amount;
        }

        SetFillLevelSprite();
        return leftoverAmount;
    }
    public float GetCurrentLoad()
    {
        return currentLoad;
    }

    public bool CheckFull()
    {
        return currentLoad == maxLoad;
    }

    public bool PartiallyFull()
    {
        return (currentLoad > 0) && !CheckFull();
    }

    public void Empty()
    {
        currentLoad = 0;
        SetFillLevelSprite();
    }
    public void SetPossessed()
    {
        possessed = true;
    }
    public void ResetPosession()
    {
        possessed = false;
    }

    void SetFillLevelSprite()
    {
        int fillLevel = 0;
        if(CheckFull())
            fillLevel = 2;
        else if(PartiallyFull())
            fillLevel = 1;

        if (possessed)
        {
            if(fillLevel == 0)
                spriteChanger.SetPlayerSprite(0);
            else
                spriteChanger.SetFilledSprite(fillLevel + 2);
        }
        else
        {
            spriteChanger.SetFilledSprite(fillLevel);
        }
    }

}
