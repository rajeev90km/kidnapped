using UnityEngine;
using System.Collections;

public class StampDetect : MonoBehaviour {

    int _myIndex;
    public GameObject _sealPrefab;
    public GameObject _papers;
    public GameObject StampScene;
    public GameObject EscapeScene;

    float _childLayerOffset = 0.0001f;

    public void SetMyIndex(int index) {
        _myIndex = index;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Gameobject: " + other.name);
        if (other.tag == Tags.Paper || other.tag == Tags.SpecialPaper) {
            if (IsEnteringFace(other.gameObject, gameObject)) {
                transform.parent.gameObject.GetComponent<Stamp>().SetPointDetectFlag(_myIndex, true);
                if (transform.parent.GetComponent<Stamp>()._isAllPointsDetected()) {
                    Quaternion _sealRot = Quaternion.Euler(0, transform.parent.parent.rotation.eulerAngles.y, 0);
                    Vector3 _sealHeadPos = transform.parent.position;
                    Vector3 _sealPos = _sealHeadPos - Vector3.Dot(_sealHeadPos - other.transform.position, other.transform.up) * other.transform.up;
                    int _childIndex = other.transform.childCount;
                    GameObject _stamp = Instantiate(_sealPrefab, _sealPos, _sealRot, other.transform) as GameObject;
                    _stamp.transform.localScale = _sealPrefab.transform.localScale.x / _stamp.transform.lossyScale.x * _stamp.transform.localScale;
                    _stamp.transform.localPosition = new Vector3(_stamp.transform.localPosition.x, _childIndex * _childLayerOffset, _stamp.transform.localPosition.z);
                    if (!other.gameObject.GetComponent<PaperParameters>().IsStampOnMe()) {
                        _papers.GetComponent<PapersMovement>().UpdateDestination();
                    }
                    other.gameObject.GetComponent<PaperParameters>().StampON();
                    if (other.tag == Tags.SpecialPaper) {
                        StartCoroutine(SwitchScene());
                    }
                } else {
                    Debug.Log("All three points have to be touched");
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == Tags.Paper && other.tag == Tags.SpecialPaper) {
            transform.parent.gameObject.GetComponent<Stamp>().SetPointDetectFlag(_myIndex, false);
        }
    }

    bool IsEnteringFace(GameObject _target, GameObject _source) {
        if (Vector3.Dot(_source.transform.position - _target.transform.position, _target.transform.up) >= 0) {
            return true;
        } else {
            return false;
        }
    }

    IEnumerator SwitchScene() {
        yield return new WaitForSeconds(0.2f);
        EscapeScene.SetActive(true);
        StampScene.SetActive(false);
        Destroy(transform.parent.parent.gameObject);
    }
}
