using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteRenderer indicatorSpriteRenderer;
    
    [Header("Numbers to Play With")]
    [SerializeField] float deltaAmount = 1f;
    [SerializeField] float maxSus = 10;

    [Header("Inspector Fields")]
    [SerializeField] Animator dogAnimator;
    [SerializeField] Sprite asleepIndicatorSprite;
    [SerializeField] Sprite alertIndicatorSprite;
    [SerializeField] Sprite asleepSprite;
    [SerializeField] Sprite alertSprite;

    [SerializeField] AudioSource dogBark;

    float susAmount = 0;

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
                susAmount += Time.deltaTime * deltaAmount;
            }else
            {
                if (susAmount > 0)
                    susAmount -= Time.deltaTime * deltaAmount;
            }
        }

        if (susAmount >= maxSus)
        {
            dogBark.Play();
            FindObjectOfType<ScoreManager>().AddScore(-10f);
            dogAnimator.SetBool("Sleeping", false);
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
        dogAnimator.SetBool("Sleeping", true);

    }
}
