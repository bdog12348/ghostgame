using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
// The broom keeps moving when you let go but hold a direction again. This is b/c of slow down method
// The broom does not move diagonally with WASD
/// </summary>
public class BroomMovement : MovementMode
{
    private bool storedPower;
    private Vector3 storedPowerInfo;
    private BroomPowerHelper powerHelper;
    private Player rePlayer = null;
    private bool startPlayingSounds = false;
    [SerializeField] AudioSource broomSwoosh;

    float idleTime = 0f;

    void Start()
    {
        storedPowerInfo = Vector3.zero;
        storedPower = true;
        powerHelper = GetComponentInChildren<BroomPowerHelper>();
    }

    public bool StoringPower()
    {
        return storedPower;
    }
    public override List<float> GetInputs(Player player)
    {
        if (rePlayer == null)
            rePlayer = player;
        List<float> inputs = new List<float>();
        Vector2 r = new Vector2(player.GetAxisRaw("Horizontal"), player.GetAxisRaw("Vertical"));

        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);

        return inputs;
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        
        // If the player lets go the guage resets
        if(moveDirection == Vector3.zero)
        {
            if(storedPower && startPlayingSounds)
                broomSwoosh.Play();
            
            else if(storedPower && !startPlayingSounds)
                startPlayingSounds = true;
            if(storedPower)
            {
                rb.velocity = -storedPowerInfo * movementSpeed * powerHelper.DeactivateWithReturn();
            }
            else
            {
                powerHelper.Deactivate();                
            }

            storedPowerInfo = Vector3.zero;
            storedPower = false;
        }
        // If the player is holding a direction while stationary, store that power
        else if(moveDirection != Vector3.zero && rb.velocity == Vector3.zero)
        {
            powerHelper.Activate();
            storedPower = true;
            // Only store the max power (so when they let go, it doesn't save the most recent position which would be small)
            if(Math.Abs(moveDirection.x) >= Math.Abs(storedPowerInfo.x) || Math.Abs(moveDirection.z) >= Math.Abs(storedPowerInfo.z))
            {
                storedPowerInfo = moveDirection;
            }
            //If they press the jump button release power and move them
            // if (rePlayer.GetButtonDown("Jump"))
            // {
            //     rb.velocity = -moveDirection * movementSpeed * powerHelper.DeactivateWithReturn();
            //     storedPowerInfo = Vector3.zero;
            // }
        }
    }

    private void Update()
    {
        if(!ObjectTaken() && powerHelper.HelperOn())
            powerHelper.Deactivate();
        // If the player is still moving, slow them down
        if (rb && rb.velocity != Vector3.zero)
        {
            Vector3 newVelocity = Vector3.zero;
            newVelocity = rb.velocity;

            if (newVelocity.x > 0)
                newVelocity.x -= 0.01f;
            else if (newVelocity.x < 0)
                newVelocity.x += 0.01f;
            if (newVelocity.z > 0)
                newVelocity.z -= 0.01f;
            else if (newVelocity.z < 0)
                newVelocity.z += 0.01f;
            if (newVelocity.x < 0.1f && newVelocity.x > -0.1f)
                newVelocity.x = 0;
            if (newVelocity.z < 0.1f && newVelocity.z > -0.1f)
                newVelocity.z = 0;
            rb.velocity = newVelocity;
            idleTime += Time.deltaTime;
        }

        if (idleTime > 5f)
        {
            idleTime = 0f;
            rb.velocity = Vector3.zero;
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel;
    }

    public bool HoldingLeft()
    {
        if(storedPowerInfo.x < 0)
            return true;
        return false;
    }

    public bool HoldingRight()
    {
        if(storedPowerInfo.x > 0)
            return true;
        return false;
    }
    
    public override void InteractWithObject(GameObject interactObject)
    {
        throw new System.NotImplementedException();
    }

}
