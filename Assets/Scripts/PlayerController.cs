using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] GameObject interactObject;
    [SerializeField] Sprite ghostSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] public int playerJoystick;
    #endregion

    #region Private Fields
    GameObject possessableObject = null;

    MovementMode movementMode;
    PlayerMovement ghostMovement;
    SpriteRenderer spriteRenderer;

    bool GameOver = false;
    bool CanInteract = false;
    bool Possessing = false;
    bool IsHuman = true;

    List<float> inputs;
    #endregion

    #region MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if(spriteRenderer.sprite != humanSprite)
            IsHuman = false;
        ghostMovement = GetComponent<PlayerMovement>();
        ghostMovement.AutoAsssignCharacterController();
        ghostMovement.SetPlayerNumber(playerJoystick);
        movementMode = ghostMovement;
        TimerHelper.OnTimerEnd += () => GameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver) return; // Disable script?

        if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 1") && !Possessing)
          || (playerJoystick == 1 && Input.GetKeyDown("space") && !Possessing))
        {
            ToggleHuman();
        }

        if (CanInteract && !Possessing && !IsHuman)
        {
            interactObject.SetActive(true);
            if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))))
            {
                Possess();
                return;
            }
        }
        else
        {
            interactObject.SetActive(false);
        }

        if (movementMode != null)
        {
            inputs = movementMode.GetInputs();
        }

        if (inputs != null)
        {
            movementMode.Move(inputs);
        }

        if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 3") || (playerJoystick == 1 && Input.GetKeyDown("e"))) && Possessing)
        {
            ResetGhost();
        }
    }

    private void FixedUpdate()
    {
    }
    #endregion

    #region Collision Handlers
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Possessable"))
        {
            CanInteract = true;
            possessableObject = other.gameObject;
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
        spriteRenderer.sprite = possessableObject.GetComponentInChildren<SpriteRenderer>().sprite;
        movementMode = possessableObject.GetComponent<MovementMode>();
        movementMode.SetCharacterController(ghostMovement.GetCharacterController());
        movementMode.SetPlayerNumber(playerJoystick);
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
        movementMode.SetPlayerNumber(0);
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
            spriteRenderer.sprite = humanSprite;
        }
        else
        {
            movementMode.SetSpeed(4f);
            spriteRenderer.sprite = ghostSprite;
        }
    }
    #endregion
}
