using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindObjsWithLayer : MonoBehaviour {
    [SerializeField]
    List<GameObject> target;
    int _targetLayer;

    List<GameObject> _unwantedObjs;
	// Use this for initialization
	void Start () {
        _unwantedObjs = new List<GameObject>();
        _targetLayer = LayerMask.NameToLayer(Layers.Sharp);
        RecursiveFindTransform(transform, _targetLayer, target);
	}

    void  RecursiveFindTransform(Transform parent, int _tarLayer, List<GameObject> target) {
        foreach(Transform child in parent) {
            RecursiveFindTransform(child, _tarLayer, target);
        }
        if (parent.gameObject.layer == _tarLayer) {
            if (!target.Contains(parent.gameObject)) {
                _unwantedObjs.Add(parent.gameObject);
                Debug.Log("Unwanted: " + parent.name);
            }
        }
    }
}
