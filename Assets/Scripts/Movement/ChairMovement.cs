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

    public override void Move(List<float> inputs)
    {
        Vector3 moveVec = new Vector3(inputs[0] * speedX * 40, 0f, inputs[1] * speedY * 40); // Chair gets a boost to movement to make it move further per step while having a reasonable speed
        char_controller.Move(FixMovementForCamera(moveVec) * Time.deltaTime);
    }
}
