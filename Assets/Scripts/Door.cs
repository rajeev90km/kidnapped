﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Door : MonoBehaviour
{

    public GameObject _oneHand;
    public GameObject _otherHand;

    public GameObject idCardObject;

    private bool moveDoor = false;
    private int direction = 1;

    public GameObject ropeObj;

    float lastPosition = 0;
    private GameObject handObj;

    public GameObject doorObj;
    public GameObject HospitalScene;
    public GameObject EscapeScene;

    bool _oneTimeCall;

    //private bool isCardPrinted = false;

    private bool doorUnlockedSoundPlayed = false;


    private AudioSource[] aSources;

    private bool rotating = false;
    private bool isRopeUntied = false;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    void Awake()
    {
        aSources = GetComponents<AudioSource>();
    }

    void Start()
    {
        _oneTimeCall = false;
        StartCoroutine(doorMover());
    }

    void FixedUpdate()
    {
        if (trackedObj)
            device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void Update()
    {
        //if (transform.parent.localEulerAngles.y >= 150f)
        //{
        //    if (!isCardPrinted)
        //    {
        //        idCardObject.GetComponent<PrintCard>()._printCard = true;
        //        isCardPrinted = true;
        //    }
        //}

        isRopeUntied = ropeObj.GetComponent<CutRope>().IsRopeUntied();
        if (isRopeUntied == true)
        {
            
            StartCoroutine(UnlockDoor());
        }

        if (rotating)
        {
            Vector3 to = new Vector3(0, 10, 0);
            if (Vector3.Distance(transform.eulerAngles, to) > 0.1f)
            {
                transform.parent.localEulerAngles = Vector3.Lerp(transform.parent.localEulerAngles, to, Time.deltaTime);
            }
            if(transform.parent.localEulerAngles.y>9f)
            {
                transform.eulerAngles = to;
                rotating = false;
            }
        }

        //Debug.Log(rotating);
        //Debug.Log("Rope Untied :"+isRopeUntied);
    }

    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(15f);

        if (doorUnlockedSoundPlayed == false)
        {
            doorUnlockedSoundPlayed = true;
            aSources[1].Play();
            rotating = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        moveDoor = false;
        handObj = col.gameObject;
        trackedObj = handObj.transform.parent.parent.GetComponent<SteamVR_TrackedObject>();
    }

    void OnTriggerExit(Collider col)
    {
        if (isRopeUntied && doorUnlockedSoundPlayed)
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                moveDoor = true;

//if (handObj.transform.position.z - transform.position.z > 0)
               // {
                    direction = 1;
                //}
                //else
                //{
                 //   direction = -1;
                //}
            }
        }
        else
        {
            aSources[0].Play();
        }
    }


    IEnumerator doorMover()
    {
        bool stoppedBefore = false;
        float yRot = 0;

        while (true)
        {
            if (moveDoor)
            {
                
                if (transform.parent.localEulerAngles.y < 145f)
                {

                    float angle = 0.0f;
                    angle = Vector3.Angle(transform.parent.position, handObj.transform.position);
                    yRot -= direction * angle * 5f * Time.deltaTime;
                    stoppedBefore = false;

                    //Debug.Log(Vector3.Distance(transform.position, handObj.transform.position));
                    if (Vector3.Distance(transform.position, handObj.transform.position) > 0.5f)
                    {
                        if (transform.parent.localEulerAngles.y < 145)
                            transform.parent.localEulerAngles = new Vector3(0, -yRot, 0);
                    }
                    else
                        if (Vector3.Distance(transform.position, handObj.transform.position) < 0.5f)
                    {
                        transform.parent.localEulerAngles = new Vector3(0, -yRot, 0);
                    }
                    else
                    {
                        moveDoor = false;
                    }
                }

                if (transform.parent.localEulerAngles.y > 20f)
                {
                    if (!_oneTimeCall)
                    {
                        StartCoroutine(SwitchScene(doorObj));
                        _oneTimeCall = true;
                    }
                }

            }
            else
            {
                if (!stoppedBefore)
                {
                    stoppedBefore = true;
                }
            }

            yield return null;
        }

    }

    IEnumerator SwitchScene(GameObject doorObj)
    {
        yield return new WaitForSeconds(0.2f);
        _oneHand.GetComponent<InteractableItems>().ResetMyChilds();
        _otherHand.GetComponent<InteractableItems>().ResetMyChilds();
        HospitalScene.SetActive(true);
        doorObj.transform.parent = HospitalScene.transform;
        EscapeScene.SetActive(false);
        // Destroy(transform.parent.parent.gameObject);
    }

}
