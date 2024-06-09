using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ARCameraSetup : MonoBehaviour
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogError("카메라 컴포넌트가 없어요.");
            return;
        }
        
        var trackedPoseDriver = gameObject.AddComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
        trackedPoseDriver.positionAction = new InputAction("Position", InputActionType.PassThrough,
            "<XRHMD>/centerEyePosition");
        trackedPoseDriver.rotationAction = new InputAction("Rotation", InputActionType.PassThrough,
            "<XRHMD>/centerEyeRotation");
    }

}
