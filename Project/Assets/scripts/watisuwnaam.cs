using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watisuwnaam : MonoBehaviour
{
    public GameObject[] scenarios;
    private GameObject scenario;
    private int isAtScenario = 0;

    private string pos_tag_name = "object-pos";
    private string neg_tag_name = "object-neg";

    public GameObject rayPoint;


    void Start()
    {
        setupScenario();
    }

    void goToNext()
    {
        isAtScenario++;

        setupScenario();
    }
    void setupScenario()
    {
        if (scenarios.Length <= isAtScenario)
        {
            Debug.Log("Reached the end!");
            return;
        }
        if (scenarios[isAtScenario] == null) return;

        Vector3 pos = new Vector3() { x = 0, y = 0, z = 0 };
        Quaternion qua = new Quaternion() { x = 0, y = 0, z = 0 };

        scenario = Instantiate(scenarios[isAtScenario], pos, qua);

        GameObject userPointer;

        int count = scenario.transform.GetChildCount();
        GameObject point = null;

        for(int i = 0; i < count; i++)
        {
            point = (scenario.transform.GetChild(i).tag == "userpoint") ? scenario.transform.GetChild(i).gameObject : null;
        }

        if(point == null)
        {
            Debug.Log("bruh user point is null!");
            return;
        }

        gameObject.transform.position = point.transform.position;
        gameObject.transform.rotation = point.transform.rotation;
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
                Destroy(scenario);

                goToNext();
            }
            else if (Hit.transform.gameObject.tag == neg_tag_name)
            {
                
            }
        }
    }
}
