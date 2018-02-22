using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyController : MonoBehaviour {

	public Transform beamSpawn;
	public GameObject beamPrefab;
	public GameObject explosionPrefab;
	public float speed;
	private int frameTimer;
	private Rigidbody2D rb;
	private bool enteredFrame;
	private LevelController level;

	// Use this for initialization
	void Start () {
		level = GameObject.Find("level").GetComponent<LevelController>();
		enteredFrame = false;
		frameTimer = 0;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2 (speed, 0f), ForceMode2D.Impulse);
	}

	// Update is called once per frame
	void Update () {
		frameTimer++;
		if (frameTimer % 180 == 0 && Time.timeScale != 0) {
			Fire ();
		}
	}
	
	void Fire() {
		var beam = Instantiate (beamPrefab, beamSpawn.position, beamSpawn.rotation) as GameObject;
		Vector2 beamVector = new Vector2 (-10f, 0f);
		beam.GetComponent<Rigidbody2D> ().AddForce (beamVector * 60);
	}
	
	void OnTriggerEnter2D(Collider2D obj){
		if (obj.CompareTag ("MainCamera")) {
			if (!enteredFrame) {
				enteredFrame = true;
			} else {
				Destroy (this.gameObject, 1f);
			}
		} else if (obj.CompareTag ("slug")) {
			var explosion = Instantiate (explosionPrefab, rb.transform.position, rb.transform.rotation);
			GameObject.Find ("explosion_sound").GetComponent<AudioSource> ().Play();
			Destroy (this.gameObject, 0f);
			Destroy (explosion, 2f);
			level.score = level.score + 5;
		} else if (obj.CompareTag ("spaceship")) {
			var explosion = Instantiate (explosionPrefab, rb.transform.position, rb.transform.rotation);
			GameObject.Find ("explosion_sound").GetComponent<AudioSource> ().Play();
			Destroy (this.gameObject, 0f);
			Destroy (explosion, 2f);
			level.score = level.score + 1;
		}
	}
}
