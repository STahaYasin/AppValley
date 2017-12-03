using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_LookWalk : MonoBehaviour {

    //VR Main Camera
    public Transform vrCamera;
    public GameObject text;

    //Angle at which walk/stop will trigger.
    public float toggleAngle = 30.0f;
    //How fast to move.
    public float speed = 3.0f;
    //Should I move forward or not?
    public bool moveforward;

    //CharacterController Script
    private CharacterController cc;
    private TextMesh tt;

	// Use this for initialization
	void Start () {
        //Find the CharacterControll
        cc = GetComponent<CharacterController>();
        tt = text.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        //Check to see if the head is rotated down to the toggleAngle, but not more than straight down.
        //if (tt != null) tt.text = vrCamera.transform.rotation.x + "° " + vrCamera.rotation.y + "° " + vrCamera.rotation.z + "°";
        if (vrCamera.transform.rotation.x >= toggleAngle && vrCamera.transform.rotation.x < 90)
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
            Vector3 forward = gameObject.transform.forward;

            //Tell CharacterController to move forward.
            if (tt != null) tt.text = "voor";
            cc.SimpleMove(forward * speed);
            if (tt != null) tt.text = "na";
        }
	}
}
