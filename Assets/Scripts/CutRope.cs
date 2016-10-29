using UnityEngine;
using System.Collections;

public class CutRope : MonoBehaviour {

    public GameObject _chair;
    private AudioSource _audio;

    private bool isRopeUntied = false;

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
            GetComponent<Animator>().SetBool("RopeCut", true);
            StartCoroutine(SetFree());
            transform.parent.parent.GetComponent<FakeHand>().SetChairDestroyedFlag();

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
        yield return new WaitForSeconds(2f);
        isRopeUntied = true;
        _chair.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
