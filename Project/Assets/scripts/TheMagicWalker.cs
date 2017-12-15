using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMagicWalker : MonoBehaviour {

    

    public GameObject _camera;
    public GameObject _pijlPrefab;
    public GameObject _pijlInstance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_camera == null) return;
        if (_pijlPrefab == null) return;

        Vector3 forward = _camera.transform.forward;
        Vector3 rayStart = _camera.transform.position;
        Debug.DrawRay(rayStart, forward * 50, Color.blue);
        RaycastHit hit;
        if(Physics.Raycast(rayStart, forward, out hit, 50))
        {
            if(hit.collider.tag == "floor")
            {
                if (_pijlInstance == null)
                {
                    _pijlInstance = Instantiate(_pijlPrefab, hit.point, Quaternion.LookRotation(forward));
                }

                Vector3 pos = _pijlInstance.transform.position;
                pos.x = hit.point.x;
                pos.z = hit.point.z;
                pos.y = hit.point.y + 0.1f;

                double xdis = hit.point.x - transform.position.x;
                if (xdis == 0) xdis = 0.01f;
                double zdis = hit.point.z - transform.position.z;
                if (zdis == 0) zdis = 0.01f;
                double distance = System.Math.Sqrt(System.Math.Pow(xdis, 2) + System.Math.Pow(zdis, 2)); 
                
                if (distance > 2)
                {
                    _pijlInstance.transform.position = pos;
                    Quaternion rot = _pijlInstance.transform.rotation;
                    rot.x = 0;
                    rot.z = 0;
                    rot.y = _camera.transform.rotation.y;
                    _pijlInstance.transform.rotation = rot;

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Vector3 newPos = hit.point;
                        newPos.y += 1.2f;
                        gameObject.transform.position = newPos;
                    }
                }
                else
                {
                    Debug.Log(distance);
                    pos = hit.point;
                    pos.x = hit.point.x;
                    pos.z = hit.point.z;
                    pos.y = hit.point.y - 15;
                    Debug.Log(""+pos.ToString());

                    _pijlInstance.transform.position = pos;
                }
            }
            else
            {
                Vector3 pos = _pijlInstance.transform.position;
                pos.x = hit.point.x;
                pos.z = hit.point.z;
                pos.y = hit.point.y - 15;

                _pijlInstance.transform.position = pos;
            }
        }
	}
}