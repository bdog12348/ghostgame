using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class PlungerMovement : MovementMode
{
    public float jumpSpeed;
    private float ySpeed;
    private float distanceToGround;
    private bool isGrounded;
    private int clickCount;
    [SerializeField] public int clickThreshold;


    void Start()
    {
        clickCount = 0;
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

        // Return before the jump if not grounded
        if(!isGrounded)
            return inputs;

        // Jump
        if(Input.GetKeyDown(currentJoystick + " button 0") || (playerNumber == 1 && Input.GetKeyDown("space")))
        {
            if(clickCount == (clickThreshold - 1))
            {
                clickCount = 0;
                ySpeed = jumpSpeed;
            }
            else
            {
                clickCount++;
            }
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
        // if (interactObject.CompareTag("Dirt"))
        // {
        //     if (!full)
        //     {
        //         currentLoad++;
        //         SetFullLevelSprite();
        //         interactObject.SetActive(false);
        //     }
        // }
        // else if (interactObject.CompareTag("Trash"))
        // {
        //     if (currentLoad > 0)
        //     {
        //         currentLoad = interactObject.GetComponent<TrashcanController>().AddTrash(currentLoad);
        //         Debug.Log($"Dustpan got {currentLoad} as a return value after adding trash");
        //         SetFullLevelSprite();
        //     }
        // }

        // if (currentLoad == MAX_LOAD)
        // {
        //     full = true;
        // }else
        // {
        //     full = false;
        // }
    }

    void SetFullLevelSprite()
    {
        // if (currentLoad == MAX_LOAD)
        // {
        //     SpriteChanger.SetFilledSprite(2);
        // }else if (currentLoad >= 1)
        // {
        //     SpriteChanger.SetFilledSprite(1);
        // }else
        // {
        //     SpriteChanger.SetFilledSprite(0);
        // }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(player.transform.position, -Vector3.up, Color.red);
        return Physics.Raycast(player.transform.position, -Vector3.up, distanceToGround + .1f);
    }
}
