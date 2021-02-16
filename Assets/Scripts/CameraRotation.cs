using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private Camera cam;

    private float yawSensitivity = 8.5f;
    private float pitchSensitivity = 5.5f;


    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = target.position;

        RotateCamera();
    }

    private void RotateCamera()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        yaw += mx * yawSensitivity;
        pitch -= my * pitchSensitivity;

        float pitch_clamped = Mathf.Clamp(pitch, -45f, 45f);

        transform.rotation = Quaternion.Euler(pitch_clamped, yaw, 0);
    }
}
