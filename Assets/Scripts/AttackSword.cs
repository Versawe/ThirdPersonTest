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

    SwordFollow follow;

    // Start is called before the first frame update
    void Start()
    {
        swordPosition = transform.position;
        startPosition = transform.localPosition;

        follow = GetComponent<SwordFollow>();

        point1 = new Vector3(startPosition.x+0.5f, startPosition.y, startPosition.z);
        point2 = new Vector3(startPosition.x, startPosition.y+0.5f, startPosition.z+0.15f);
        point3 = new Vector3(startPosition.x - 0.75f, startPosition.y, startPosition.z+0.17f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AttackOne();
        }

        transform.localPosition = follow.swordLocPos.position + swordPosition;
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
        print(swingLocations1);
    }
}