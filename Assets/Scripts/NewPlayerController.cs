using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject interactObject;
    [SerializeField]
    private Sprite ghostSprite;

    public GameObject possessableObject;
    public GameObject currentObject;
    public GameObject originalGhost;

    private SpriteRenderer spriteRenderer;
    
    public float moveSpeed = 5.0f;

    float horizontal;
    float vertical;

    private bool canInteract = false;
    private bool possessing = false;

    public void MoveInput(float horizontal, float vertical, bool forward, int direction)
    {
        if(currentObject.name == "Player")
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }
        else if(currentObject.name == "Broom")
        {
            this.horizontal = horizontal / 2;
            this.vertical = vertical / 2;
        }
        else if(currentObject.name == "Roomba")
        {
            if(forward)
                this.horizontal = (float)0.1;
            if(direction == 2) //left
                this.vertical = (float)0.1;
            else if(direction == 1) //right
                this.vertical = (float)-0.1;
        }
        else
        {
            this.horizontal = horizontal / 2;
            this.vertical = vertical / 2;
        }
        
    }

    public void OnPossessAttempt()
    {
        // If we are not currently possessing, and we can, then do so
        if(possessing == false && possessableObject != null)
        {
            currentObject = possessableObject;
            possessing = true;
            interactObject.SetActive(false);
            spriteRenderer.sprite = possessableObject.GetComponentInChildren<SpriteRenderer>().sprite;
            possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            Debug.Log($"Posessing {currentObject.name}");
        }
        else if(possessing == true)
        {
            currentObject = originalGhost;
            spriteRenderer.sprite = ghostSprite;
            possessableObject.transform.position = transform.position;
            possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            Debug.Log($"Going Ghost{currentObject.name}");
            possessing = false;
        }
    }

    void Start()
    {
        currentObject = gameObject;
        originalGhost = gameObject;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!possessing && other.gameObject.tag == "Possessable")
        {
            possessableObject = other.gameObject;
            interactObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Possessable")
        {
            if(!possessing)
            {
                possessableObject = null;
                interactObject.SetActive(false);
            } 
        }
    }
    
}
