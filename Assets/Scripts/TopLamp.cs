using UnityEngine;
using System.Collections;

public class TopLamp : MonoBehaviour {

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.realtimeSinceStartup) * 10 + 90, 0, 0);
    }
}
