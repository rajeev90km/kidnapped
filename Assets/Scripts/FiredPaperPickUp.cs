using UnityEngine;
using System.Collections;

public class FiredPaperPickUp : MonoBehaviour {

    bool isTrackPlayed = false;
    public GameObject argueObject;
    AudioSource argueAudioSource;

	// Use this for initialization
	void Start () {
        argueAudioSource = argueObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (transform.parent && !isTrackPlayed)
        {
            argueAudioSource.Play();
            isTrackPlayed = true;
        }
	}

    
}
