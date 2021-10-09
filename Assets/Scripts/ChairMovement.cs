using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tap on keys to move, first key that's pressed you get an impulse in that direction
/// </summary>
public class ChairMovement : MovementMode
{
    bool CanMove = true;

    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();

        if (Input.GetKeyDown(KeyCode.W))
        {
            inputs.Add(0);
            inputs.Add(1);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            inputs.Add(0);
            inputs.Add(-1);
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            inputs.Add(-1);
            inputs.Add(0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputs.Add(1);
            inputs.Add(0);
        }

        return inputs;
    }

    public override void Move(List<float> inputs)
    {
        if (CanMove && inputs.Count == 2)
        {
            char_controller.Move(new Vector3(inputs[0] * speedX * 10, 0f, inputs[1] * speedY * 10) * Time.deltaTime);
            CanMove = false;
            StartCoroutine(WaitToMove());
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.15f);

        CanMove = true;
    }
}
