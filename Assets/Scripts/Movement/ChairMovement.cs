using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tap on keys to move, first key that's pressed you get an impulse in that direction
/// </summary>
public class ChairMovement : MovementMode
{
    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();

        if (Input.GetKeyDown(KeyCode.W))
        {
            inputs.Add(0);
            inputs.Add(1);
            return inputs;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            inputs.Add(0);
            inputs.Add(-1);
            return inputs;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            inputs.Add(-1);
            inputs.Add(0);
            return inputs;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputs.Add(1);
            inputs.Add(0);
            return inputs;
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
        Vector3 moveVel = moveDirection * movementSpeed * 10;
        rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }
}
