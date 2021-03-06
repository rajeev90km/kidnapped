﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class InteractableItems : MonoBehaviour {
    // TrackedController ancestor (contains button events)
    private SteamVR_TrackedController _trackedController;

    public GameObject EscapeScene;
    // Object (if any) in grab range and not currently grabbed
    public List<GameObject> _hovereds;
    public List<GameObject> _hoveredParents;

    // Object currently grabbed
    private GameObject _grabbed;
    public GameObject _otherHand;

    Animator _handAnim;
    int _triggerPressHash = Animator.StringToHash("TriggerPress");

    void Start() {
        _handAnim = transform.parent.GetComponent<Animator>();
        _hovereds = new List<GameObject>();
        _hoveredParents = new List<GameObject>();
        // TrackedController is on great-grandparent of attach point
        _trackedController = transform.parent.parent.GetComponent<SteamVR_TrackedController>();

        // Register trigger click and unclick callbacks
        _trackedController.TriggerClicked += new ClickedEventHandler(OnTriggerClick);
        _trackedController.TriggerUnclicked += new ClickedEventHandler(OnTriggerUnclick);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.Interactable || other.tag == Tags.BreakHeir) {
            if (!_hovereds.Contains(other.gameObject))
            {
                _hovereds.Add(other.gameObject);
                if (other.gameObject.transform.parent)
                {
                    if (other.gameObject.transform.parent.name == "attach")
                    {
                        _hoveredParents.Add(_otherHand.GetComponent<InteractableItems>().GetRealParent(other.gameObject));
                    } else
                    {
                        _hoveredParents.Add(other.gameObject.transform.parent.gameObject);
                    }
                }
                else
                {
                    _hoveredParents.Add(null);
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (_hovereds.Contains(other.gameObject)) {
            int index = _hovereds.IndexOf(other.gameObject);
            _hoveredParents.RemoveAt(index);
            if (!_hovereds.Remove(other.gameObject)) {
                Debug.LogWarning("Gameobj is not existed for removing");
            }
        }
    }

    public void OnTriggerClick(object sender, ClickedEventArgs e) {
        _handAnim.SetBool(_triggerPressHash, true);
        if (_hovereds.Count > 0) {
            GameObject _closestOne = GetClosestObj();
            //if (_closestOne.transform.parent == _hoveredParents[_hovereds.IndexOf(_closestOne)]) {
            _closestOne.transform.parent = this.transform;
            _grabbed = _closestOne;
            Rigidbody body = _grabbed.GetComponent<Rigidbody>();
            body.useGravity = true;
            body.isKinematic = false;
            Debug.Assert(body != null, "Grabbable lacks a RigidBody");
            body.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            _otherHand.GetComponent<InteractableItems>().ResetGrabbedObj(_grabbed);
            //}
        }
    }

    private GameObject GetClosestObj() {
        float _miniDistance = float.MaxValue;
        GameObject _miniObj = null;
        foreach(GameObject obj in _hovereds) {
            float _curDistance = Vector3.Distance(obj.transform.position, transform.position);
            if (_curDistance < _miniDistance) {
                _miniDistance = _curDistance;
                _miniObj = obj;
            }
        }
        return _miniObj;
    }
    public void OnTriggerUnclick(object sender, ClickedEventArgs e) {
        _handAnim.SetBool(_triggerPressHash, false);
        if (_grabbed) {
            Rigidbody body = _grabbed.GetComponent<Rigidbody>();
            Debug.Assert(body != null, "Grabbable lacks a RigidBody");
            body.constraints = RigidbodyConstraints.None;
            if (_hoveredParents[_hovereds.IndexOf(_grabbed)])
            {
                _grabbed.transform.parent = _hoveredParents[_hovereds.IndexOf(_grabbed)].transform;
            } else
            {
                Debug.Log("Yohooo");
                _grabbed.transform.parent = EscapeScene.transform;
                //_grabbed.transform.parent = null;
            }
            body.velocity = SteamVR_Controller.Input((int)_trackedController.controllerIndex).velocity;
            body.angularVelocity = SteamVR_Controller.Input((int)_trackedController.controllerIndex).angularVelocity;
            _grabbed = null;
        }
    }

    public GameObject GetRealParent(GameObject _target)
    {
        if (_hovereds.Contains(_target))
        {
            return _hoveredParents[_hovereds.IndexOf(_target)];
        } else
        {
            Debug.Log("this means obj is a child of hand but not registerted in the list");
            return null;
        }
    }

    public void SetGrabbedParent(GameObject _originParent)
    {
        Debug.Log("SetGrabbedParent is called");
        _hoveredParents[_hovereds.IndexOf(_grabbed)] = _originParent;
    }

    public void UpdateSubstitutionObj(GameObject origin, GameObject sub) {
        if (_hovereds.Contains(origin)) {
            _hovereds[_hovereds.IndexOf(origin)] = sub;
            if (_grabbed == origin) {
                _grabbed = sub;
            }
        } else {
            Debug.LogWarning("No target obj for substitution");
        }
    }

    public void ResetGrabbedObj(GameObject grabbedObj) {
        if (grabbedObj == _grabbed)
        {
            _otherHand.GetComponent<InteractableItems>().SetGrabbedParent(_hoveredParents[_hovereds.IndexOf(_grabbed)]);
            _grabbed = null;
            Debug.Log(grabbedObj.name);
        }
    }

    public void ResetMyChilds()
    {

        Debug.Log("Reset my childs is called");
        _grabbed = null;
        _hovereds.Clear();
        _hoveredParents.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ResetMyChild(GameObject child)
    {
        if (_grabbed == child)
        {
            _grabbed = null;
            if (_hovereds.Contains(child))
            {
                Debug.Log("Reset my child is called");
                child.transform.parent = _hoveredParents[_hovereds.IndexOf(child)].transform;
                _hoveredParents.RemoveAt(_hovereds.IndexOf(child));
                _hovereds.Remove(child);
                child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
        
    }
}
