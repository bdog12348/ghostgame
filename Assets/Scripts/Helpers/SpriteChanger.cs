using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour
{
    [Header("Player Sprites")]
    [SerializeField] Sprite[] playerSprites;

    [Header("Others")]
    [SerializeField] Sprite[] filledSprites;
    Sprite defaultSprite;

    SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = SpriteRenderer.sprite;
    }

    public void SetDefaultSprite()
    {
        SpriteRenderer.sprite = defaultSprite;
    }

    public void SetPlayerSprite(int playerNumber)
    {
        SpriteRenderer.sprite = playerSprites[playerNumber];
    }

    public void SetFilledSprite(int level)
    {
        if (level == 0)
        {
            SetDefaultSprite();
        }
        else if (level == 1)
        {
            SpriteRenderer.sprite = filledSprites[0];
        }else if (level == 2)
        {
            SpriteRenderer.sprite = filledSprites[1];
        }
    }
}
