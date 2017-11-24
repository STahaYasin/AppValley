using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_LookWalk : MonoBehaviour {

    //VR Main Camera
    public Transform vrCamera;

    //Angle at which walk/stop will trigger.
    public float toggleAngle = 30.0f;
    //How fast to move.
    public float speed = 3.0f;
    //Should I move forward or not?
    public bool moveforward;

    //CharacterController Script
    private CharacterController cc;

	// Use this for initialization
	void Start () {
        //Find the CharacterControll
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //Check to see if the head is rotated down to the toggleAngle, but not more than straight down.
        if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f)
        {
            //Move forward
            moveforward = true;
        } else
        {
            //Stop moving
            moveforward = false;
        }

        //Check to see if I should move.
        if (moveforward == true)
        {
            //Find the forward direction.
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);

            //Tell CharacterController to move forward.
            cc.SimpleMove(forward * speed);
        }
	}
}
