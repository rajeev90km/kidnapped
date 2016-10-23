using UnityEngine;
using System.Collections;

public class PanelRangeDetection : MonoBehaviour {
    [SerializeField]
    GameObject PanelCenter;

    Animator _handAnim;
    int _nearPanelHash = Animator.StringToHash("NearPanel");
    float _range = 0.5f;

    void Start() {
        _handAnim = GetComponent<Animator>();
    }

    void Update() {
        if (Vector3.Distance(PanelCenter.transform.position, transform.position) < _range) {
            _handAnim.SetBool(_nearPanelHash, true);
        } else {
            _handAnim.SetBool(_nearPanelHash, false);
        }
    }
}
