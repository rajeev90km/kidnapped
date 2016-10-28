using UnityEngine;
using System.Collections;

public class BallBounceSound : MonoBehaviour {
    public AudioClip _bounceSound;

    AudioSource _myAudSource;
    // Use this for initialization
    void Start() {
        _myAudSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision colli) {
        if (colli.gameObject.layer == LayerMask.NameToLayer(Layers.StaticOBJ)) {
            _myAudSource.clip = _bounceSound;
            _myAudSource.Play();
        }
    }
}
