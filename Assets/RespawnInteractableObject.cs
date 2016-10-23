using UnityEngine;
using System.Collections;

public class RespawnInteractableObject : MonoBehaviour {

    public Vector3 _defaultPosition = new Vector3(-1f, 1f, -0.5f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Interactable))
        {
            other.gameObject.transform.position = _defaultPosition;
        }
    }
}
