using UnityEngine;
using System.Collections;

public class RadioLight : MonoBehaviour {

    private Light _light;
    public float _maxIntensity;

	// Use this for initialization
	void Start () {
        _light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        _light.intensity = Mathf.PingPong(Time.time*_maxIntensity, _maxIntensity);
    }
}
