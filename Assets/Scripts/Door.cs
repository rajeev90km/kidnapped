using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Door : MonoBehaviour
{

    public GameObject idCardObject;

    private bool moveDoor = false;
    private int direction = 1;

    public GameObject passCodePanel;

    float lastPosition = 0;
    private GameObject handObj;

    private bool isCardPrinted = false;


    private bool isCodeUnlocked = false;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    void Awake()
    {

    }

    void Start()
    {

        StartCoroutine(doorMover());

    }

    void FixedUpdate()
    {
        if (trackedObj)
            device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void Update()
    {
        if (transform.parent.localEulerAngles.y >= 150f)
        {
            if (!isCardPrinted)
            {
                idCardObject.GetComponent<PrintCard>()._printCard = true;
                isCardPrinted = true;
            }
        }

        isCodeUnlocked = passCodePanel.GetComponent<PassCode>().IsCodeUnlocked();
        Debug.Log(isCodeUnlocked);
    }

    void OnTriggerEnter(Collider col)
    {
        moveDoor = false;
        handObj = col.gameObject;
        trackedObj = handObj.transform.parent.parent.GetComponent<SteamVR_TrackedObject>();
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log(isCodeUnlocked);
        if (isCodeUnlocked)
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                moveDoor = true;

                if (handObj.transform.position.z - transform.position.z > 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().Play();
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
                if (transform.parent.localEulerAngles.y < 150f)
                {
                    float angle = 0.0f;
                    angle = Vector3.Angle(transform.parent.position, handObj.transform.position);
                    yRot -= direction * angle * 5f * Time.deltaTime;
                    stoppedBefore = false;

                    //Debug.Log(Vector3.Distance(transform.position, handObj.transform.position));
                    if (Vector3.Distance(transform.position, handObj.transform.position) > 0.5f)
                    {
                        if (transform.parent.localEulerAngles.y < 150f)
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

}
