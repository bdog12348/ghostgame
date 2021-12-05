using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class RoombaMovement : MovementMode
{
    private float[] xDirections = {0f, 1f, 1f, 1f, 0f, -1f, -1f, -1f};
    private float[] zDirections = {1f, 1f, 0f, -1f, -1f, -1f, 0f, 1f};   

    private int direction;

    float maxLoad = 1f;
    float currentLoad = 0f;
    bool full = false;

    [SerializeField] Slider fullIndicator;
    [SerializeField] GameObject canvas;

    void Start()
    {
        direction = 0;
        fullIndicator.maxValue = maxLoad;
    }
    public override List<float> GetInputs(Player player)
    {
        Vector3 newDirection = Vector3.zero;
        
        if(player.GetButtonDown("RoombaTurnL")) // L button
        {
            if(direction == 0)
                direction = 7;
            else
                direction -= 1;
                    
        }
        if(player.GetButtonDown("RoombaTurnR")) // R button
        {
            if(direction == 7)
                direction = 0;
            else
                direction += 1;
        }
        if(player.GetButton("RoombaGo")) // A button
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
        if (interactObject.CompareTag("Trash"))
        {
            if (currentLoad > 0)
            {
                currentLoad = interactObject.GetComponent<TrashcanController>().AddTrash(currentLoad);
            }
            if (currentLoad < maxLoad)
                full = false;
        }
    }

    private void Update()
    {
        if (currentLoad > 0 )
        {
            canvas.SetActive(true);
            fullIndicator.enabled = true;
        }else
        {
            fullIndicator.enabled = false;
            canvas.SetActive(false);
        }
        fullIndicator.value = currentLoad;
    }

    public override void Move(List<float> inputs)
    {
        // Vector3 moveDirection = FixMovementForCamera(Vector3.forward * inputs[1] + Vector3.right * inputs[0]);
        // Vector3 moveVel = moveDirection * movementSpeed;
        // rb.AddForce(moveVel - rb.velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dirt") && !full)
        {
            Dirt dirt = other.gameObject.GetComponent<Dirt>();
            if (dirt.GetSmall())
            {
                Destroy(other.gameObject);
            }else
            {
                dirt.SetSmall(true);
            }
            currentLoad += 0.5f;

            if (currentLoad == maxLoad)
            {
                full = true;
            }
        }
    }
}
