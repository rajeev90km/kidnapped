using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {
    Rigidbody _mrb;
    public GameObject other;
    // Use this for initialization
    Vector3 _lastVelo;
    Vector3 _lastAngularVelo;
    bool test;
    bool firstTime;
    void Awake() {
        Debug.Log("test2 awake");
    }

    void OnEnable() {
        Debug.Log("test OnEnable");
    }

    Rigidbody _myRb;
    void Start () {
        _myRb = GetComponent<Rigidbody>();
        firstTime = true;
        test = false;
        _lastVelo = Vector3.zero;
        Debug.Log("Test2");
        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
	}

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            test = true;
        }
        if (test) {
            if (firstTime){
                Debug.Log(name + " lastVelo: " + _lastVelo);
                Debug.Log(name + " lastAngularVelo: " + _lastAngularVelo);
                firstTime = false;
            }
            Debug.Log(name + " Velo: " + GetComponent<Rigidbody>().velocity.ToString("F4"));
            Debug.Log(name + " AngularVelo: " + _myRb.angularVelocity.ToString("F4"));
        }
        _lastVelo = _myRb.velocity;
        _lastAngularVelo = _myRb.angularVelocity;
    }


    void OnCollisionEnter(Collision colli) {

        Debug.Log("InDetector Velo: " + _myRb.velocity);
        foreach(ContactPoint _point in colli.contacts) {
            //Debug.Log(name + " : " + _point.point);

            //GetComponent<Rigidbody>().AddForceAtPosition(colli.impulse / Time.fixedDeltaTime, _point.point,ForceMode.Impulse);
        }
        //_myRb.AddForce(Vector3.one * 100, ForceMode.Impulse);
        test = true;
    }

    // Update is called once per frame
    void Update () {
    }
}
