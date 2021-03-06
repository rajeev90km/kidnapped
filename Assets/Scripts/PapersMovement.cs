﻿using UnityEngine;
using System.Collections;

public class PapersMovement : MonoBehaviour {
    public float _movementOffset = 0.6f;
    public float _duration = 3.0f;
    AudioSource _audio;

    Vector3 _des;
	// Use this for initialization
	void Start () {
        _des = transform.position;
        _audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, _des, _movementOffset * Time.deltaTime / _duration);
    }

    public void UpdateDestination() {
        _des.z += _movementOffset;
        _audio.Play();
    }
}
