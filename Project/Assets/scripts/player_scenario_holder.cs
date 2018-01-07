using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionModel
{
    public string tag;
    public GameObject Scene;
}

public class player_scenario_holder : MonoBehaviour {

    public ActionModel ActionPos = new ActionModel() { tag = "pos"};
    public ActionModel ActionNeg = new ActionModel() { tag = "neg" };

    public bool PlaysAudio = false;
    public AudioClip audio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public ActionModel GetPos()
    {
        return ActionPos;
    }
    public ActionModel GetNeg()
    {
        return ActionNeg;
    }
}