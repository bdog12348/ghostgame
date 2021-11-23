using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BroomMovement : MovementMode
{
    // The broom keeps moving when you let go but hold a direction again. This is b/c of slow down method
    // The broom does not move diagonally with WASD
    private bool storedPower;
    private Vector3 storedPowerInfo;

    void Start()
    {
        storedPowerInfo = Vector3.zero;
        storedPower = true;
    }
    public override List<float> GetInputs()
    {
        string currentJoystick = "joystick " + playerNumber.ToString();

        List<float> inputs = new List<float>();
        Vector2 r;

    
        if(playerNumber == 1 && Input.GetAxisRaw("HorizontalGamepad1") == 0 && Input.GetAxisRaw("VerticalGamepad1") == 0)
        {
            r = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // keyboard
        }
        else if(playerNumber != 0)
        {
            r = new Vector2(Input.GetAxisRaw("HorizontalGamepad" + playerNumber.ToString()), Input.GetAxisRaw("VerticalGamepad" + playerNumber.ToString()));
        }
        else
        {
            r = new Vector2(0f,0f);
        }

        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);

        return inputs;
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        Vector3 moveVel = moveDirection * movementSpeed;
        
        // If the player just let go of the direction they were holding
        if(moveVel == Vector3.zero && storedPowerInfo != Vector3.zero && storedPower)
        {
            Vector3 newMoveDirection = -storedPowerInfo;
            rb.velocity = newMoveDirection * movementSpeed;
            storedPowerInfo = Vector3.zero;
            storedPower = false;
        }
        // If the player is holding a direction while stationary, store that power
        else if(moveVel != Vector3.zero && rb.velocity == Vector3.zero)
        {
            // Only store the max power (so when they let go, it doesn't save the most recent position which would be small)
            if(Math.Abs(moveDirection.x) >= Math.Abs(storedPowerInfo.x) || Math.Abs(moveDirection.z) >= Math.Abs(storedPowerInfo.z))
            {
                storedPowerInfo = moveDirection;
            }
        }
        // If the player is still moving, slow them down
        else if(rb.velocity != Vector3.zero)
        {
            Vector3 newVelocity = Vector3.zero;
            newVelocity = rb.velocity;

            if(newVelocity.x > 0)
                newVelocity.x -= 0.01f;
            else if(newVelocity.x < 0)
                newVelocity.x += 0.01f;
            if(newVelocity.z > 0)
                newVelocity.z -= 0.01f;
            else if(newVelocity.z < 0)
                newVelocity.z += 0.01f;
            if(newVelocity.x < 0.01f && newVelocity.x > -0.01f)
                newVelocity.x = 0;
            if(newVelocity.z < 0.01f && newVelocity.z > -0.01f)
                newVelocity.z = 0;
            rb.velocity = newVelocity;
        }
        storedPower = true;
        //rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }

    public override void InteractWithObject(GameObject interactObject)
    {
        throw new System.NotImplementedException();
    }

}
