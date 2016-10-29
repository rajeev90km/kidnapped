using UnityEngine;
using System.Collections;

public class NewsPaperPickUpSound : MonoBehaviour {
    public TVSoundEffect _tvSound;

    void OnCollisionEnter(Collision colli) {
        if (colli.gameObject.layer == LayerMask.NameToLayer(Layers.Palm)) {
            _tvSound.PlayNews();
        }
    }

}
