using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RagdollScript : MonoBehaviour
{
    public Transform monster;

    private Animator anim;

    public List<Rigidbody> bodies = new List<Rigidbody>();

    Rigidbody selectedLimb;

    [Range(0, 50000)]
    public float thrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        anim = monster.gameObject.GetComponent<Animator>(); // gets animator
        
        foreach(Rigidbody bods in monster.gameObject.GetComponentsInChildren<Rigidbody>()) // grabs each rigibody attached to monster
        {
            bodies.Add(bods);
        }

    }

    // Update is called once per frame
    void Update()
    {
        CameraLookAtMonster();
        BlastOff();
        ComeBack();
    }

    private void ComeBack() //resets monster's position
    {
        if (Input.GetButtonDown("Fire2"))
        {
            anim.enabled = true;
            monster.position = new Vector3(0, 0, 0);
        }
    }

    private void BlastOff() //launchs monster, very funny
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.enabled = false; //need this to activate ragdoll
            selectedLimb = bodies[Random.Range(0, 11)]; //selects random limb to make monster launch

            print(selectedLimb);
            selectedLimb.AddForce((transform.forward + new Vector3(0, 1f, 0)) * thrust, ForceMode.Force); //adds force to make him launch
            //NOTE: I notice that when force is applied to oustside limbs the result can be glitchy,
            //Seems to work better on spine rigidbodies
        }
    }

    private void CameraLookAtMonster() //keeps cam rotated towards the monster
    {
        Vector3 relativePos = transform.position - monster.position;
        relativePos = relativePos + new Vector3(0, -1.75f, 0);
        Quaternion rotation = Quaternion.LookRotation(-relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
