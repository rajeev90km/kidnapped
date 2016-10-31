using UnityEngine;
using System.Collections;

public class TVSoundEffect : MonoBehaviour {

    public AudioClip _TVBGM;
    public AudioClip _TVNews;
    public GameObject _Credits;

    AudioSource _TVBGMSource;
    public AudioSource _TVNewsSource;

    public float _fadeOutTime;
    bool _isFadeInOut;
    float _silenceThreshold = 0.001f;
    float _defaultVolume;
	// Use this for initialization
	void Start () {
        _fadeOutTime = 5.0f;
        _isFadeInOut = false;
        _TVBGMSource = GetComponent<AudioSource>();
        _defaultVolume = _TVBGMSource.volume;
        PlayBGM();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_isFadeInOut) {
            if (_TVBGMSource.volume > 0) {
                _TVBGMSource.volume -= _defaultVolume * Time.deltaTime / _fadeOutTime;
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
        StartCoroutine(StartCredits());
    }

    IEnumerator StartCredits()
    {
        yield return new WaitForSeconds(15f);
        _Credits.SetActive(true);
    }
}
