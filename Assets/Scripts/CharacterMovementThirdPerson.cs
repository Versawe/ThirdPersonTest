using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementThirdPerson : MonoBehaviour
{
    private CharacterController cc;

    private float playerSpeed = 6.5f;

    private bool jumpOnce = false;

    Vector3 inputDir;
    Vector3 copyInputVec;
    private bool isMoving = false;

    private float jumpSpeed = 8.5f;
    private float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;

    private float pushPower = 2f;

    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
        //using axis raw to allow for wasd direction to be stored in Vector3 
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        inputDir = transform.forward * v + transform.right * h;

        //applies input vector to apply to char controller
        cc.SimpleMove(inputDir * playerSpeed);

        //this statement rotates the character to be facing away from camera if movement is applied
        if (h != 0 || v != 0)
        {

            Quaternion camRot = Quaternion.Euler(0, cam.eulerAngles.y, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, 0.1f);

            isMoving = true;
        }
        if (isMoving && h != 0 || v != 0)
        {
            copyInputVec = inputDir;
            //Vector3.Lerp(inputDir, new Vector3(0,0,0), 0.01f);
            //print("hello");
        }
        else
        {
            isMoving = false;
        }
        //ON THE RIGHT TRACK FINISH AT KBELLS!! GOOD LUCK!
        print(copyInputVec);
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
