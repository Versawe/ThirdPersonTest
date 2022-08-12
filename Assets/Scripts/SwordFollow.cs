using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFollow : MonoBehaviour
{
    public string swordState = "Follow";

    public Transform cam;

    public Transform swordLocPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = swordLocPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (swordState == "Follow") SwordLocked();   
    }

    private void SwordLocked()
    {
        transform.position = swordLocPos.position;
        transform.rotation = swordLocPos.rotation;
    }
}
