using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController cc;

    private float playerSpeed = 5;

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
    }

    private void MovePlayer()
    {
        float v = Input.GetAxisRaw("Vertical");

        float h = Input.GetAxisRaw("Horizontal");

        Vector3 inputDir = transform.forward * v + transform.right * h;

        cc.SimpleMove(inputDir * playerSpeed);

        if (h != 0 || v != 0)
        {
            //transform.forward = cam.forward;

            Quaternion camRot = Quaternion.Euler(0, cam.eulerAngles.y, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, camRot, 0.1f);
        }
    }
}
