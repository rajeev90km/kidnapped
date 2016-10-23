using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BreakBottle : MonoBehaviour {

    float _explosionRadius = 0.2f;
    float _explosionForce = 2.0f;
    float _velocityThreshold = 1f;
    Rigidbody _myRb;
    [SerializeField]
    BottleSounds _bottleSounds;

    Vector3 _myVelo;
    Vector3 _lastPos;

    void Start () {
        _myVelo = Vector3.zero;
        _lastPos = transform.position;
        _myRb = GetComponent<Rigidbody>();
        Debug.Assert(_myRb != null, "Breakable Bottle lacks a RigidBody");
    }

    void Update() {
        UpdateVelocity();
    }

    void UpdateVelocity() {
        _myVelo = (transform.position - _lastPos) / Time.deltaTime;
        _lastPos = transform.position;
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == Tags.Tough) {
            if (_myVelo.magnitude > _velocityThreshold) {
                Debug.Log("Broken: " + _myVelo.magnitude);
                _bottleSounds.PlayBreakingSound();
                foreach (Transform childPiece in transform) {
                    //if (childPiece.gameObject.activeSelf) {
                    //    childPiece.gameObject.SetActive(false);
                    //} else {
                    childPiece.gameObject.SetActive(true);
                    if (childPiece.tag != Tags.UnBreakable) {
                        childPiece.parent = null;
                        if (!childPiece.GetComponent<Rigidbody>()) {
                            childPiece.gameObject.AddComponent<Rigidbody>();
                        }
                            
                        EmitScatterPiece(childPiece, other);
                    } else {
                        if (!childPiece.GetComponent<Rigidbody>()) {
                            childPiece.gameObject.AddComponent<Rigidbody>();
                        }
                        SubstituteMySonToMyself(childPiece);
                    }
                    //}
                }
                Destroy(gameObject);
            } else {
                Debug.Log("Smash Harder!");
            }
        }
    }

    void SubstituteMySonToMyself(Transform myson)
    {
        myson.parent = transform.parent;
        myson.position = transform.position;
        myson.rotation = transform.rotation;
        Rigidbody _mysonRd = myson.GetComponent<Rigidbody>();
        _mysonRd.velocity = _myRb.velocity;
        _mysonRd.angularVelocity = _myRb.angularVelocity;
        _mysonRd.useGravity = _myRb.useGravity;
        _mysonRd.isKinematic = _myRb.isKinematic;
        _mysonRd.constraints = _myRb.constraints;
    }

    void EmitScatterPiece(Transform piece, Collision other) {
        Rigidbody _pieceRb = piece.GetComponent<Rigidbody>();
        Debug.Assert(_pieceRb != null, "Pieces lack a RigidBody");
        if (_pieceRb) {
            _pieceRb.AddExplosionForce(_explosionForce * _myVelo.magnitude / 2.0f, other.contacts[0].point, _explosionRadius, 0.0f, ForceMode.Impulse);
        }
    }
}
