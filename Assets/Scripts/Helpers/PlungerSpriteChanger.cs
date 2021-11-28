using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerSpriteChanger : SpriteChanger
{
    [SerializeField] Sprite downPlayerSprite;
    private PlungerMovement plungerMovement;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = SpriteRenderer.sprite;
        plungerMovement = gameObject.transform.parent.GetComponent<PlungerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if(plungerMovement.GetPlayerNumber() != -1 && plungerMovement.Charging())
            SpriteRenderer.sprite = downPlayerSprite;
        else if(plungerMovement.GetPlayerNumber() != -1)
            SetPlayerSprite(plungerMovement.GetPlayerNumber());
    }

}
