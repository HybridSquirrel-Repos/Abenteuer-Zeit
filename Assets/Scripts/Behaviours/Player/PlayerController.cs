﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [Header("Scripts")]
    public PlayerMovementBehaviour movementScript;
    public DimensionSwitch dimensionSwitch;
    private TwoWayPlatform twoWayPlatform;


    //Player ID
    private int playerID;

    [Header("Sub Behaviours")]
    public PlayerMovementBehaviour playerMovementBehaviour;
    public PlayerAnimationBehaviour playerAnimationBehaviour;
    public PlayerVisualsBehaviour playerVisualsBehaviour;


    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;

    //Action Maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    //Current Control Scheme
    private string currentControlScheme;

    //Setup TwowaysPlatform
    private void Awake()
    { twoWayPlatform = GameObject.Find("PlatformManager").GetComponent<TwoWayPlatform>(); }

    //This is called from the GameManager; when the game is being setup.
    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;

        currentControlScheme = playerInput.currentControlScheme;

        playerMovementBehaviour.SetupBehaviour();
        playerAnimationBehaviour.SetupBehaviour();
        playerVisualsBehaviour.SetupBehaviour(playerID, playerInput);
    }


    //INPUT SYSTEM ACTION METHODS --------------

    //This is called from PlayerInput; when a joystick or arrow keys has been pushed.
    //It stores the input Vector as a Vector3 to then be used by the smoothing function.


    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    //This is called from PlayerInput, when a button has been pushed, that corresponds with the 'Attack' action
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateAttack();
        }
    }

    //This is called from Player Input, when a button has been pushed, that correspons with the 'TogglePause' action
    public void OnTogglePause(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            GameManager.Instance.TogglePauseState(this);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            UpdateJump(true);
        }

        if (value.canceled)
        {
            UpdateJump(false);
        }
    }

    public void OnDimentionSwitching(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateDimenstionSwitching();
        }
    }

    public void OnJumpDown(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateOnJumpDown();
        }
    }
    
    public void OnDash(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateOnDash();
        }
    }

    //INPUT SYSTEM AUTOMATIC CALLBACKS --------------

    //This is automatically called from PlayerInput, when the input device has changed
    //(IE: Keyboard -> Xbox Controller)
    public void OnControlsChanged()
    {

        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;

            playerVisualsBehaviour.UpdatePlayerVisuals();
            RemoveAllBindingOverrides();
        }
    }

    //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
    //IE: Device unplugged or has run out of batteries



    public void OnDeviceLost()
    {
        playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
    }


    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
        playerVisualsBehaviour.UpdatePlayerVisuals();
    }






    //Update Loop - Used for calculating frame-based data
    void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimationMovement();
    }

    //Input's Axes values are raw


    void CalculateMovementInputSmoothing()
    {

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);

    }

    void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(rawInputMovement);
    }

    void UpdateAttack()
    {
        //script for damage or something similar 
        playerAnimationBehaviour.PlayAttackAnimation();
    }

    void UpdateJump(bool value)
    {
        if (value) playerAnimationBehaviour.PlayJumpAnimation();
        movementScript.UpdateJump(value);
    }

    void UpdateDimenstionSwitching()
    {
        transform.SetParent(null, true); //Fix the disappearance of player when standing on moving platform
        playerAnimationBehaviour.PlayDimenstionSwitchingAnimation();
        dimensionSwitch.UpdateDimentionSwitching();
        
    }

    void UpdateOnJumpDown()
    {
        movementScript.TwoWayUpdate();
        twoWayPlatform.buttonPressed = true;

    }

    void UpdateOnDash()
    {
        playerAnimationBehaviour.PlayDashAnimation();
        movementScript.Dash();
    }






    void UpdatePlayerAnimationMovement()
    {
        playerAnimationBehaviour.UpdateMovementAnimation(rawInputMovement.x);
    }


    public void SetInputActiveState(bool gameIsPaused)
    {
        switch (gameIsPaused)
        {
            case true:
                playerInput.DeactivateInput();
                break;

            case false:
                playerInput.ActivateInput();
                break;
        }
    }

    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }



    //Switching Action Maps ----



    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }


    //Get Data ----
    public int GetPlayerID()
    {
        return playerID;
    }

    public InputActionAsset GetActionAsset()
    {
        return playerInput.actions;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    
}
