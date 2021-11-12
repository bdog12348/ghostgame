using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class DustpanMovement : MovementMode
{
    bool full = false;
    int currentLoad = 0;
    readonly int MAX_LOAD = 2;

    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();
        Vector2 r;
        if(playerNumber == 1 && Input.GetAxisRaw("HorizontalGamepad1") == 0 && Input.GetAxisRaw("VerticalGamepad1") == 0)
        {
            r = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if(playerNumber != 0)
        {
            r = new Vector2(Input.GetAxisRaw("HorizontalGamepad" + playerNumber.ToString()), Input.GetAxisRaw("VerticalGamepad" + playerNumber.ToString()));
        }
        else
        {
            return null;
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
        if (moveVel == Vector3.zero)
            rb.velocity = Vector3.zero;
        else
            rb.velocity = moveVel;
            //rb.AddForce(moveVel, ForceMode.VelocityChange);
    }

    public override void InteractWithObject(GameObject interactObject)
    {
        if (interactObject.CompareTag("Dirt"))
        {
            if (!full)
            {
                currentLoad++;
                SetFullLevelSprite();
                interactObject.SetActive(false);
            }
        }else if (interactObject.CompareTag("Trash"))
        {
            if (currentLoad > 0)
            {
                currentLoad = interactObject.GetComponent<TrashcanController>().AddTrash(currentLoad);
                Debug.Log($"Dustpan got {currentLoad} as a return value after adding trash");
                SetFullLevelSprite();
            }
        }

        if (currentLoad == MAX_LOAD)
        {
            full = true;
        }else
        {
            full = false;
        }
    }

    void SetFullLevelSprite()
    {
        if (currentLoad == MAX_LOAD)
        {
            SpriteChanger.SetFilledSprite(2);
        }else if (currentLoad >= 1)
        {
            SpriteChanger.SetFilledSprite(1);
        }else
        {
            SpriteChanger.SetFilledSprite(0);
        }
    }
}
