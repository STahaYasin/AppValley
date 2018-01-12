using System;
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
    private List<Coroutine> cors = new List<Coroutine>();

    private GameObject thiss;

    void Start()
    {
        thiss = this.gameObject;
        hit = new RaycastHit();

        InstanciateScene();
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

        if (holder.MoveUserToPoint != null && !holder.userStaysAtOldPos)
        {
            gameObject.transform.position = holder.MoveUserToPoint.transform.position;
            //TODO take the quaternion to;
        }

        foreach(ActionModel_ action in holder.Actions)
        {
            if (action == null) continue;
            //Play audio per action
            if(action.PlayAudio)
            {
                PlayAudioAtBegin(action);
            }

            //Auto actions
            if (action.AutoAction_Enabled)
            {
                Coroutine c = StartCoroutine(WaitAndStartAction(action));
                cors.Add(c);
            }

            //Action when moved, set distance
            if (action.ActionWhenMoved)
            {
                action.ActionDistance_DistanceAtStart = Vector3.Distance(this.transform.position, action.ActionDistance_Point.transform.position);
            }

            //Animation
            if (action.PlayAnimation)
            {
                Coroutine c = StartCoroutine(WaitAndPlayAnimation(action));
                cors.Add(c);
            }
        }
    }
    void Update()
    {

        if (holder == null) return;

        foreach (ActionModel_ action in holder.Actions)
        {
            if (action == null) continue;

            if (action.ActionWithRay_Enabled)
            {
                CheckRayHit(action);
            }

            if (action.ActionWhenMoved)
            {
                CheckDistance(action);
            }
        }
    }
    void PlayAudioAtBegin(ActionModel_ pl)
    {
        if (pl.PLayAudio_Audio == null || pl.PlayAudio_Source == null) return;

        IEnumerator cor = WaitAndPlayAudio(pl);
        StartCoroutine(cor);

    }
    void PlayAudio(ActionModel_ pl)
    {
        pl.PlayAudio_Source.PlayOneShot(pl.PLayAudio_Audio);
    }
    void CheckDistance(ActionModel_ action)
    {
        float newDistance = Vector3.Distance(this.transform.position, action.ActionDistance_Point.transform.position);

        if ((action.ActionWhenCloseEnaugh && newDistance <= action.ActionDistance_Distance) || (action.ActionWhenFarEnouf && newDistance > action.ActionDistance_Distance) || (action.ActionCommingCloser && newDistance + action.ActionDistance_Distance < action.ActionDistance_DistanceAtStart) || (action.ActionGettingFurther && newDistance - action.ActionDistance_Distance > action.ActionDistance_DistanceAtStart))
        {
            Debug.Log("Distance check");
            foreach (Coroutine cor in cors)
            {
                if (cor == null) continue;
                StopCoroutine(cor);
            }
            StopAllCoroutines();

            Hit(action);
        }
    }
    void CheckRayHit(ActionModel_ action)
    {
        if (action == null || RayPoint == null) return;

        if(Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, RayRange))
        {
            if(hit.transform.gameObject.tag == action.ActionWithRay_Tag)
            {
                DrawRay(Color.red);
                Hit(action);
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
    void Hit(ActionModel_ action)
    {
        foreach(Coroutine cor in cors)
        {
            if (cor == null) continue;
            StopCoroutine(cor);
        }
        StopAllCoroutines();

        SceneToInstanciate = action.Scene;
        InstanciateScene();
    }
    IEnumerator WaitAndPlayAudio(ActionModel_ pl)
    {
        yield return new WaitForSeconds(pl.PlayAudio_Delay);
        PlayAudio(pl);
    }
    IEnumerator WaitAndStartAction(ActionModel_ pl)
    {
        yield return new WaitForSeconds(pl.AutoAction_Delay);
        Hit(pl);
    }
    IEnumerator WaitAndPlayAnimation(ActionModel_ pl)
    {
        yield return new WaitForSeconds(pl.PlayAudio_Delay);
        Coroutine c = StartCoroutine(StartAnim(pl));
        cors.Add(c);
        //playArtikLan(pl);
    }
    IEnumerator StartAnim(ActionModel_ pl)
    {
        Animation anim = null;

        if (pl.Animator != null)
        {
            anim = pl.Animator;
        }
        else
        {
            anim = GameObject.FindGameObjectWithTag(pl.OrTag).GetComponent<Animation>();
        }

        if(anim != null)
        {
            anim.Play(pl.PlayAnimation_Animation.name);
        }
        
        yield return new WaitForSeconds(pl.PlayAnimation_Animation.length);

        if (pl.ActionAfterAnimation)
        {
            yield return new WaitForSeconds(pl.ActionAfterAnimationDelay);
            Hit(pl);
        }
    }
    void playArtikLan(ActionModel_ pl)
    {
        Animation anim = thiss.GetComponent<Animation>();
        anim.Play(pl.PlayAnimation_Animation.name);
    }
}