using UnityEngine;
using System.Collections;

public class NewsPaperPickUpSound : MonoBehaviour {
    public TVSoundEffect _tvSound;
    public GameObject _kid;
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
                _kid.GetComponent<Animator>().SetBool("isResting", true);
                _oneTime = true;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(Layers.Palm)) {

        }
    }

}
