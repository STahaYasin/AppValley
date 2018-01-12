using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSuper
{
    public GameObject Scene;
    public float DelayToAutoAction = 0f;
}

[System.Serializable]
public class ActionModel: ActionSuper
{
    public string tag = "no tag action";
    
    public bool AutoAfterDelay = false;

    public bool ActionAtCommingCloser = false;
    public GameObject point;
    public float Distance = 0f;
}
[System.Serializable]
public class ActionModel_
{
    public GameObject Scene;

    public bool ActionWithRay_Enabled = false;
    public string ActionWithRay_Tag = "no tag action";

    public bool AutoAction_Enabled = false;
    public float AutoAction_Delay = 0f;

    public bool ActionWhenMoved = false;
    public bool ActionCommingCloser = false;
    public bool ActionGettingFurther = false;
    public bool ActionWhenCloseEnaugh = false;
    public bool ActionWhenFarEnouf = false;
    public float ActionDistance_Distance = 0f;
    public float ActionDistance_DistanceAtStart = 0f;
    public GameObject ActionDistance_Point;

    public bool PlayAudio = false;
    public float PlayAudio_Delay = 0f;
    public AudioSource PlayAudio_Source;
    public AudioClip PLayAudio_Audio;

    public bool PlayAnimation = false;
    public float PlayAnimation_Delay = 0f;
    public AnimationClip PlayAnimation_Animation;
}
[System.Serializable]
public class MoveActionodel: ActionSuper
{
    public bool enabled = false;
    public bool actionAtComingCloser = false;
    public bool chackForDistance = false;
    public float MaxDisToMove = 0f;
    public GameObject DistanceCheckPoint;
}
[System.Serializable]
public class PlayAudio
{
    public bool enabled = false;
    public float delayInSec = 0.5f;
    public AudioClip Audio;
    public AudioSource Source;
}

[System.Serializable]
public class player_scenario_holder : MonoBehaviour {

    public bool userStaysAtOldPos = false;
    public GameObject MoveUserToPoint;

    //public PlayAudio PlayAudioAtBegin;

    //public ActionModel ActionPos = new ActionModel() { tag = "pos"};
    //public ActionModel ActionNeg = new ActionModel() { tag = "neg" };
   // public MoveActionodel ActionMove = new MoveActionodel();

    public ActionModel_[] Actions;

    public bool PlaysAudio = false;
    public AudioClip audio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DestroyThis()
    {
        Destroy(this);
    }
}