using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class DustpanMovement : MovementMode
{
    bool full = false;
    int currentLoad = 0;
    readonly int MAX_LOAD = 2;

    public float jumpSpeed;
    private float ySpeed;
    private float distanceToGround;
    private bool isGrounded;

    SpriteChanger dirtSpriteChanger;

    private void Start()
    {
        dirtSpriteChanger = transform.Find("Dirt Overlay").GetComponent<SpriteChanger>();
    }

    void Update()
    {
        // Check if it is grounded on this frame
        if(player != null)
        {
            isGrounded = IsGrounded();
        }

        // Set the distanceToGround if the object was just inhabited by a player
        if(player == null)
        {
            distanceToGround = 0f;
        }
        else if(distanceToGround == 0f)
        {
            distanceToGround = GetComponent<BoxCollider>().bounds.extents.y;
        }

        ySpeed += Physics.gravity.y * Time.deltaTime;
    }
    public override List<float> GetInputs(Player player)
    {
        string currentJoystick = "joystick " + playerNumber.ToString();

        List<float> inputs = new List<float>();
        Vector2 r = new Vector2(player.GetAxisRaw("Horizontal"), player.GetAxisRaw("Vertical"));

        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);

        // Return before the jump if not grounded
        if(!isGrounded)
            return inputs;

        // Jump
        if(player.GetButtonDown("Jump"))
        {
            ySpeed = jumpSpeed;
        }

        return inputs;
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        Vector3 moveVel = moveDirection * movementSpeed;
        if (moveVel == Vector3.zero)
        {
            moveVel.y = -1; // Makes the object fall if it's in the air
        } 
        else
        {
            moveVel.y = ySpeed;
        }
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
        }
        else if (interactObject.CompareTag("Trash"))
        {
            if (currentLoad > 0)
            {
                currentLoad = interactObject.GetComponent<TrashcanController>().AddTrash(currentLoad);
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
            dirtSpriteChanger.SetFilledSprite(2);
        }else if (currentLoad >= 1)
        {
            dirtSpriteChanger.SetFilledSprite(1);
        }else
        {
            dirtSpriteChanger.SetFilledSprite(0);
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(player.transform.position, -Vector3.up, Color.red);
        return Physics.Raycast(player.transform.position, -Vector3.up, distanceToGround - .1f);
    }
}
