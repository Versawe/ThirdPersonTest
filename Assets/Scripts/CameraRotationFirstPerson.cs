using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationFirstPerson : MonoBehaviour
{
    private Camera cam;

    private float yawSensitivity = 9.5f;
    private float pitchSensitivity = 6.5f;


    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Transform target;

    private Animator an;
    private bool run;
    private bool left;
    private bool right;
    private bool idle;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // gets main camera
        cam = Camera.main;

        player = GameObject.FindGameObjectWithTag("Player");

        an = player.GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //has rig set to focus on character
        Vector3 targetPositionFixed = new Vector3(target.position.x, target.position.y +0.4f, target.position.z);
        transform.position = targetPositionFixed;

        RotateCamera();
    }

    private void RotateCamera()
    {
        //saves axis movement of x and y mouse movement
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // yaw and pitch values change determined on 
        // mousex and mousey movement and applied sensitivity to both
        yaw += mx * yawSensitivity;
        pitch -= my * pitchSensitivity;

        //clamp pitch, so camera doesn't rotate too far low or high to seem weird
        float pitch_clamped = Mathf.Clamp(pitch, -89f, 45f);

        // use the clamped pitch and yaw to rotate camera rig, entered in as euler angles through Quaternion class 
        transform.rotation = Quaternion.Euler(pitch_clamped, yaw, 0);

        if (mx != 0 && !player.GetComponent<CharacterMovementFirstPerson>().isMoving)
        {
            run = true;
            left = false;
            right = false;
            idle = false;

            an.SetBool("IsRun", run);
            an.SetBool("IsLeft", left);
            an.SetBool("IsRight", right);
            an.SetBool("IsIdle", idle);
        }
        if (mx == 0 && !player.GetComponent<CharacterMovementFirstPerson>().isMoving)
        {
            run = false;
            left = false;
            right = false;
            idle = true;

            an.SetBool("IsRun", run);
            an.SetBool("IsLeft", left);
            an.SetBool("IsRight", right);
            an.SetBool("IsIdle", idle);
        }
    }
}
