using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject interactObject;
    [SerializeField]
    private Sprite ghostSprite;
    [SerializeField]
    private Sprite humanSprite;

    public GameObject possessableObject;
    public GameObject currentObject;
    public GameObject originalGhost;

    private SpriteRenderer spriteRenderer;
    Rigidbody rb;
    
    public float moveSpeed = 5.0f;

    float horizontal;
    float vertical;

    private bool canInteract = true;
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
            this.horizontal = horizontal * 2;
            this.vertical = vertical * 2;
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
        if(possessing == false && possessableObject != null && canInteract)
        {
            currentObject = possessableObject;
            possessing = true;
            interactObject.SetActive(false);
            ChangeSprites(possessableObject.GetComponentInChildren<SpriteRenderer>().sprite);
            possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            Debug.Log($"Posessing {currentObject.name}");
        }
        else if(possessing == true)
        {
            currentObject = originalGhost;
            ChangeSprites(ghostSprite);
            possessableObject.transform.position = transform.position;
            possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            Debug.Log($"Going Ghost{currentObject.name}");
            possessing = false;
        }
    }

    public void SwapForms()
    {
        if (canInteract) //is a ghost
        {
            ChangeSprites(humanSprite);
            canInteract = false;
            moveSpeed = 8;
        }
        else
        {
            ChangeSprites(ghostSprite);
            canInteract = true;
            moveSpeed = 5;
        }
    }

    void Start()
    {
        currentObject = gameObject;
        originalGhost = gameObject;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * vertical + Vector3.right * horizontal);
        Vector3 moveVel = moveDirection * moveSpeed;
        rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!possessing && other.gameObject.tag == "Possessable" && canInteract)
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
    Vector3 FixMovementForCamera(Vector3 moveVector)
    {
        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;
        camF.y = 0;
        camR.y = 0;
        camF.Normalize();
        camR.Normalize();
        Vector3 velVector = ((camF * moveVector.z + camR * moveVector.x) * Time.deltaTime) / Time.deltaTime;
        return velVector;
    }

    void ChangeSprites(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        GetComponent<BoxCollider>().size = new Vector3(spriteSize.x / 2, spriteSize.y / 4, .5f);
        GetComponent<BoxCollider>().center = new Vector3(0, -spriteSize.y / 4, 0);
    }
}
