using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    float currentLoad = 0;
    [SerializeField] float maxLoad = 3;

    [SerializeField] SpriteChanger spriteChanger;

    [SerializeField] GameObject[] dustpanObjects;
    [SerializeField] GameObject[] roombaObjects;
    [SerializeField] AudioSource fillTrashSound;

    GameObject indicator;
    bool possessed = false;
    private Color colorShowing = new Color();

    void Start()
    {
        indicator = transform.GetChild(2).gameObject;
    }
    void Update()
    {
        bool newColor = false;
        foreach (GameObject obj in roombaObjects)
        {
            if(!CheckFull() && !obj.GetComponent<RoombaMovement>().Empty() && obj.GetComponent<PossessedStatus>().ObjectTaken())
            {
                colorShowing = obj.GetComponent<PossessedStatus>().PlayerColor();
                newColor = true;
            }
        }
        foreach (GameObject obj in dustpanObjects)
        {
            if(!CheckFull() && !obj.GetComponent<DustpanMovement>().Empty() && obj.GetComponent<PossessedStatus>().ObjectTaken())
            {
                colorShowing = obj.GetComponent<PossessedStatus>().PlayerColor();
                newColor = true;
            }
        }
        if(newColor)
        {
            indicator.SetActive(true);
            indicator.GetComponent<SpriteRenderer>().color = colorShowing;
        }
        else
        {
            indicator.SetActive(false);
        }
    }
    
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
            fillTrashSound.Play();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dirt"))
        {
            Dirt dirt = other.gameObject.GetComponent<Dirt>();
            if (dirt.GetVelocity() != Vector3.zero)
            {
                if (!CheckFull())
                {
                    if (dirt.GetSmall())
                        currentLoad += 0.5f;
                    
                    else
                        currentLoad++;
                    fillTrashSound.Play();
                    SetFillLevelSprite();
                    Destroy(other.gameObject);
                }
            }
        }else if  (other.gameObject.CompareTag("Leaf"))
        {
            Leaf leaf = other.gameObject.GetComponent<Leaf>();
            if (leaf.GetVelocity() != Vector3.zero)
            {
                if (!CheckFull())
                {
                    currentLoad++;
                    fillTrashSound.Play();
                    SetFillLevelSprite();
                    Destroy(other.gameObject);
                }
            }
        }

        
    }

}
