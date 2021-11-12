using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementMode : MonoBehaviour
{

    #region Protected Fields
    protected Rigidbody rb;
    protected SpriteChanger SpriteChanger;
    protected float verticalVelocity;
    protected int playerNumber = 0;
    #endregion

    #region Serialized Fields
    //Note: not all of these will be used for every movement mode
    [Header("Movement Config")]
    [SerializeField] protected float movementSpeed = 5f;

    public string[] interactableTags;
    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        SpriteChanger = GetComponentInChildren<SpriteChanger>();
    }
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
