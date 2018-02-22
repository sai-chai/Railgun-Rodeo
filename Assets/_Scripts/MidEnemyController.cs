using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidEnemyController : MonoBehaviour {

	public Transform beamSpawn;
	public GameObject beamPrefab;
	public GameObject explosionPrefab;
	public float speed;
	private int frameTimer;
	private Rigidbody2D rb;
	private int health;
	private SpriteRenderer renderer;
	private bool enteredFrame;
	private LevelController level;

	// Use this for initialization
	void Start () {
		level = GameObject.Find("level").GetComponent<LevelController>();
		enteredFrame = false;
		health = 100;
		frameTimer = 0;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2 (speed, 0f), ForceMode2D.Impulse);
		renderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			var explosion = Instantiate (explosionPrefab, rb.transform.position, rb.transform.rotation) as GameObject;
			GameObject.Find ("explosion_sound").GetComponent<AudioSource> ().Play();
			Destroy (this.gameObject, 0f);
			Destroy (explosion, 3f);
			level.score = level.score + 30;
		}
		frameTimer++;
		if (frameTimer % 200 == 0 && Time.timeScale != 0) {
			Fire ();
		}
		if (frameTimer % 210 == 0 && Time.timeScale != 0) {
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
				Destroy (this.gameObject, 2f);
			}
		} else if (obj.CompareTag ("slug")) {
			health = health - 40;
		} else if (obj.CompareTag ("spaceship")) {
			health = health - 10;
			StartCoroutine (FlashSprite ());
		}
	}

	IEnumerator FlashSprite(){
		Color hurt = new Color32 (101, 0, 0, 255);
		renderer.color = hurt;
		yield return new WaitForSeconds (0.1f);
		renderer.color = Color.white;
		yield return new WaitForSeconds (0.03f);
		renderer.color = hurt;
		yield return new WaitForSeconds (0.1f);
		renderer.color = Color.white;
		yield return new WaitForSeconds (0.03f);
		renderer.color = hurt;
		yield return new WaitForSeconds (0.1f);
		renderer.color = Color.white;
	}
}
