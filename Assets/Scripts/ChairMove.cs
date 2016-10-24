using UnityEngine;
using System.Collections;

public class ChairMove : MonoBehaviour {

    GameObject cameraEye;

    float currentTime;
	// Use this for initialization
	void Start () {
        cameraEye = GameObject.Find("Camera (eye)");

        InvokeRepeating("UpdateChairPosition", 1.0f, 1.0f);
    }
	
    void UpdateChairPosition()
    {
        if (cameraEye) { 
            if (cameraEye.transform.position.y > 1.0f)
               transform.position = new Vector3(cameraEye.transform.position.x, transform.position.y, cameraEye.transform.position.z);
        }

    }
    // Update is called once per frame
    void Update () {
        
	}
}
