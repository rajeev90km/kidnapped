﻿using UnityEngine;
using System.Collections;

public class CutRope : MonoBehaviour {

    public GameObject _chair;
    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.Sharp))
        {
            GetComponent<Animator>().SetBool("RopeCut", true);
            StartCoroutine(SetFree());
            transform.parent.GetComponent<ChairMove>().SetChairDestroyedFlag();
        }
    }

    IEnumerator SetFree()
    {
        yield return new WaitForSeconds(2f);
        _audio.Play();
        _chair.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}