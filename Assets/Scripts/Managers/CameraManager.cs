using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public enum SinglePlayerCameraMode
{
    Stationary,
    FollowAndOrbit,
    TwoDFollow,
}

public class CameraManager : Singleton<CameraManager>
{
    [Header("Component References")]
    public GameObject gameplayCameraObject;
    public GameObject uiOverlayCameraObject;

    [Header("Virtual Cameras")]
    public GameObject VCamStationaryObject;
    public GameObject VCamSinglePlayerOrbitObject;
    public GameObject VCamSinglePlayer2DFollow;

    public void SetupManager()
    {
        SetCameraObjectNewState(gameplayCameraObject, true);
        SetCameraObjectNewState(uiOverlayCameraObject, false);
    }

    public void SetupSinglePlayerCamera(SinglePlayerCameraMode currentCameraMode)
    {
        switch (currentCameraMode)
        {
            case SinglePlayerCameraMode.Stationary:
                SetCameraObjectNewState(VCamStationaryObject, true);
                SetCameraObjectNewState(VCamSinglePlayer2DFollow, false);
                SetCameraObjectNewState(VCamSinglePlayerOrbitObject, false);
                break;

            case SinglePlayerCameraMode.FollowAndOrbit:
                SetCameraObjectNewState(VCamStationaryObject, false);
                SetCameraObjectNewState(VCamSinglePlayer2DFollow, false);
                SetCameraObjectNewState(VCamSinglePlayerOrbitObject, true);
                break;
            
            case SinglePlayerCameraMode.TwoDFollow:
                SetCameraObjectNewState(VCamStationaryObject, false);
                SetCameraObjectNewState(VCamSinglePlayerOrbitObject, false);
                SetCameraObjectNewState(VCamSinglePlayer2DFollow, true);
                break;
        }
    }

    void SetCameraObjectNewState(GameObject cameraObject, bool newState)
    {
        cameraObject.SetActive(newState);
    }

    //This is called by UIBillboardBehaviour so they can orient to wherever the gameplay camera is.
    public Transform GetGameplayCameraTransform()
    {
        return gameplayCameraObject.transform;
    }

    public Camera GetGameplayCamera()
    {
        return gameplayCameraObject.GetComponent<Camera>();
    }

}
