using UnityEngine;
using System.Collections;

public class PrintCard : MonoBehaviour {

    public bool _printCard;
    private bool _playAudio;
    private AudioSource _audio;
    private Vector3 _destination;

	// Use this for initialization
	void Start () {
        _printCard = false;
        _playAudio = false;
        _audio = GetComponent<AudioSource>();
        _destination = new Vector3(0.0149f, 0.9746f, -2.2173f);
    }
	
	// Update is called once per frame
	void Update () {
	    if (_printCard)
        {
            if (!_playAudio && !_audio.isPlaying)
            {
                _audio.Play();
                _playAudio = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * 0.1f);
        }
	}
}
