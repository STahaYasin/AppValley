using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{

    private string pos_tag_name = "pos";
    private string neg_tag_name = "neg";

    public GameObject rayPoint;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && rayPoint != null)
        {
            ButtonPressed();
        }
    }
    void ButtonPressed()
    {
        RaycastHit Hit = new RaycastHit();

        if (Physics.Raycast(rayPoint.transform.position, rayPoint.transform.forward, out Hit, 250))
        {
            Debug.Log("ray");
            Debug.DrawRay(rayPoint.transform.position, rayPoint.transform.forward * 250, Color.yellow);

            if (Hit.transform.gameObject.tag == pos_tag_name)
            {
                Application.LoadLevel(1);
            }
            else if (Hit.transform.gameObject.tag == neg_tag_name)
            {
                Application.Quit();
            }
        }
    }
}