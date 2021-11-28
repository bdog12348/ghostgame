using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomSpriteChanger : SpriteChanger
{
    [Header("Bent Player Sprites")]
    [SerializeField] Sprite[] bentPlayerSprites;

    private BroomMovement broomMovement;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = SpriteRenderer.sprite;
        broomMovement = gameObject.transform.parent.GetComponent<BroomMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(broomMovement.GetPlayerNumber() != -1 && !broomMovement.StoringPower())
        {
            SetPlayerSprite(broomMovement.GetPlayerNumber());
        }
        else if(broomMovement != null && broomMovement.HoldingLeft())
            SetLeftPlayerSprite();
        else if(broomMovement != null && broomMovement.HoldingRight())
            SetRightPlayerSprite();
    }
    public void SetLeftPlayerSprite()
    {
        SpriteRenderer.sprite = bentPlayerSprites[broomMovement.GetPlayerNumber()];
        SpriteRenderer.flipX = true;
    }

    public void SetRightPlayerSprite()
    {
        SpriteRenderer.sprite = bentPlayerSprites[broomMovement.GetPlayerNumber()];
        SpriteRenderer.flipX = false;
    }

}
