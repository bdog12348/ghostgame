using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] GameObject interactIndicatorObject;
    [SerializeField] Sprite ghostSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] public int playerJoystick;
    [SerializeField] Animator spriteAnimator;
    #endregion

    #region Private Fields
    GameObject possessableObject = null;
    GameObject interactObject = null;

    MovementMode movementMode;
    PlayerMovement ghostMovement;
    SpriteRenderer spriteRenderer;

    bool GameOver = false;
    bool CanInteract = false;
    bool Possessing = false;
    bool IsHuman = false;

    List<float> inputs;
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
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
          || (playerJoystick == 1 && Input.GetKeyDown(KeyCode.Space) && !Possessing))
        {
            ToggleHuman();
            spriteAnimator.SetBool("IsHuman", IsHuman);
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
            }
        }
        else
        {
            interactIndicatorObject.SetActive(false);
        }

        if (movementMode != null)
        {
            inputs = movementMode.GetInputs();
        }

        if (inputs != null)
        {
            // Do animation stuff
            //if (inputs[0] < 0)
            //{
            //    spriteRenderer.flipX = false;
            //    spriteAnimator.SetBool("Moving", true);

            //}else if (inputs[0] > 0)
            //{
            //    spriteRenderer.flipX = true;
            //    spriteAnimator.SetBool("Moving", true);
            //}else
            //{
            //    spriteAnimator.SetBool("Moving", false);
            //}

            movementMode.Move(inputs);
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
        if (other.gameObject.CompareTag("Possessable") && !Possessing)
        {
            CanInteract = true;
            possessableObject = other.gameObject;
        }

        if (movementMode.interactableTags.Contains(other.gameObject.tag) && Possessing)
        {
            CanInteract = true;
            interactObject = other.gameObject;
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

        if (movementMode.interactableTags.Contains(other.gameObject.tag) && Possessing)
        {
            CanInteract = false;
            if (Possessing)
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
        ChangeSprites(possessableObject.GetComponentInChildren<SpriteRenderer>().sprite);
        movementMode = possessableObject.GetComponent<MovementMode>();
        movementMode.SetRigidbody(ghostMovement.GetRigidbody());
        movementMode.SetPlayerNumber(playerJoystick);
        movementMode.SetPlayer(gameObject);
        possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    /// <summary>
    /// Sets ghost to not possessing and resets sprites
    /// </summary>
    void ResetGhost()
    {
        Possessing = false;       
        ChangeSprites(ghostSprite);
        possessableObject.transform.position = transform.position;
        possessableObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        possessableObject = null;

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
        }
        else
        {
            movementMode.SetSpeed(4f);
            ChangeSprites(ghostSprite);
        }
    }
    void ChangeSprites(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        GetComponent<BoxCollider>().size = new Vector3(spriteSize.x / 2, spriteSize.y / 4, .5f);
        GetComponent<BoxCollider>().center = new Vector3(0, -spriteSize.y / 4, 0);
    }
    #endregion
}
