using UnityEngine;
using System.Collections;

public class CutRope : MonoBehaviour {

    public GameObject _chair;
    private AudioSource _audio;

    public bool isRopeUntied = false;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.Sharp) && other.transform.parent.name == "attach")
        {
            
            if (!isRopeUntied)
            {
                GetComponent<Animator>().SetBool("RopeCut", true);
                StartCoroutine(SetFree());
                transform.parent.parent.GetComponent<FakeHand>().SetChairDestroyedFlag();
            }


        }
    }

    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(17f);
    }

    public bool IsRopeUntied()
    {
        return isRopeUntied;
    }

    IEnumerator SetFree()
    {
        if (!_audio.isPlaying)
        {
            _audio.Play();
        }
        
        yield return new WaitForSeconds(2f);
        isRopeUntied = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2f);
        _chair.SetActive(false);
    }
}
