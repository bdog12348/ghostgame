using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BroomMovement : MovementMode
{

    private List<float> charge = new List<float>();

    void awake()
    {
        charge.Add(0);
        charge.Add(0);
    }

    public override List<float> GetInputs()
    {
        List<float> inputs = new List<float>();
        Vector2 r;
        if((playerNumber == 1 && Input.GetAxisRaw("HorizontalGamepad1") == 0 && Input.GetAxisRaw("VerticalGamepad1") == 0 &&
        Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) ||
        Input.GetAxisRaw("HorizontalGamepad" + playerNumber.ToString()) == 0 && Input.GetAxisRaw("VerticalGamepad" + playerNumber.ToString()) == 0)
        {
            return null;
        }
        else if(playerNumber == 1 && Input.GetAxisRaw("HorizontalGamepad1") == 0 && Input.GetAxisRaw("VerticalGamepad1") == 0)
        {
            r = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            r = new Vector2(Input.GetAxisRaw("HorizontalGamepad" + playerNumber.ToString()), Input.GetAxisRaw("VerticalGamepad" + playerNumber.ToString()));
        }
        r = (r.magnitude > 1) ? r.normalized : r;
        inputs.Add(r.x);
        inputs.Add(r.y);
        //charge[0] += inputs[0];
        //charge[1] += inputs[1];
        print(charge[0]);

        return null;
    }

    public override void Move(List<float> inputs)
    {
        Vector3 moveVec = new Vector3(inputs[0] * speedX, 0f, inputs[1] * speedY);
        char_controller.Move(FixMovementForCamera(moveVec) * Time.deltaTime);
    }
}
