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
    protected Sprite defaultSprite;

    protected SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = SpriteRenderer.sprite;
    }

    public void SetDefaultSprite()
    {
        if(SpriteRenderer != null)
            SpriteRenderer.sprite = defaultSprite;
    }

    public void SetPlayerSprite(int playerNumber)
    {
        if(SpriteRenderer != null)
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
