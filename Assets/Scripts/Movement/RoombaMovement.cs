using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RoombaMovement : MovementMode
{
    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();
        string currentJoystick = "joystick " + playerNumber.ToString();
        if(Input.GetKey(currentJoystick + " button 0") || (playerNumber == 1 && Input.GetKey("w"))) // A button
        {
            inputs.Add(0);
            inputs.Add(1);
            return inputs;
        }
        if(Input.GetKey(currentJoystick + " button 4") | (playerNumber == 1 && Input.GetKey("q"))) // L button
        {
            Debug.Log("Turning left");
            inputs.Add(-1);
            inputs.Add(0);
            return inputs;
        }
        if(Input.GetKey(currentJoystick + " button 5") || (playerNumber == 1 && Input.GetKey("e"))) // R button
        {

        }
        return null;
    }

    public override void InteractWithObject(GameObject interactObject)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        Vector3 moveVel = moveDirection * movementSpeed;
        rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }
}
