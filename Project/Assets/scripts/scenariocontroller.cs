using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class autoActionWithTimer
{
    public bool enabled = false;
    public float Timer = 0;
    public Animation animation;
    public AudioClip audio;
    public bool NextEnabled = false;
    public float TimerToEnter = 0;
    public GameObject NexInstance;
}
[System.Serializable]
public class actionObject
{
    public GameObject NextInstance;
    public string tag = "";
    public Animation animation;
    public AudioClip audio;
    public float AnimationOrAudioDuration = 0f;
}

[RequireComponent(typeof(AudioSource))]
public class scenariocontroller : MonoBehaviour
{
    public Transform userPos;
    public GameObject UserPoint;
    public GameObject rayPoint;
    private AudioSource audioSource;

    public actionObject ActionPositive = new actionObject() { tag = "object-pos"};
    public actionObject ActionNegative = new actionObject() { tag = "object-neg"};
    public autoActionWithTimer AutoActionWithTimer = new autoActionWithTimer() { };

    private GameObject InstanctiatedScenario;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (UserPoint == null) return;

        GameObject user = GameObject.FindGameObjectWithTag("Player");
        if (user == null)
        {
            Debug.Log("User not found!");
            return;
        }

        userPos = user.GetComponent<Transform>();

        user.transform.position = UserPoint.transform.position;
        user.transform.rotation = UserPoint.transform.rotation;
    }

    void goToNext()
    {

    }
    void setupScenarioPos()
    {
        setupScenario(ActionPositive);
        Destroy(gameObject);
    }
    void setupScenarioNeg()
    {
        if (ActionNegative == null) return;
        setupScenario(ActionNegative);
        //Destroy(gameObject);
    }
    void cannotSetupScenarion()
    {
        Debug.Log("Scenario was null and cannot be instanciated!");
    }
    void setupScenario(actionObject nextObject)
    {
        if (nextObject == null)
        {
            cannotSetupScenarion();
            Debug.Log("NULL ulaaannnnn");
            return;
        }

        if (nextObject.audio != null && audioSource != null) audioSource.PlayOneShot(nextObject.audio);
        // TODO some animatio stuff

        waiter(nextObject.AnimationOrAudioDuration);

        Vector3 pos = new Vector3() { x = 0, y = 0, z = 0 };
        Quaternion qua = new Quaternion() { x = 0, y = 0, z = 0 };

        InstanctiatedScenario = Instantiate(nextObject.NextInstance, pos, qua);
    }


    void Update()
    {
        Debug.Log("Update");
        if (AutoActionWithTimer.NextEnabled)
        {
            AutoActionWithTimer.TimerToEnter -= Time.deltaTime;
            if (AutoActionWithTimer.TimerToEnter < 0)
            {
                setupScenarioPos();
            }
        }

        checkCollition();
    }
    void checkCollition()
    {
        RaycastHit Hit = new RaycastHit();
        Debug.Log(rayPoint.transform.rotation.y);
        Vector3 forward = rayPoint.transform.forward;
        Debug.Log(forward.ToString());
        if (Physics.Raycast(rayPoint.transform.position, forward, out Hit, 250))
        {
            Debug.Log("ray");
            Debug.DrawRay(rayPoint.transform.position, rayPoint.transform.forward * 250, Color.yellow);

            if (Hit.transform.gameObject.tag == "object-pos")
            {
                //double distance = Vector3.Distance(Hit.transform.position, rayPoint.transform.position);
                Debug.Log("P");
                setupScenarioPos();
            }
            else if (Hit.transform.gameObject.tag == "object-neg")
            {
                Debug.Log("N");
                setupScenarioNeg();
            }
        }
    }

    IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
    }
}