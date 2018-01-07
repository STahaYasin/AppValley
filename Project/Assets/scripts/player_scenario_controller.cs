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
        if (InstanciatedScene != null) Destroy(InstanciatedScene);
        InstanciatedScene = Instantiate(SceneToInstanciate, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        OnInstanciatedScene();
    }
    void OnInstanciatedScene()
    {
        holder = InstanciatedScene.GetComponent<player_scenario_holder>();

        if(holder.PlayAudioAtBegin != null && holder.PlayAudioAtBegin.enabled)
        {
            PlayAudioAtBegin(holder.PlayAudioAtBegin);
        }

        if (holder.ActionNeg.AutoAfterDelay)
        {
            StartCoroutine(waitForNewAction(holder.ActionNeg.DelayToAutoAction, holder.ActionNeg));
        }
        if (holder.ActionPos.AutoAfterDelay)
        {
            StartCoroutine(waitForNewAction(holder.ActionPos.DelayToAutoAction, holder.ActionPos));
        }

        Debug.Log(holder.GetPos().tag);
    }
    void PlayAudioAtBegin(PlayAudio pl)
    {
        if (pl.Audio == null || pl.Source == null) return;

        IEnumerator cor = waiter(pl.delayInSec, pl);
        StartCoroutine(cor);

    }
    void PlayAudio(PlayAudio pl)
    {
        pl.Source.PlayOneShot(pl.Audio);
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
    IEnumerator waiter(float sec, PlayAudio pl)
    {
        yield return new WaitForSeconds(sec);
        PlayAudio(pl);
    }
    IEnumerator waitForNewAction(float sec, ActionModel pl)
    {
        yield return new WaitForSeconds(sec);
        Hit(pl);
    }
}