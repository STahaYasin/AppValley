using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_scenario_controller : MonoBehaviour {

    public GameObject RayPoint;
    private RaycastHit hit;
    public int RayRange = 250;

    public GameObject SceneToInstanciate;
    public GameObject InstanciatedScene;

    player_scenario_holder holder;
	
	void Start () {
        hit = new RaycastHit();

        InstanciateScene();
	}
	void Update () {

        if (holder == null) return;
        CheckRayHit();
	}
    
    void InstanciateScene()
    {
        InstanciatedScene = Instantiate(SceneToInstanciate, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        OnInstanciatedScene();
    }
    void OnInstanciatedScene()
    {
        holder = InstanciatedScene.GetComponent<player_scenario_holder>();

        Debug.Log(holder.GetPos().tag);
    }
    void CheckRayHit()
    {
        if(RayPoint == null)
        {
            Debug.LogWarning("RayPoint == null!!");
            return;
        }

        if(Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, RayRange))
        {
            if(hit.transform.gameObject.tag == holder.ActionPos.tag)
            {
                DrawRay(Color.green);
                HitPos();
            }
            else if (hit.transform.gameObject.tag == holder.ActionNeg.tag)
            {
                DrawRay(Color.red);
                HitNeg();
            }
            else
            {
                DrawRay(Color.yellow);
            }
        }
        else
        {
            DrawRay(Color.blue);
        }
    }
    void DrawRay(Color color)
    {
        Debug.DrawRay(RayPoint.transform.position, RayPoint.transform.forward * RayRange, color);
    }
    void HitPos()
    {
        Hit(holder.ActionPos);
    }
    void HitNeg()
    {
        Hit(holder.ActionNeg);
    }
    void Hit(ActionModel action)
    {
        SceneToInstanciate = action.Scene;
        InstanciateScene();
    }
}