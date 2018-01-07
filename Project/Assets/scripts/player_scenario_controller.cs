using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_scenario_controller : MonoBehaviour {

    public GameObject RayPoint;
    private RaycastHit hit;
    public int RayRange = 250;

    public float DistanceToCheckPoint = 0;

    public GameObject SceneToInstanciate;
    public GameObject InstanciatedScene;

    player_scenario_holder holder;

    private Coroutine cor;
	
	void Start () {
        hit = new RaycastHit();

        InstanciateScene();
	}
	void Update () {

        if (holder == null) return;
        CheckRayHit();

        if(holder.ActionMove != null && holder.ActionMove.enabled)
        {
            CheckDistance();
        }
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

        if (holder.MoveUserToPoint != null)
        {
            gameObject.transform.position = holder.MoveUserToPoint.transform.position;
            //TODO take the quaternion to;
        }

        if(holder.PlayAudioAtBegin != null && holder.PlayAudioAtBegin.enabled)
        {
            PlayAudioAtBegin(holder.PlayAudioAtBegin);
        }

        if (holder.ActionNeg.AutoAfterDelay)
        {
            cor = StartCoroutine(waitForNewAction(holder.ActionNeg));
        }
        if (holder.ActionPos.AutoAfterDelay)
        {
            cor = StartCoroutine(waitForNewAction(holder.ActionPos));
        }

        if(holder.ActionMove != null && holder.ActionMove.enabled)
        {
            DistanceToCheckPoint = Vector3.Distance(this.transform.position, holder.ActionMove.DistanceCheckPoint.transform.position);
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
    void CheckDistance()
    {
        float newDistance = Vector3.Distance(this.transform.position, holder.ActionMove.DistanceCheckPoint.transform.position);
        if((holder.ActionMove.actionAtComingCloser && (newDistance + holder.ActionMove.MaxDisToMove) < DistanceToCheckPoint) || (!holder.ActionMove.actionAtComingCloser && (newDistance - holder.ActionMove.MaxDisToMove) > DistanceToCheckPoint))
        {
            Debug.Log("User moved like expected to do an action!");
            StopCoroutine(cor);
            StartCoroutine(waitForNewAction(holder.ActionMove));
        }
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
    void Hit(ActionSuper action)
    {
        StopCoroutine(cor);
        SceneToInstanciate = action.Scene;
        InstanciateScene();
    }
    IEnumerator waiter(float sec, PlayAudio pl)
    {
        yield return new WaitForSeconds(sec);
        PlayAudio(pl);
    }
    IEnumerator waitForNewAction(ActionSuper pl)
    {
        yield return new WaitForSeconds(pl.DelayToAutoAction);
        Debug.Log("fvbfgbjk");
        Hit(pl);
    }
}