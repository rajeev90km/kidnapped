using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
    List<int> intlist;
    Rigidbody _myrb;
	// Use this for initialization
	void Start () {
        _myrb = GetComponent<Rigidbody>();
        intlist = new List<int>();
        intlist.Add(1);
        intlist.Add(2);
        intlist.Add(3);
        intlist.Add(4);
        intlist.RemoveAt(intlist.IndexOf(3));
        Debug.Log("index 1: " + intlist[2]);
        Debug.Log("index length: " + intlist.Count);

    }

    public void CloneRb(Rigidbody _newRb)
    {
        _myrb = Instantiate(_newRb) as Rigidbody;
    }

    // Update is called once per frame
    void Update () {
	}

    void OnCollisionEnter(Collision colli) {
        Debug.Log("Colli detected by: " + gameObject.name);
    }
}
