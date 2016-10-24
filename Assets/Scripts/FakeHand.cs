using UnityEngine;
using System.Collections;

public class FakeHand : MonoBehaviour {
    [SerializeField]
    Transform _realHand;
    bool _isChairDestroyed;
    // Use this for initialization
    void Start () {
        _isChairDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = _realHand.position;
        transform.rotation = Quaternion.Euler(0, _realHand.rotation.eulerAngles.y, 0);
	}

    public void SetChairDestroyedFlag() {
        _isChairDestroyed = true;
    }
}
