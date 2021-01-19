using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")] 
    public PlayerMovement movementScript;
    public DimensionSwitch dimensionSwitch;
    public TwoWayPlatform twoWayPlatform;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;
    
    //Action maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";
    
    
    //Current Control scheme
    private string currentControlScheme;
    
    //Callbacks
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector2(inputMovement.x, 0);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateJump();
        }
    }

    public void OnDimentionSwitching(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateDimenstionSwitching();
        }
    } 
    
    public void OnTogglePause(InputAction.CallbackContext value)
    {
            
    }

    public void OnJumpDown(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            UpdateOnJumpDown();
        }
    }
    
    // Update is called once per frame
    void Update()
    {

      UpdatePlayerMovement();
      //animation
    }
    
    //Processing data
    void UpdatePlayerMovement()
    {
        movementScript.UpdateMovementData(rawInputMovement);
    }
    
    void UpdateJump()
    {
        movementScript.UpdateJump();
    }

    void UpdateDimenstionSwitching()
    {
        dimensionSwitch.UpdateDimentionSwitching();
    }

    void UpdateOnJumpDown()
    {
        movementScript.TwoWayUpdate();
        twoWayPlatform.buttonPressed = true;

    }
    
    //Controller changed

    public void OnControllerChanged()
    {
        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;
            //Update Visuals
            RemoveAllBindingOverrides();
        }
    }
    
    //Switching Action maps
    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }
    
    
    
    //Device disconnect methods

    public void OnDeviceLost()
    {
        //update visuals
    }

    public void OnDeviceRegained()
    {
        StartCoroutine(WaitForDeviceToBeRegained());
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
        //Update visuals
        
    }

    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }
}