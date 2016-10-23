using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {
    Rigidbody _mrb;
    public GameObject other;
	// Use this for initialization
	void Start () {
        _mrb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            other.GetComponent<Test>().CloneRb(_mrb);
        }
	}
}
