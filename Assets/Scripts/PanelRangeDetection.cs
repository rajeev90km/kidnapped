using UnityEngine;
using System.Collections;

public class PanelRangeDetection : MonoBehaviour {
    [SerializeField]
    GameObject PanelCenter;

    Animator _handAnim;
    int _nearPanelHash = Animator.StringToHash("NearPanel");
    float _range = 1;

    void Start() {
        _handAnim = GetComponent<Animator>();
    }

    void Update() {
        if (Vector3.Distance(PanelCenter.transform.position, transform.position) < _range) {
            _handAnim.SetBool(_nearPanelHash, true);
            Debug.Log("Yes");
        } else {
            _handAnim.SetBool(_nearPanelHash, false);
            Debug.Log("No");
        }
    }
}
