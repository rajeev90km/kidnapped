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
        transform.position = new Vector3(cameraObj.transform.position.x-0.07f, cameraObj.transform.position.y + (direction * 0.296f), cameraObj.transform.position.z);
        cameraBlur = cameraObj.GetComponent<Blur>();
        cameraBlur.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(BlinkEye());
    }

    IEnumerator BlinkEye()
    {
        if (!stopBlinking)
        {
            yield return new WaitForSeconds(2f);
            ctr++;
            transform.position = new Vector3(transform.position.x, transform.position.y + (direction * Mathf.Sin(Time.time - startTime) / 130000 * ctr), transform.position.z);
            yield return new WaitForSeconds(12f);
            cameraBlur.enabled = false;
            stopBlinking = true;
            Destroy(gameObject,2f);
        }
    }
}
