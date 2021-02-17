using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementThirdPerson : MonoBehaviour
{
    private CharacterController cc;
    private Animator an;

    private float playerSpeed = 6.5f;

    Vector3 inputDir;
    Vector3 copyInputVec;
    private bool isMoving = false;
    private float skidDelay = 0.15f;
    private float skidLength = 0.75f;

    private float jumpSpeed = 8.5f;
    private float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    private bool jumpOnce = false;

    private float pushPower = 2f;

    private bool run;
    private bool left;
    private bool right;
    private bool idle;

    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        an = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    //movement for player
    private void MovePlayer()
    {
        //gets input from WASD using Unity's Vertical and Horizontal axis's
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        //applies this gameobjects forward and right vector to a vector 3 depending on the axis's numbers
        // Example: w = 1, s = -1, a = -1, d = 1
        inputDir = transform.forward * v + transform.right * h;

        //Animation Triggers below, should come up with a function to shorten the length of each statement in the FUTURE
        //animations for running, forward, left and right
        if (v != 0) //forward
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
        else if (h < 0) // left
        {
            run = false;
            left = true;
            right = false;
            idle = false;

            an.SetBool("IsRun", run);
            an.SetBool("IsLeft", left);
            an.SetBool("IsRight", right);
            an.SetBool("IsIdle", idle);
        }
        else if (h > 0) // right
        {
            run = false;
            left = false;
            right = true;
            idle = false;

            an.SetBool("IsRun", run);
            an.SetBool("IsLeft", left);
            an.SetBool("IsRight", right);
            an.SetBool("IsIdle", idle);
        }
        else if(h > 0 || h < 0 && v !=0) // angle aka forward
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
        //animation for idle
        if (h == 0 && v == 0)
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

        //skidding functionality
        if (isMoving)
        {
            if (h != 0 || v != 0)
            {
                copyInputVec = inputDir;
            }
            else
            {
                inputDir = Vector3.Lerp(copyInputVec, new Vector3(0, 0, 0), skidLength);
                skidDelay -= Time.deltaTime;
                if (skidDelay <= 0) isMoving = false;
            }
        }
        if (!isMoving)
        {
            skidDelay = 0.15f;
        }

        //applies input vector to apply to char controller
        cc.SimpleMove(inputDir * playerSpeed);

        //this statement rotates the character to be facing away from camera if movement is applied
        if (h != 0 || v != 0)
        {

            Quaternion camRot = Quaternion.Euler(0, cam.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, 0.1f);
            isMoving = true;
        }
    }

    //player jump ability
    private void JumpPlayer()
    {
        //checks if spacebar clicked and only triggable once
        if (!jumpOnce && Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
            jumpOnce = true;
        }
        //applies gravity and move ability for char controller
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        //if on ground locks jumponce to be sure to allow character to jump again
        if (cc.isGrounded) jumpOnce = false;
    }

    //pushes block
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitBody = hit.collider.attachedRigidbody;

        if(hitBody == null || hitBody.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        hitBody.velocity = pushDir * pushPower;
    }
}
