using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raytestrer : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = gameObject.transform.forward;
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(transform.position, forward, out hit, 200))
        {
            if(hit.transform.gameObject.tag == "object-neg")
            {
                Renderer ren = obj.GetComponent<Renderer>();
                ren.material.color = Color.yellow;
            }
        }
	}
}