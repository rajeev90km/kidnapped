using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PrintCard : MonoBehaviour {

    public bool _printCard;
    private bool _playAudio;
    private AudioSource _audio;
    private Vector3 _destination;

    private GameObject handObj;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    // Use this for initialization
    void Start () {
        _printCard = false;
        _playAudio = false;
        _audio = GetComponent<AudioSource>();
        _destination = new Vector3(0.0149f, 0.9746f, -2.2173f);
    }
	
    void OnTriggerEnter(Collider other)
    {
        //handObj = other.gameObject;
        //trackedObj = handObj.transform.parent.parent.GetComponent<SteamVR_TrackedObject>();
        //Debug.Log(trackedObj);
        //device = SteamVR_Controller.Input((int)trackedObj.index);

        //if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    GetComponent<Rigidbody>().useGravity = true;
        //}
    }

    void FixedUpdate()
    {
        if (trackedObj)
            device = SteamVR_Controller.Input((int)trackedObj.index);
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
            transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * 0.03f);
        }
        if(transform.position == _destination)
        {
            _printCard = false;
        }
	}
}
