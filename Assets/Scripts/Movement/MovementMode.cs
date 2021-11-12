using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementMode : MonoBehaviour
{

    #region Protected Fields
    protected Rigidbody rb;
    protected GameObject player;
    protected float verticalVelocity;
    protected Vector3 slopeNormal;
    protected int playerNumber = 0;
    protected int currentGoalNumber = 0;
    #endregion

    #region Serialized Fields
    //Note: not all of these will be used for every movement mode
    [Header("Movement Config")]
    [SerializeField] protected float movementSpeed = 5f;

    [Header("Optional Goals")]
    public string[] interactableTags;
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

    public abstract void InteractWithObject(GameObject interactObject);

    public void SetPlayerNumber(int player)
    {
        playerNumber = player;
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }

    /// <summary>
    /// Assigns rb to Rigidbody on GameObject if there is one
    /// </summary>
    public void AutoAsssignRigidbody()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Sets rb to <paramref name="rigidb"/>
    /// </summary>
    /// <param name="rigidb">Rigidbody to set to</param>
    public void SetRigidbody(Rigidbody rigidb)
    {
        rb = rigidb;
    }

    /// <summary>
    /// Returns current character controller in MovementMode
    /// </summary>
    /// <returns>Current character controller</returns>
    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void AddSpeed(float amount)
    {
        movementSpeed += amount;
    }
    public void SubtractSpeed(float amount)
    {
        movementSpeed -= amount;
    }
    public void SetSpeed(float amount)
    {
        movementSpeed = amount;
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Checks if character controller is hitting the ground
    /// </summary>
    /// <returns>True if raycasts hit the ground, False otherwise</returns>
    //protected bool Grounded()
    //{
    //    if (verticalVelocity > 0)
    //        return false;

    //    float yRay = (char_controller.bounds.center.y - (char_controller.height * 0.5f)) + innerVerticalOffset; // Bottom of character controller
    //    RaycastHit hit;

    //    //Mid
    //    if (Physics.Raycast(new Vector3(char_controller.bounds.center.x, yRay, char_controller.bounds.center.z), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
    //    {
    //        Debug.DrawRay(new Vector3(char_controller.bounds.center.x, yRay, char_controller.bounds.center.z), -Vector3.up * (innerVerticalOffset + distanceGrounded), Color.red);
    //        slopeNormal = hit.normal;
    //        return (slopeNormal.y > slopeThreshold) ? true : false;
    //    }
    //    //Front-Right
    //    if (Physics.Raycast(new Vector3(char_controller.bounds.center.x + (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z + (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
    //    {
    //        slopeNormal = hit.normal;
    //        return (slopeNormal.y > slopeThreshold) ? true : false;
    //    }
    //    //Front-Left
    //    if (Physics.Raycast(new Vector3(char_controller.bounds.center.x - (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z + (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
    //    {
    //        slopeNormal = hit.normal;
    //        return (slopeNormal.y > slopeThreshold) ? true : false;
    //    }
    //    //Back-Right
    //    if (Physics.Raycast(new Vector3(char_controller.bounds.center.x + (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z - (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
    //    {
    //        slopeNormal = hit.normal;
    //        return (slopeNormal.y > slopeThreshold) ? true : false;
    //    }
    //    //Back-Left
    //    if (Physics.Raycast(new Vector3(char_controller.bounds.center.x - (char_controller.bounds.extents.x - extremitiesOffset), yRay, char_controller.bounds.center.z - (char_controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
    //    {
    //        slopeNormal = hit.normal;
    //        return (slopeNormal.y > slopeThreshold) ? true : false;
    //    }

    //    return false;
    //}

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
