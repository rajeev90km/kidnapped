using UnityEngine;
using System.Collections;

public class RespawnInteractableObject : MonoBehaviour {

    public Vector3 _defaultPosition = new Vector3(-1f, 1f, -0.5f);
    public Transform EscapeScene;

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
            if (other.gameObject.transform.parent.name == "attach")
            {
                other.gameObject.transform.parent.gameObject.GetComponent<InteractableItems>().ResetMyChild(other.gameObject);
            }
            else
            {
                other.gameObject.transform.parent = EscapeScene;
            }
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
