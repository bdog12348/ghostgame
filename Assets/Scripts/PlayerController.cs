using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] GameObject interactIndicatorObject;
    [SerializeField] GameObject ghostObject;
    [SerializeField] GameObject humanObject;
    [SerializeField] Sprite ghostSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] public int playerJoystick;
    [SerializeField] Animator spriteAnimator;
    [SerializeField] SpriteRenderer[] spriteRenderers;
    #endregion

    #region Private Fields
    GameObject possessableObject = null;
    GameObject currentlyPossessedObject = null;
    GameObject draggingObject = null;
    GameObject interactObject = null;

    MovementMode movementMode;
    PlayerMovement ghostMovement;

    bool GameOver = false;
    bool CanInteract = false;
    bool Possessing = false;
    bool IsHuman = false;
    bool isDraggingObject = false;

    List<float> inputs;
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        ghostMovement = GetComponent<PlayerMovement>();
        ghostMovement.AutoAsssignRigidbody();
        ghostMovement.SetPlayerNumber(playerJoystick);
        movementMode = ghostMovement;
        TimerHelper.OnTimerEnd += () => GameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver) return; // Disable script?

        if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 1") && !Possessing)
          || (playerJoystick == 1 && Input.GetKeyDown(KeyCode.Space) && !Possessing && !isDraggingObject))
        {
            ToggleHuman();
        }

        if (CanInteract)
        {
            if (Possessing) // On goal object
            {
                interactIndicatorObject.SetActive(true);
                if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 2") || (playerJoystick == 1 && Input.GetKeyDown("f"))))
                {
                    movementMode.InteractWithObject(interactObject);
                    CanInteract = false;
                    return;
                }
            }
            else if (!Possessing && !IsHuman) // On possessable object
            {
                interactIndicatorObject.SetActive(true);
                if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))))
                {
                    Possess();
                    return;
                }
            }else if (IsHuman) // On an object only human form can interact with; currently only trash can so is going to be hard coded
            {
                interactIndicatorObject.SetActive(true);
                if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))))
                {
                    if (!isDraggingObject)
                    {
                        isDraggingObject = true;
                        draggingObject = interactObject;
                        draggingObject.transform.SetParent(transform);
                        CanInteract = false;
                    }
                    return;
                }
            }
        }
        else
        {
            interactIndicatorObject.SetActive(false);
        }

        if (IsHuman && isDraggingObject)
        {
            if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))))
            {
                isDraggingObject = false;
                draggingObject.transform.SetParent(transform.parent);
                draggingObject = null;
                CanInteract = false;
            }
        }

        if (movementMode != null)
        {
            inputs = movementMode.GetInputs();
        }

        if (inputs != null)
        {
            // Do animation stuff
            if (inputs[0] != 0 || inputs [1] != 0)
            {
                if (IsHuman)
                    spriteAnimator.SetBool("Moving", true);
            }else
            {
                if (IsHuman)
                    spriteAnimator.SetBool("Moving", false);
            }

            if (inputs[0] < 0)
            {
                foreach(SpriteRenderer renderer in spriteRenderers)
                {
                    renderer.flipX = true;
                }

            }
            else if (inputs[0] > 0)
            {
                foreach (SpriteRenderer renderer in spriteRenderers)
                {
                    renderer.flipX = false;
                }
            }

            movementMode.Move(inputs);
        }

        if (Possessing)
        {
            currentlyPossessedObject.transform.position = transform.position;
        }

        if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))) && Possessing)
        {
            ResetGhost();
        }
    }
    #endregion

    #region Collision Handlers
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Possessable") && !Possessing && !IsHuman)
        {
            CanInteract = true;
            possessableObject = other.gameObject;
        }

        if (movementMode.interactableTags.Contains(other.gameObject.tag))
        {
            if (movementMode.Equals(ghostMovement))
            {
                if (IsHuman)
                {
                    CanInteract = true;
                    interactObject = other.gameObject;
                }
            }else
            {
                CanInteract = true;
                interactObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Possessable")) // Potentially store name of GO in case we have multiple possessable objects over each other and it becomes a problem
        {
            CanInteract = false;
            if (!Possessing)
                possessableObject = null;
        }

        if (movementMode.interactableTags.Contains(other.gameObject.tag))
        {
            CanInteract = false;
            //if (Possessing)
                interactObject = null;
        }
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Sets possessing to true and changes sprites to interacted object's sprite.
    /// Also changes movement mode to whichever movement mode is on the object and changes character controller to ghosts.
    /// </summary>
    void Possess()
    {
        Possessing = true;
        CanInteract = false;

        ghostObject.SetActive(false);
        currentlyPossessedObject = possessableObject;
        movementMode = possessableObject.GetComponent<MovementMode>();
        movementMode.SetRigidbody(ghostMovement.GetRigidbody());
        movementMode.SetPlayerNumber(playerJoystick);
        movementMode.SetPlayer(gameObject);
    }

    /// <summary>
    /// Sets ghost to not possessing and resets sprites
    /// </summary>
    void ResetGhost()
    {
        Possessing = false;       
        possessableObject = null;

        ghostObject.SetActive(true);
        currentlyPossessedObject = null;

        movementMode.SetRigidbody(null);
        movementMode.SetPlayerNumber(0);
        movementMode.SetPlayer(null);
        movementMode = ghostMovement;
    }

    /// <summary>
    /// Toggles from human to ghost and changes sprites + speed
    /// </summary>
    void ToggleHuman()
    {
        IsHuman = !IsHuman;
        if (IsHuman)
        {
            movementMode.SetSpeed(10f);
            ChangeSprites(humanSprite);
            ghostObject.SetActive(false);
            humanObject.SetActive(true);
        }
        else
        {
            movementMode.SetSpeed(4f);
            ChangeSprites(ghostSprite);
            ghostObject.SetActive(true);
            humanObject.SetActive(false);
        }
        CanInteract = false;
    }

    void ChangeSprites(Sprite sprite)
    {
        //spriteRenderer.sprite = sprite;
        Vector2 spriteSize = sprite.bounds.size;
        GetComponent<BoxCollider>().size = new Vector3(spriteSize.x / 2, spriteSize.y / 4, .5f);
        GetComponent<BoxCollider>().center = new Vector3(0, -spriteSize.y / 4, 0);
    }
    #endregion
}
