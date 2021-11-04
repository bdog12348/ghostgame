using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementMode : MonoBehaviour
{
    #region Protected Fields
    protected CharacterController char_controller;
    protected float verticalVelocity;
    protected Vector3 slopeNormal;
    protected int playerNumber = 0;
    #endregion

    #region Serialized Fields
    //Note: not all of these will be used for every movement mode
    [Header("Movement Config")]
    [SerializeField] protected float speedX = 5;
    [SerializeField] protected float speedY = 5;
    [SerializeField] protected float gravity = 0.25f;
    [SerializeField] protected float terminalVelocity = 5.0f;
    [SerializeField] protected float jumpForce = 8.0f;

    [Header("Ground Check Raycast")]
    [SerializeField] protected float extremitiesOffset = 0.05f;
    [SerializeField] protected float innerVerticalOffset = 0.25f;
    [SerializeField] protected float distanceGrounded = 0.15f;
    [SerializeField] protected float slopeThreshold = 0.55f;
    #endregion

    #region Public Methods
    /// <summary>
    /// Takes in input however it may be handled depending on the child class
    /// </summary>
    /// <returns>List of the input parameters needed to carry out movement</returns>
    public abstract List<float> GetInputs();

    /// <summary>
    /// Gien <paramref name="inputs"/> figures out how ot move the player appropriately
    /// </summary>
    /// <param name="inputs">List of inputs used to calculate movement</param>
    public abstract void Move(List<float> inputs);

    public void SetPlayerNumber(int player)
    {
        playerNumber = player;
    }


    /// <summary>
    /// Assigns character controller to character controller on GameObject if there is one
    /// </summary>
    public void AutoAsssignCharacterController()
    {
        char_controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Sets character controller to <paramref name="characterController"/>
    /// </summary>
    /// <param name="characterController">Character controller to set to</param>
    public void SetCharacterController(CharacterController characterController)
    {
        char_controller = characterController;
    }

    /// <summary>
    /// Returns current character controller in MovementMode
    /// </summary>
    /// <returns>Current character controller</returns>
    public CharacterController GetCharacterController()
    {
        return char_controller;
    }

    public void AddSpeed(float amount, bool bothAxes = true, bool xAxis = true)
    {
        if (bothAxes)
        {
            speedX += amount;
            speedY += amount;
        }
        else
        {
            if (xAxis)
            {
                speedX += amount;
            }
            else
            {
                speedY += amount;
            }
        }
    }
    public void SubtractSpeed(float amount, bool bothAxes = true, bool xAxis = true)
    {
        if (bothAxes)
        {
            speedX -= amount;
            speedY -= amount;
        }
        else
        {
            if (xAxis)
            {
                speedX -= amount;
            }
            else
            {
                speedY -= amount;
            }
        }
    }
    public void SetSpeed(float amount, bool bothAxes = true, bool xAxis = true)
    {
        if (bothAxes)
        {
            speedX = amount;
            speedY = amount;
        }
        else
        {
            if (xAxis)
            {
                speedX = amount;
            }
            else
            {
                speedY = amount;
            }
        }
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Checks if character controller is hitting the ground
    /// </summary>
    /// <returns>True if raycasts hit the ground, False otherwise</returns>
    protected bool Grounded()
    {
        if (verticalVelocity > 0)
            return false;

        float yRay = (char_controller.bounds.center.y - (char_controller.height * 0.5f)) + innerVerticalOffset; // Bottom of character controller
        RaycastHit hit;

        //Mid
        if (Physics.Raycast(new Vector3(char_controller.bounds.center.x, yRay, char_controller.bounds.center.z), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            Debug.DrawRay(new Vector3(char_controller.bounds.center.x, yRay, char_controller.bounds.center.z), -Vector3.up * (innerVerticalOffset + distanceGrounded), Color.red);
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        //Front-Right
        if (Physics.Raycast(new Vector3(char_controller.bounds.center.x + (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z + (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        //Front-Left
        if (Physics.Raycast(new Vector3(char_controller.bounds.center.x - (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z + (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        //Back-Right
        if (Physics.Raycast(new Vector3(char_controller.bounds.center.x + (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z - (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        //Back-Left
        if (Physics.Raycast(new Vector3(char_controller.bounds.center.x - (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z - (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        return false;
    }

    /// <summary>
    /// Given <paramref name="moveVector"/> returns a Vector3 following the floor even if it's sloped
    /// </summary>
    /// <param name="moveVector">Input vector after anything added to it</param>
    /// <returns>Adjusted move vector</returns>
    protected Vector3 FollowFloor(Vector3 moveVector)
    {
        Vector3 right = new Vector3(slopeNormal.y, -slopeNormal.x, 0).normalized;
        Vector3 forward = new Vector3(0, -slopeNormal.z, slopeNormal.y).normalized;
        return right * moveVector.x + forward * moveVector.z;
    }

    protected Vector3 FixMovementForCamera(Vector3 moveVector)
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
    #endregion
}
