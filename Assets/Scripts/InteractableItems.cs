﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class InteractableItems : MonoBehaviour {
    // TrackedController ancestor (contains button events)
    private SteamVR_TrackedController _trackedController;

    // Object (if any) in grab range and not currently grabbed
    private List<GameObject> _hovereds;
    private List<GameObject> _hoveredParents;

    // Object currently grabbed
    private GameObject _grabbed;
    public GameObject _otherHand;

    void Start() {
        _hovereds = new List<GameObject>();
        _hoveredParents = new List<GameObject>();
        // TrackedController is on great-grandparent of attach point
        _trackedController = transform.parent.parent.parent.GetComponent<SteamVR_TrackedController>();

        // Register trigger click and unclick callbacks
        _trackedController.TriggerClicked += new ClickedEventHandler(OnTriggerClick);
        _trackedController.TriggerUnclicked += new ClickedEventHandler(OnTriggerUnclick);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Tags.Interactable)) {
            _hovereds.Add(other.gameObject);
            _hoveredParents.Add(other.gameObject.transform.parent.gameObject);
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
        if (_hovereds.Count > 0) {
            GameObject _closestOne = GetClosestObj();
            //if (_closestOne.transform.parent == _hoveredParents[_hovereds.IndexOf(_closestOne)]) {
            _closestOne.transform.parent = this.transform;
            _grabbed = _closestOne;
            Rigidbody body = _grabbed.GetComponent<Rigidbody>();
            Debug.Assert(body != null, "Grabbable lacks a RigidBody");
            body.isKinematic = true;
            _otherHand.GetComponent<InteractableItems>().ResetGrabbedObj(_closestOne);
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
        if (_grabbed) {
            Rigidbody body = _grabbed.GetComponent<Rigidbody>();
            Debug.Assert(body != null, "Grabbable lacks a RigidBody");
            body.isKinematic = false;
            body.transform.parent = _hoveredParents[_hovereds.IndexOf(_grabbed)].transform;
            body.velocity = SteamVR_Controller.Input((int)_trackedController.controllerIndex).velocity;
            body.angularVelocity = SteamVR_Controller.Input((int)_trackedController.controllerIndex).angularVelocity;
            _grabbed = null;
        }
    }

    public void ResetGrabbedObj(GameObject grabbedObj) {
        if (grabbedObj == _grabbed) {
            _grabbed = null;
        }
    }
}