using UnityEngine;
using System.Collections;

public class ChairMove : MonoBehaviour {

    [SerializeField]
    Transform BoundHand;
    Vector3 _lastBoundHandPos;
    Vector3 _handChairOffset;
    float _moveThreshold = 0.01f;

    bool _isChairDestroyed;
    // Use this for initialization
	void Start () {
        _isChairDestroyed = false;
        _handChairOffset = new Vector3(-0.28f, -0.656f, -0.004f);
        _lastBoundHandPos = BoundHand.position;
    }


    void Update() {
        if (!_isChairDestroyed) {
            if (Vector3.Distance(BoundHand.position, _lastBoundHandPos) > _moveThreshold) {
                transform.position = BoundHand.position + _handChairOffset;
                //maybe a lerp here?
                _lastBoundHandPos = BoundHand.position;
            }
        }
	}

    public void SetChairDestroyedFlag() {
        _isChairDestroyed = true;
    }
}
