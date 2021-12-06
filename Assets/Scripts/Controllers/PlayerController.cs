using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Rewired;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] GameObject interactIndicatorObject;
    [SerializeField] GameObject ghostObject;
    [SerializeField] GameObject humanObject;
    [SerializeField] public Sprite ghostSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] public int playerJoystick;
    [SerializeField] Animator spriteAnimator;
    [SerializeField] SpriteRenderer[] spriteRenderers;
    [SerializeField] BoxCollider collider;

    #endregion

    #region Private Fields
    GameObject possessableObject = null;
    GameObject currentlyPossessedObject = null;
    GameObject interactObject = null;

    GameObject[] possessableTags;
    GameObject[] trashTags;


    Player player;

    SpriteChanger spriteChanger = null;
    MovementMode movementMode;
    PlayerMovement ghostMovement;

    bool GameOver = false;
    bool CanInteract = false;
    bool Possessing = false;
    bool possessingTrash = false;
    float currentTrashLoad = 0;
    bool IsHuman = false;
    float totalHoldTime = 1f;
    float holdTimer;

    bool showingPossessables = true;
    List<float> inputs;
    float showTimer = 5.0f;
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
        holdTimer = totalHoldTime;
        spriteRenderers[0].sprite = ghostSprite;
        InitializeAllPossessables();
        ShowAllPossessables();
    }

    void InitializeAllPossessables()
    {
        possessableTags = GameObject.FindGameObjectsWithTag("Possessable");
        trashTags = GameObject.FindGameObjectsWithTag("Trash");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver || GameManager.Paused) return; // Disable script?

        //if ((Input.GetKeyDown("joystick " + playerJoystick.ToString() + " button 1") && !Possessing)
        //  || (playerJoystick == 1 && Input.GetKeyDown(KeyCode.Space) && !Possessing && !isDraggingObject))
        //{
        //    ToggleHuman();
        //}

        if(showTimer > 0f)
            showTimer -= Time.deltaTime;
        if(!showingPossessables && player.GetButtonDown("ShowPossessables"))
        {
            showTimer = 5.0f;
            ShowAllPossessables();
        }
        else if(showingPossessables && player.GetButtonDown("ShowPossessables"))
            showTimer = 5.0f;
        if(showingPossessables && showTimer <= 0f)
        {
            HideAllPossessables();
        }
        

        if (CanInteract)
        {
            if (Possessing) // On goal object
            {
                interactIndicatorObject.SetActive(true);
                if (player.GetButtonDown("Action"))
                {
                    movementMode.InteractWithObject(interactObject);
                    CanInteract = false;
                    return;
                }
            }
            else if (!Possessing && !IsHuman) // On possessable object
            {
                interactIndicatorObject.SetActive(true);
                if (player.GetButton("Action"))
                {
                    holdTimer -= Time.deltaTime;
                    if (holdTimer <= 0f)
                    {
                        Possess();
                        holdTimer = totalHoldTime;
                    }                
                    return;
                }
                else if(player.GetButtonUp("Action"))
                {
                    holdTimer = totalHoldTime;
                }
            }
        }
        else
        {
            interactIndicatorObject.SetActive(false);
        }

        if (movementMode != null)
        {
            inputs = movementMode.GetInputs(player);
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

        if (player.GetButton("Action") && Possessing)
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                ResetGhost();
                holdTimer = totalHoldTime;
            }         
        }
        else if(player.GetButtonUp("Action") && Possessing)
        {
            holdTimer = totalHoldTime;
        }
    }
    #endregion

    #region Collision Handlers
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Possessable") || other.gameObject.CompareTag("Trash")) 
            && !other.gameObject.GetComponent<PossessedStatus>().ObjectTaken() && !Possessing && !IsHuman)
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
        if (other.gameObject.CompareTag("Possessable") || 
        (other.gameObject.CompareTag("Trash") && other.gameObject.GetComponent<TrashcanController>().CheckFull())) // Potentially store name of GO in case we have multiple possessable objects over each other and it becomes a problem
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

        collider.isTrigger = false;
        transform.position = possessableObject.transform.position;
        ghostObject.SetActive(false);
        currentlyPossessedObject = possessableObject;
        movementMode = possessableObject.GetComponent<MovementMode>();
        movementMode.SetRigidbody(ghostMovement.GetRigidbody());
        movementMode.SetPlayerNumber(playerJoystick);
        movementMode.SetPlayer(gameObject);
        spriteChanger = currentlyPossessedObject.transform.Find("Sprite").gameObject.GetComponent<SpriteChanger>();
        if (currentlyPossessedObject.CompareTag("Trash"))
        {
            possessingTrash = true;
            currentTrashLoad = currentlyPossessedObject.GetComponent<TrashcanController>().GetCurrentLoad();
        }
        if(!possessingTrash && spriteChanger != null)
            spriteChanger.SetPlayerSprite(playerJoystick);
        else if(possessingTrash && spriteChanger != null)
        {
            TrashcanController tController = currentlyPossessedObject.GetComponent<TrashcanController>();
            tController.SetPossessed();
            if(tController.PartiallyFull())
                spriteChanger.SetFilledSprite(3);
            else if(tController.CheckFull())
                spriteChanger.SetFilledSprite(4);
            else
                spriteChanger.SetPlayerSprite(playerJoystick);
        }
        
    }

    void ShowAllPossessables()
    {
        showingPossessables = true;
        foreach (GameObject possessable in possessableTags)
        {
            possessable.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Show");

        }
        foreach (GameObject possessable in trashTags)
        {
            possessable.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Show");
        }
    }

    void HideAllPossessables()
    {
        showingPossessables = false;
        foreach (GameObject possessable in possessableTags)
        {
            possessable.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Hide");

        }
        foreach (GameObject possessable in trashTags)
        {
            possessable.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Hide");
        }
    }

    /// <summary>
    /// Sets ghost to not possessing and resets sprites
    /// </summary>
    void ResetGhost()
    {
        Possessing = false;       
        possessableObject = null;
        collider.isTrigger = true;
        ghostObject.SetActive(true);
        if(possessingTrash && spriteChanger != null)
        {
            TrashcanController tController = currentlyPossessedObject.GetComponent<TrashcanController>();
            tController.ResetPosession();
            if(tController.CheckFull())
                spriteChanger.SetFilledSprite(2);
            else if(tController.PartiallyFull())
                spriteChanger.SetFilledSprite(1);
            else
                spriteChanger.SetDefaultSprite();
            currentTrashLoad = 0;
            possessingTrash = false;
        }
        else if(spriteChanger != null)
            spriteChanger.SetDefaultSprite();
        currentlyPossessedObject = null;

        movementMode.SetRigidbody(null);
        movementMode.SetPlayerNumber(-1);
        movementMode.SetPlayer(null);
        movementMode = ghostMovement;
        spriteChanger = null;
    }
    #endregion

    #region Getters / Setters

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public bool PossessingTrash()
    {
        return possessingTrash;
    }
    public TrashcanController GetPossessedTrashController()
    {
        if(possessingTrash)
            return currentlyPossessedObject.GetComponent<TrashcanController>(); 
        else
            return null;
        
    }

    #endregion
}
