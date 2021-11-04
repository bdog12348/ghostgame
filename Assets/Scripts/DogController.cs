using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteRenderer indicatorSpriteRenderer;

    [SerializeField] Sprite asleepIndicatorSprite;
    [SerializeField] Sprite alertIndicatorSprite;
    [SerializeField] Sprite asleepSprite;
    [SerializeField] Sprite alertSprite;

    float susAmount = 0;
    float maxSus = 100;
    float deltaAmount = 0.1f;

    bool ready = true;
    bool playerInRange = false;

    private void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer = transform.Find("StatusIndicator").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        indicatorSpriteRenderer.color = new Color(1, 1 - (susAmount / maxSus), 1 - (susAmount / maxSus));

        if (ready)
        {
            if (playerInRange)
            {
                susAmount += deltaAmount;
            }else
            {
                if (susAmount > 0)
                    susAmount -= deltaAmount;
            }
        }

        if (susAmount >= maxSus)
        {
            FindObjectOfType<ScoreManager>().AddScore(-10f);
            susAmount = 0f;
            spriteRenderer.sprite = alertSprite;
            indicatorSpriteRenderer.sprite = alertIndicatorSprite;
            ready = false;
            StartCoroutine(Cooldown());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        ready = true;
        spriteRenderer.sprite = asleepSprite;
        indicatorSpriteRenderer.sprite = asleepIndicatorSprite;
    }
}
