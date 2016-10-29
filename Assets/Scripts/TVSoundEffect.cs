using UnityEngine;
using System.Collections;

public class TVSoundEffect : MonoBehaviour {

    public AudioClip _TVBGM;
    public AudioClip _TVNews;

    AudioSource _TVBGMSource;
    public AudioSource _TVNewsSource;

    public float _fadeOutFactor = 1f;
    bool _isFadeInOut;
    float _silenceThreshold = 0.01f;
	// Use this for initialization
	void Start () {
        _isFadeInOut = false;
        _TVBGMSource = GetComponent<AudioSource>();
        PlayBGM();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_isFadeInOut) {
            if (_TVBGMSource.volume > 0) {
                _TVBGMSource.volume /= _fadeOutFactor;
                if (_TVBGMSource.volume < _silenceThreshold) {
                    _TVBGMSource.volume = 0.0f;
                }
            }
        }
	}

    public void PlayBGM() {
        _TVBGMSource.clip = _TVBGM;
        _TVBGMSource.Play();
    }

    public void PlayNews() {
        if (_TVBGMSource.isPlaying) {
            _isFadeInOut = true;
            _TVNewsSource.clip = _TVNews;
            _TVNewsSource.Play();
        }
    }
}
