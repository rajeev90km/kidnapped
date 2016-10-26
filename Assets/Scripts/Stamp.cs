using UnityEngine;
using System.Collections;

public class Stamp : MonoBehaviour {
    bool _point1Dected;
    bool _point2Dected;
    bool _point3Dected;



    void Start () {

        _point1Dected = false;
        _point2Dected = false;
        _point3Dected = false;
        int i = 0;
        foreach(Transform child in transform) {
            child.gameObject.GetComponent<StampDetect>().SetMyIndex(i);
            i++;
        }
    }


    public bool _isAllPointsDetected() {
        if (_point1Dected && _point2Dected && _point3Dected) {
            return true;
        } else {
            return false;
        }
    }

    public void SetPointDetectFlag(int indexOfChild, bool tof) {
        switch (indexOfChild) {
            case 0:
                _point1Dected = tof;
                break;
            case 1:
                _point2Dected = tof;
                break;
            case 2:
                _point3Dected = tof;
                break;
            default:
                Debug.Log("Error index of stamp child");
                break;
        }
    }
}
