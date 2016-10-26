using UnityEngine;
using System.Collections;

public class PaperParameters : MonoBehaviour {


    bool _isStampOnMe;

    void Start() {
        _isStampOnMe = false;
    }

    public void StampON() {
        _isStampOnMe = true;
    }

    public bool IsStampOnMe() {
        return _isStampOnMe;
    }
}
