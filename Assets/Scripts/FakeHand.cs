using UnityEngine;
using System.Collections;

public class FakeHand : MonoBehaviour {

    [SerializeField]
    Transform _realHand;
    Vector3 _realHandPrevPos;
    bool _isChairDestroyed;

    public float TRACKING_THRESHOLD = 0.004f;
    

    // Use this for initialization
    void Start () {
        _isChairDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (HandMoved())
        {
            transform.position = _realHand.position;
            transform.rotation = Quaternion.Euler(0, _realHand.rotation.eulerAngles.y, 0);
            _realHandPrevPos = _realHand.position;
        }
        
        
    }

    private bool HandMoved()
    {
        if (Vector3.Distance(_realHand.position, _realHandPrevPos) > TRACKING_THRESHOLD)
        {
            return true;
        } else {
            return false;
        };
        
    }

    public void SetChairDestroyedFlag() {
        _isChairDestroyed = true;
    }
}
