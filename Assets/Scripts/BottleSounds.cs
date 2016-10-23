using UnityEngine;
using System.Collections;

public class BottleSounds : MonoBehaviour {

    AudioSource _myAudio;

    void Start() {
        _myAudio = GetComponent<AudioSource>();
        Debug.Assert(_myAudio != null, "Bottle doesn't have a audiosource attached to it");
    }

    public void PlayBreakingSound() {
        if (!_myAudio.isPlaying) {
            _myAudio.Play();
        }
    }
}
