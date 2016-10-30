using UnityEngine;
using System.Collections;

public class PaperParameters : MonoBehaviour {


    bool _isStampOnMe;
    AudioSource _audio;

    void Start() {
        _isStampOnMe = false;
        _audio = GetComponent<AudioSource>();
    }

    public void StampON() {
        _isStampOnMe = true;
        if (!_audio.isPlaying)
            _audio.Play();
    }

    public bool IsStampOnMe() {
        return _isStampOnMe;
    }
}
