using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[Serializable]
public class KeyboardInputEvent : UnityEvent<float, float, bool, int> { }

[Serializable]
public class P1MoveInputEvent : UnityEvent<float, float, bool, int> { }

[Serializable]
public class P1AttemptPossessEvent : UnityEvent { }

[Serializable]
public class P2MoveInputEvent : UnityEvent<float, float, bool, int> { }

[Serializable]
public class P2AttemptPossessEvent : UnityEvent { }

public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameObject p1GhostObject;

    [SerializeField]
    private GameObject p2GhostObject;

    private PlayerControls playerControls;

    public KeyboardInputEvent KeyboardInputEvent;
    public P1MoveInputEvent p1MoveInputEvent;
    public P1AttemptPossessEvent p1AttemptPossessEvent;
    public P2MoveInputEvent p2MoveInputEvent;
    public P2AttemptPossessEvent p2AttemptPossessEvent;

    bool p1SwitchObject;
    bool p2SwitchObject;

    enum CurrentForm {Ghost, Broom, Dustpan, Roomba};
    CurrentForm[] CurrentForms;

    Gamepad[] gamepads;
    int numGamepads;
    bool keyboard = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Ghost.Enable();
        playerControls.Broom.Enable();
        playerControls.Dustpan.Enable();
        playerControls.Roomba.Enable();
    }

    private void OnEnable()
    {
        //If we are not switching an object this frame, we can save a lot of time
        p1SwitchObject = true;
        p2SwitchObject = true;

        //We need to establish which gamepads are already connected to Unity
        gamepads = new Gamepad[] {null, null, null, null};
        foreach (Gamepad g in Gamepad.all)
        {
            gamepads[numGamepads] = g;
            numGamepads += 1;
        }

        //Mark what object each player currently is
        CurrentForms = new CurrentForm[] {CurrentForm.Ghost, CurrentForm.Ghost, CurrentForm.Ghost, CurrentForm.Ghost};
    }

    private void ChangeInputControls(int playerNumber, GameObject ghostObject, 
    Action<InputAction.CallbackContext> MoveEvent, Action<InputAction.CallbackContext> AttemptPossess)
    {
        // Ghost Controls
        if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Player")
        {
            CurrentForms[playerNumber] = CurrentForm.Ghost;
            if (playerNumber == 0 && keyboard)
            {
                playerControls.Ghost.KeyboardMove.performed += KeyboardEvent;
                playerControls.Ghost.KeyboardMove.canceled += KeyboardEvent;
                playerControls.Ghost.Move.performed -= MoveEvent;
                playerControls.Ghost.Move.canceled -= MoveEvent;
            }
            else
            {
                playerControls.Ghost.Move.performed += MoveEvent;
                playerControls.Ghost.Move.canceled += MoveEvent;
                playerControls.Ghost.KeyboardMove.performed -= KeyboardEvent;
                playerControls.Ghost.KeyboardMove.canceled -= KeyboardEvent;
            }
            playerControls.Ghost.Possess.performed += AttemptPossess;
            //playerControls.Ghost.Possess.canceled += AttemptPossess;
        }
        else
        {
            if (playerNumber == 0 && keyboard)
            {
                playerControls.Ghost.KeyboardMove.performed -= KeyboardEvent;
                playerControls.Ghost.KeyboardMove.canceled -= KeyboardEvent; 
            }
            else
            {
                playerControls.Ghost.Move.performed -= MoveEvent;
                playerControls.Ghost.Move.canceled -= MoveEvent;
            }
            playerControls.Ghost.Possess.performed -= AttemptPossess;
        }

        // Broom Controls
        if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Broom")
        {
            CurrentForms[playerNumber] = CurrentForm.Ghost;
            playerControls.Broom.Move.performed += MoveEvent;
            playerControls.Broom.Move.canceled += MoveEvent;
            playerControls.Broom.Possess.performed += AttemptPossess;
        }
        else
        {
            playerControls.Broom.Move.performed -= MoveEvent;
            playerControls.Broom.Move.canceled -= MoveEvent;
            playerControls.Broom.Possess.performed -= AttemptPossess;
        }

        // Dustpan Controls
        if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Dustpan")
        {
            CurrentForms[playerNumber] = CurrentForm.Ghost;
            playerControls.Dustpan.Move.performed += MoveEvent;
            playerControls.Dustpan.Move.canceled += MoveEvent;
            playerControls.Dustpan.Possess.performed += AttemptPossess;
        }
        else
        {
            playerControls.Dustpan.Move.performed -= MoveEvent;
            playerControls.Dustpan.Move.canceled -= MoveEvent;
            playerControls.Dustpan.Possess.performed -= AttemptPossess;
        }

        // Roomba Controls
        if(ghostObject.GetComponent<NewPlayerController>().currentObject.name == "Roomba")
        {
            CurrentForms[playerNumber] = CurrentForm.Roomba;
            playerControls.Roomba.Move.started += MoveEvent;
            playerControls.Roomba.Move.performed += MoveEvent;
            playerControls.Roomba.Move.canceled += MoveEvent;
            playerControls.Roomba.Possess.performed += AttemptPossess;
        }
        else
        {
            playerControls.Roomba.Move.performed -= MoveEvent;
            playerControls.Roomba.Move.canceled -= MoveEvent;
            playerControls.Roomba.Possess.performed -= AttemptPossess;
        }    
    }

    private void Update()
    {
        if(Keyboard.current.kKey.wasPressedThisFrame)
        {
            keyboard = keyboard ? false : true;
            ChangeInputControls(0, p1GhostObject, P1MoveEvent, P1AttemptPossess);
        }

        //If there is a device (gamepad) change, we need to handle it
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    if (gamepads.Contains(device) == false)
                    {
                        gamepads[numGamepads] = (Gamepad)device;
                        numGamepads += 1;
                        Debug.Log(device + "added.");
                    }
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    if (gamepads.Contains(device) == true)
                    {
                        int foundLocation = 0;
                        for(int i = 0; i < gamepads.Length; i++)
                        {
                            if(gamepads[i] == (Gamepad)device)
                                foundLocation = i;
                        }
                        gamepads[foundLocation] = null;
                        numGamepads -= 1;
                        Debug.Log(device + "unplugged.");
                    }
                    break;
                case InputDeviceChange.Reconnected:
                    // Plugged back in.
                    break;
                case InputDeviceChange.Removed:
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };

        if(p1SwitchObject)
        {
            ChangeInputControls(0, p1GhostObject, P1MoveEvent, P1AttemptPossess);
            p1SwitchObject = false;
        }

        if(p2SwitchObject)
        {
            ChangeInputControls(1, p2GhostObject, P2MoveEvent, P2AttemptPossess);
            p2SwitchObject = false;
        }
    }

    private void KeyboardEvent(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        p1MoveInputEvent.Invoke(moveInput.x, moveInput.y, false, 0);
    }

    private void P1MoveEvent(InputAction.CallbackContext context)
    {
        if(gamepads[0] != null)
        {
            bool forward = false;
            bool left = false;
            bool right = false;
            int direction = 0;

            if(CurrentForms[0] == CurrentForm.Roomba)
            {
                forward = gamepads[0].buttonSouth.wasPressedThisFrame;
                left = gamepads[0].leftShoulder.wasPressedThisFrame;
                right = gamepads[0].rightShoulder.wasPressedThisFrame;
                Debug.Log($"{forward}, {left}, {right}");
                if(left && right)
                    direction = 3;
                else if(left)
                    direction = 2;
                else if(right)
                    direction = 1;
            }
            Vector2 moveInput = gamepads[0].leftStick.ReadValue();
            p1MoveInputEvent.Invoke(moveInput.x, moveInput.y, forward, direction);    
        }
    }

    private void P1AttemptPossess(InputAction.CallbackContext context)
    {
        if(gamepads[0] != null && gamepads[0].buttonNorth.wasPressedThisFrame)
        {
            p1AttemptPossessEvent.Invoke();
            p1SwitchObject = true;
        }
    }

    private void P2MoveEvent(InputAction.CallbackContext context)
    {
        if(gamepads[1] != null)
        {
            bool forward = false;
            bool left = false;
            bool right = false;
            int direction = 0;

            if(CurrentForms[1] == CurrentForm.Roomba)
            {
                forward = gamepads[1].buttonSouth.wasPressedThisFrame;
                left = gamepads[1].leftShoulder.wasPressedThisFrame;
                right = gamepads[1].rightShoulder.wasPressedThisFrame;
                if(left && right)
                    direction = 3;
                else if(left)
                    direction = 2;
                else if(right)
                    direction = 1;
            }

            Vector2 moveInput = gamepads[1].leftStick.ReadValue();
            p2MoveInputEvent.Invoke(moveInput.x, moveInput.y, forward, direction);
        } 
    }

    private void P2AttemptPossess(InputAction.CallbackContext context)
    {
        if(gamepads[1] != null && gamepads[1].buttonNorth.wasPressedThisFrame)
        {
            p2AttemptPossessEvent.Invoke();
            p2SwitchObject = true;
        } 
    }
}
