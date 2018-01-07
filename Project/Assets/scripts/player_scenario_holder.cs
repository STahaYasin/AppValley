using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionModel
{
    public string tag = "no tag action";
    public GameObject Scene;

    public bool AutoAfterDelay = false;
    public float DelayToAutoAction = 0f;
}
[System.Serializable]
public class PlayAudio
{
    public bool enabled = false;
    public float delayInSec = 0.5f;
    public AudioClip Audio;
    public AudioSource Source;
}

public class player_scenario_holder : MonoBehaviour {

    public PlayAudio PlayAudioAtBegin;

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
    public void DestroyThis()
    {
        Destroy(this);
    }
}