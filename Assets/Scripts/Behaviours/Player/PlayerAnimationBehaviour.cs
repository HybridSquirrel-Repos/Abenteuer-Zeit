using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    [Header("Component References")]
    public Animator playerAnimator;

    //Animation String IDs
    private int playerMovementAnimationID;
    private int playerAttackAnimationID;
    private int playerJumpAnimationID;
    private int playerDimenstionSwitchingAnimationID;
    private int playerDashID;

    public void SetupBehaviour()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {
        playerMovementAnimationID = Animator.StringToHash("Movement");
        playerAttackAnimationID = Animator.StringToHash("Attack");
        playerJumpAnimationID = Animator.StringToHash("Jump");
        playerDimenstionSwitchingAnimationID = Animator.StringToHash("DimentionSwitching");
        playerDashID = Animator.StringToHash("Dash");
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        playerAnimator.SetFloat(playerMovementAnimationID, movementBlendValue);
        if (movementBlendValue <= -0.1)
        {
            gameObject.transform.localScale = new Vector3(-0.4f, 0.4f, 1f);
        }
        else if (movementBlendValue >= 0.1)
        {
            gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }

    public void PlayAttackAnimation()
    {
        //playerAnimator.SetTrigger(playerAttackAnimationID);
    }

    public void PlayJumpAnimation()
    {
        playerAnimator.SetTrigger(playerJumpAnimationID);
    }

    public void PlayDimenstionSwitchingAnimation()
    {
        playerAnimator.SetTrigger(playerDimenstionSwitchingAnimationID);
    }

    public void PlayDashAnimation()
    {
        playerAnimator.SetTrigger(playerDashID);
    }

}
