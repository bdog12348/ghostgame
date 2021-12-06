using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

/// <summary>
/// Basic 2D movement, WASD / Arrow Keys to move and you move smoothly in that direction
/// </summary>
public class TrashcanMovement : MovementMode
{
    [SerializeField] float totalHoldTime = 3f;

    [SerializeField] GameObject toSpawn;

    [SerializeField] GameObject roomba;

    [SerializeField] GameObject sliderGo;
    [SerializeField] Slider trashGauge;
    [SerializeField] AudioSource trashSpillSound;

    private TrashcanController trashcanController;
    float holdtimer;

    void Start()
    {
        holdtimer = totalHoldTime;
        trashcanController = GetComponent<TrashcanController>();
        trashGauge.maxValue = totalHoldTime;
    }
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
        {
            rb.velocity = Vector3.zero;
            holdtimer = totalHoldTime;
        }
        else
        {
            rb.velocity = moveVel;
            holdtimer -= Time.deltaTime;
            if (holdtimer <= 0f)
            {
                if(trashcanController.GetCurrentLoad() > 0)
                    trashSpillSound.Play();
                for(int i = 0; i < trashcanController.GetCurrentLoad(); i++)
                {
                    GameObject newObject = Instantiate(toSpawn, new Vector3(transform.position.x - .25f + .5f * i, transform.position.y, transform.position.z), Quaternion.identity, transform.parent.parent);
                    newObject.GetComponent<Dirt>().setRoomba(roomba);
                }
                trashcanController.Empty();
                holdtimer = totalHoldTime;
            }
        }
        trashGauge.value = totalHoldTime - holdtimer;
        if (totalHoldTime - holdtimer == 0)
        {
            sliderGo.SetActive(false);
        } else
        {
            sliderGo.SetActive(true);
        }
            //rb.AddForce(moveVel, ForceMode.VelocityChange);
    }
}
