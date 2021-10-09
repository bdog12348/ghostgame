using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject interactObject;
    [SerializeField]
    private Sprite ghostSprite;

    private GameObject possessableObject = null;

    private MovementMode movementMode;
    private GhostMovement ghostMovement;
    private SpriteRenderer spriteRenderer;

    private bool CanInteract = false;
    private bool Possessing = false;

    List<float> inputs;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ghostMovement = gameObject.AddComponent<GhostMovement>();
        ghostMovement.AutoAsssignCharacterController();
        movementMode = ghostMovement;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementMode();

        if (Input.GetButtonDown("Possess") && Possessing)
        {
            ResetGhost();
        }

        if (CanInteract && !Possessing)
        {
            interactObject.SetActive(true);
            if (Input.GetButtonDown("Possess"))
            {
                Possess();
            }
        }
        else
        {
            interactObject.SetActive(false);
        }
    }

    /// <summary>
    /// Gets inputs and sends those for respective movement mode
    /// </summary>
    private void HandleMovementMode()
    {
        if (movementMode != null)
        {
            inputs = movementMode.GetInputs();
        }

        if (inputs != null)
        {
            movementMode.Move(inputs);
        }
    }

    /// <summary>
    /// Sets possessing to true and changes sprites to interacted object's sprite.
    /// Also changes movement mode to whichever movement mode is on the object and changes character controller to ghosts.
    /// </summary>
    void Possess()
    {
        Possessing = true;
        CanInteract = false;
        spriteRenderer.sprite = possessableObject.GetComponentInChildren<SpriteRenderer>().sprite;
        movementMode = possessableObject.GetComponent<MovementMode>();
        movementMode.SetCharacterController(ghostMovement.GetCharacterController());
        possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    /// <summary>
    /// Sets ghost to not possessing and resets sprites
    /// </summary>
    void ResetGhost()
    {
        Possessing = false;
        spriteRenderer.sprite = ghostSprite;
        possessableObject.transform.position = transform.position;
        possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        possessableObject = null;

        movementMode.SetCharacterController(null);
        movementMode = ghostMovement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Possessable")
        {
            CanInteract = true;
            possessableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Possessable")
        {
            CanInteract = false;
            if (!Possessing)
                possessableObject = null;
        }
    }
}
