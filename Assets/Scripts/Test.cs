using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
    List<int> intlist;
    Rigidbody _myrb;
    // Use this for initialization


    void Awake() {
        Debug.Log("test awake");
    }

    void OnEnable() {
        Debug.Log("test OnEnable");
    }

    void Start () {
        Debug.Log("test");
        _myrb = GetComponent<Rigidbody>();
        intlist = new List<int>();
        intlist.Add(1);
        intlist.Add(2);
        intlist.Add(3);
        intlist.Add(4);
        intlist.RemoveAt(intlist.IndexOf(3));
        Debug.Log("index 1: " + intlist[2]);
        Debug.Log("index length: " + intlist.Count);
        //_myrb.velocity = new Vector3(-2, 0, 0);

    }

    public void CloneRb(Rigidbody _newRb)
    {
        _myrb = Instantiate(_newRb) as Rigidbody;
    }

    // Update is called once per frame
    void Update () {
	}

    void OnTriggerEnter() {
        Debug.Log("Trigger: " + name);
    }

    void OnCollisionEnter(Collision colli) {
        Debug.Log("Contacts Length: " + colli.contacts.GetLength(0));
    }

    void OnCollisionStay(Collision colli) {
    }
}
