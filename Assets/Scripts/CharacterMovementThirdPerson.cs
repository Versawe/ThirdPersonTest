using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementThirdPerson : MonoBehaviour
{
    private CharacterController cc;

    private float playerSpeed = 6.5f;

    private bool jumpOnce = false;

    Vector3 inputDir;

    private float jumpSpeed = 8.5f;
    private float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;

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

    private void MovePlayer()
    {
        float v = Input.GetAxisRaw("Vertical");

        float h = Input.GetAxisRaw("Horizontal");

        inputDir = transform.forward * v + transform.right * h;

        cc.SimpleMove(inputDir * playerSpeed);


        if (h != 0 || v != 0)
        {
            //transform.forward = cam.forward;

            Quaternion camRot = Quaternion.Euler(0, cam.eulerAngles.y, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, 0.1f);
        }
    }

    private void JumpPlayer()
    {
        if (!jumpOnce && Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
            jumpOnce = true;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
        if (cc.isGrounded) jumpOnce = false;
    }
}
