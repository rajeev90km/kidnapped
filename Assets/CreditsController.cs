using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    public GameObject _sunLight;
    public bool _sunDown = false;
    public float MIN_SUN_HEIGHT;

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_sunDown) {
            _sunLight.transform.Translate(Vector3.down * Time.deltaTime * 0.05f);
            Color sunLightColor = _sunLight.GetComponent<Light>().color;
            _sunLight.GetComponent<Light>().color = new Color(sunLightColor.r, sunLightColor.g - Time.deltaTime * 0.03f, sunLightColor.b);
            
            if (_sunLight.transform.position.y < MIN_SUN_HEIGHT)
            {
                _sunDown = false;
            }
        }
	
	}

    
}
