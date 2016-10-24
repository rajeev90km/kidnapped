using UnityEngine;
using System.Collections;

public class CutRope : MonoBehaviour {

    public GameObject _chair;
    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "brokenBottleTop")
        {
            GetComponent<Animator>().SetBool("RopeCut", true);
            StartCoroutine(SetFree());
        }
    }

    IEnumerator SetFree()
    {
        yield return new WaitForSeconds(2f);
        _audio.Play();
        _chair.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
