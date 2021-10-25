using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class PlayerMovement : MovementMode
{
    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();

        Vector2 r = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);

        return inputs;
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveVector = new Vector3(inputs[0] * speedX, 0, inputs[1] * speedY);
        char_controller.Move(FixMovementForCamera(moveVector) * Time.deltaTime);
    }
}
