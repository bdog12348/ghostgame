using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoombaMovement : MovementMode
{
    private float[] xDirections = {0f, 1f, 1f, 1f, 0f, -1f, -1f, -1f};
    private float[] zDirections = {1f, 1f, 0f, -1f, -1f, -1f, 0f, 1f};   

    private int direction;

    void Start()
    {
        direction = 0;
    }
    public override List<float> GetInputs()
    {
        Vector3 newDirection = Vector3.zero;
        string currentJoystick = "joystick " + playerNumber.ToString();
        
        if(Input.GetKeyDown(currentJoystick + " button 4") | (playerNumber == 1 && Input.GetKeyDown("a"))) // L button
        {
            if(direction == 0)
                direction = 7;
            else
                direction -= 1;
                    
        }
        if(Input.GetKeyDown(currentJoystick + " button 5") || (playerNumber == 1 && Input.GetKeyDown("d"))) // R button
        {
            if(direction == 7)
                direction = 0;
            else
                direction += 1;
        }
        if(Input.GetKey(currentJoystick + " button 0") || (playerNumber == 1 && Input.GetKey("w"))) // A button
        {
            Vector2 r = new Vector2(xDirections[direction], zDirections[direction]);
            r = (r.magnitude > 1) ? r.normalized : r;
            newDirection.x = r.x;
            newDirection.z = r.y;
            rb.velocity = newDirection * movementSpeed;           
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        return null;    // "Move" does not need to be called
    }

    public override void InteractWithObject(GameObject interactObject)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(List<float> inputs)
    {
        // Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        // Vector3 moveVel = moveDirection * movementSpeed;
        // rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }
}
