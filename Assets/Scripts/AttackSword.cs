using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : MonoBehaviour
{
    public Vector3 swordPosition;

    Vector3 startPosition;
    private int swingLocations1 = 0;
    List<Vector3> attackOnePositions;

    Vector3 point1;
    Vector3 point2;
    Vector3 point3;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        swordPosition = transform.localPosition;

        point1 = new Vector3(startPosition.x+0.05f, startPosition.y, startPosition.z);
        point2 = new Vector3(startPosition.x, startPosition.y+0.25f, startPosition.z+0.05f);
        point3 = new Vector3(startPosition.x - 0.15f, startPosition.y, startPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AttackOne();
        }

        transform.localPosition = swordPosition;
    }

    private void AttackOne()
    {
        swingLocations1++;
        if (swingLocations1 == 0) swordPosition = startPosition;
        else if (swingLocations1 == 1) swordPosition = point1;
        else if (swingLocations1 == 2) swordPosition = point2;
        else if (swingLocations1 == 3) swordPosition = point3;
        else 
        {
            swordPosition = startPosition;
            swingLocations1 = 0;            
        }
        //print(swingLocations1);
    }
}