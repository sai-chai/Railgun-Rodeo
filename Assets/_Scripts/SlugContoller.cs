using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugContoller : MonoBehaviour {

	public GameObject hitPrefab;

	void Start(){
		GameObject.Find ("slug_sound").GetComponent<AudioSource> ().Play();
	}

	// Update is called once per frame
	void Update () {
		Destroy (this.gameObject, 6f);
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.tag.Equals ("MainCamera", System.StringComparison.Ordinal)) {
			Destroy (this.gameObject, 0.2f);
		} else if (obj.CompareTag("enemy")){
			var hit = Instantiate (hitPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			Destroy (this.gameObject, 0.05f);
			Destroy (hit, 1.5f);
		}
	}
}
