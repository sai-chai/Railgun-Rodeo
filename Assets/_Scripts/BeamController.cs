using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {

	public GameObject hitPrefab;

	void Start(){
		GameObject.Find ("beam_sound").GetComponent<AudioSource> ().Play();
	}

	void Update () {
		Destroy (this.gameObject, 6f);
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.tag.Equals ("MainCamera", System.StringComparison.Ordinal)) {
			Destroy (this.gameObject, 0.2f);
		} else if (obj.CompareTag("spaceship")) {
			var hit = Instantiate (hitPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			Destroy (this.gameObject, 0.5f);
			Destroy (hit, 0.8f);
		}
	}
}
