using UnityEngine;
using System.Collections;

public class NewsPaperPickUpSound : MonoBehaviour {
    public TVSoundEffect _tvSound;
    bool _oneTime = false;
    void Start()
    {
    }
    void Update()
    {
        if (transform.parent.name == "attach")
        {
            if (!_oneTime)
            {
                _tvSound.PlayNews();
                _oneTime = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.Palm)) {

        }
    }

}
