using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    float currentLoad = 0;
    [SerializeField] float maxLoad = 3;

    [SerializeField] SpriteChanger spriteChanger;

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

    public void Empty()
    {
        currentLoad = 0;
        SetFillLevelSprite();
    }

    void SetFillLevelSprite()
    {
        if (currentLoad == maxLoad)
        {
            spriteChanger.SetFilledSprite(1);
        }else
        {
            spriteChanger.SetFilledSprite(0);
        }
    }

}
