using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class PlayerMovement : MovementMode
{
    public override List<float> GetInputs(Player player)
    {
        List<float> inputs = new List<float>();
        Vector2 r = new Vector2(player.GetAxisRaw("Horizontal"), player.GetAxisRaw("Vertical"));
        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);

        return inputs;
    }

    public override void InteractWithObject(GameObject interactObject)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        Vector3 moveVel = moveDirection * movementSpeed;
        if (moveVel == Vector3.zero)
            rb.velocity = Vector3.zero;
        else
            rb.velocity = moveVel;
            //rb.AddForce(moveVel, ForceMode.VelocityChange);
    }
}
