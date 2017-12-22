using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenariocontroller : MonoBehaviour
{
    public GameObject nextPosScenario;
    public GameObject nextNegScenario;

    public bool autoNextEnabled = false;
    public float autoNextTimer = 0;

    public GameObject UserPoint;
    private GameObject InstanctiatedScenario;

    public Animation nextPosAnimation;
    public Animation nextNegAnimation;

    private string pos_tag_name = "object-pos";
    private string neg_tag_name = "object-neg";

    public GameObject rayPoint;


    void Start()
    {
        if (UserPoint == null) return;

        GameObject user = GameObject.FindGameObjectWithTag("Player");
        if (user == null)
        {
            Debug.Log("User not found!");
            return;
        }

        user.transform.position = UserPoint.transform.position;
        user.transform.rotation = UserPoint.transform.rotation;
    }

    void goToNext()
    {

    }
    void setupScenarioPos()
    {
        setupScenario(nextPosScenario);
    }
    void setupScenarioNeg()
    {
        setupScenario(nextNegScenario);
    }
    void cannotSetupScenarion()
    {
        Debug.Log("Scenario was null and cannot be instanciated!");
    }
    void setupScenario(GameObject nextScenario)
    {
        if(nextScenario == null)
        {
            cannotSetupScenarion();
            return;
        }

        Vector3 pos = new Vector3() { x = 0, y = 0, z = 0 };
        Quaternion qua = new Quaternion() { x = 0, y = 0, z = 0 };

        InstanctiatedScenario = Instantiate(nextScenario, pos, qua);

        Destroy(gameObject);
    }


    void Update()
    {
        if (autoNextEnabled)
        {
            autoNextTimer -= Time.deltaTime;
            if (autoNextTimer < 0)
            {
                setupScenarioPos();
            }
        }

        if (rayPoint != null)
        {
            checkCollition();
        }
    }
    void checkCollition()
    {
        RaycastHit Hit = new RaycastHit();

        if (Physics.Raycast(rayPoint.transform.position, rayPoint.transform.forward, out Hit, 250))
        {
            Debug.Log("ray");
            Debug.DrawRay(rayPoint.transform.position, rayPoint.transform.forward * 250, Color.yellow);

            if (Hit.transform.gameObject.tag == pos_tag_name && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("P");
                setupScenarioPos();
            }
            else if (Hit.transform.gameObject.tag == neg_tag_name)
            {
                Debug.Log("N");
                setupScenarioNeg();
            }
        }
    }
}