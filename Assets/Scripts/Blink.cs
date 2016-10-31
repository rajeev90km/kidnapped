using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Blink : MonoBehaviour {

    public int direction;
    private int ctr = 0;

    float startTime;

    private bool stopBlinking = false;
    private Blur cameraBlur;
    private GameObject cameraObj;

    void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
        
    }

    // Use this for initialization
    void Start () {
       cameraObj = GameObject.Find("Camera (eye)");
       transform.parent = cameraObj.transform;
       transform.localPosition = new Vector3(-0.11f, (direction * 0.762f), 0.06f);
        transform.localEulerAngles = new Vector3(0,0,0);
        //cameraBlur = cameraObj.GetComponent<Blur>();
        //cameraBlur.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(BlinkEye());
    }

    IEnumerator BlinkEye()
    {
        if (!stopBlinking)
        {
            yield return new WaitForSeconds(7f);
            ctr++;
            transform.localPosition = new Vector3(-0.11f, transform.localPosition.y + (direction * Mathf.Sin(Time.time - startTime) /10000 * ctr), 0.06f);
            yield return new WaitForSeconds(5f);
            //cameraBlur.enabled = false;
            stopBlinking = true;
            Destroy(gameObject,2f);
        }
    }
}
