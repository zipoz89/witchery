using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCameraObject : MonoBehaviour
{

    public GameObject targetObj;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    //moveCameraObject cameraObejct;
    void Start()
    {
        //cameraObejct = targetObj.GetComponent<moveCameraObject>();
    }

    void LateUpdate()
    {
        SmothFollow();
    }

    private void SmothFollow()
    {
        Vector3 targetPosition = targetObj.transform.TransformPoint(new Vector3(0, 0, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
