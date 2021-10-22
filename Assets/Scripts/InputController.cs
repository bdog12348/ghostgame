using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float, float> { }

[Serializable]
public class AttemptPossessEvent : UnityEvent { }

public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostObject;

    private PlayerControls playerControls;
    public MoveInputEvent moveInputEvent;
    public AttemptPossessEvent attemptPossessEvent;

    bool switchObject;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        switchObject = true;
    }

    private void Update()
    {
        if(switchObject)
        {
            // Ghost Controls
            if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Player")
            {
                playerControls.Ghost.Enable();
                playerControls.Ghost.Move.performed += MoveEvent;
                playerControls.Ghost.Move.canceled += MoveEvent;
                playerControls.Ghost.Possess.performed += AttemptPossess;
                //playerControls.Ghost.Possess.canceled += AttemptPossess;
            }
            else
            {
                playerControls.Ghost.Disable();
            }

            // Broom Controls
            if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Broom")
            {
                playerControls.Broom.Enable();
                Debug.Log("Enabled broom");
                playerControls.Broom.Move.performed += MoveEvent;
                playerControls.Broom.Move.canceled += MoveEvent;
                playerControls.Broom.Possess.performed += AttemptPossess;
            }
            else
            {
                playerControls.Broom.Disable();
            }

            // Dustpan Controls
            if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Dustpan")
            {
                playerControls.Dustpan.Enable();
                Debug.Log("Enabled dustpan");
                playerControls.Dustpan.Move.performed += MoveEvent;
                playerControls.Dustpan.Move.canceled += MoveEvent;
                playerControls.Dustpan.Possess.performed += AttemptPossess;
            }
            else
            {
                playerControls.Dustpan.Disable();
            }

            // Roomba Controls
            if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Roomba")
            {
                playerControls.Roomba.Enable();
                Debug.Log("Enabled roomba");
                playerControls.Roomba.Move.performed += MoveEvent;
                playerControls.Roomba.Move.canceled += MoveEvent;
                playerControls.Roomba.Possess.performed += AttemptPossess;
            }
            else
            {
                playerControls.Roomba.Disable();
            }

            switchObject = false;
        }
    }

    private void MoveEvent(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
    }

    private void AttemptPossess(InputAction.CallbackContext context)
    {
        Debug.Log("Attempting possession");
        attemptPossessEvent.Invoke();
        switchObject = true;
    }

}
