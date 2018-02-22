using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {

	public float speed = 20f;
	public Transform slugSpawn;
	public GameObject slugPrefab;
	public GameObject explosionPrefab;
	private SpriteRenderer renderer;
	public int ammo;
	private Rigidbody2D rb;
	public int health;
	private LevelController level;
	private GameController game;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Global").GetComponent<GameController> ();
		level = GameObject.Find ("level").GetComponent<LevelController> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (SceneManager.GetActiveScene ().name.Equals ("Level 1")) {
			health = 100;
			ammo = 40;
		} else if (SceneManager.GetActiveScene ().name.Equals ("Level 2")) {
			health = game.health;
			ammo = game.ammo;
		}
		renderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0f) {
			if (health <= 0) {
				var explosion = Instantiate (explosionPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
				Destroy (this.gameObject, 0f);
				Destroy (explosion, 3f);
				level.gameOver = true;
			}

			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow)) {
				float moveH = Input.GetAxis ("Horizontal");
				float moveV = Input.GetAxis ("Vertical");
				Vector2 motion = new Vector2 (moveH, moveV);
				rb.AddForce (motion * speed);
			}

			//
			if (Input.GetKeyDown (KeyCode.Space)) {
				this.Fire ();
			}

			//reload
			if (Input.GetKeyDown (KeyCode.RightShift)) {
				if (ammo <= 0 && level.score >= 30) {
					StartCoroutine (Reload ());
				}
			}

			//repair
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				int difference = 100 - health;
				if (level.score > difference) {
					level.score -= difference;
					health = 100;
					GameObject.Find ("repair_sound").GetComponent<AudioSource> ().Play ();
				}
			}
		}		
	}

	IEnumerator Reload(){
		GameObject.Find ("reload_sound").GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (3.5f);
		level.score -= 30;
		ammo = 40;
	}
	
	void Fire(){
		if (ammo > 0) {
			var slug = Instantiate (slugPrefab, slugSpawn.position, slugSpawn.rotation) as GameObject;
			Vector2 slugVector = new Vector2 (10f, 0f);
			slug.GetComponent<Rigidbody2D> ().AddForce (slugVector * 40);
			ammo--;
		}
	}

	 void OnTriggerEnter2D(Collider2D obj){
		int frameCount = Time.frameCount;
		if (obj.CompareTag ("beam")) {
			health = health - 7;
		} else if (SceneManager.GetActiveScene().name.Equals("Level 2") && obj.CompareTag ("asteroid")) {
			health = health - 20;
			GameObject.Find ("crash_sound").GetComponent<AudioSource> ().Play ();
			StartCoroutine (FlashSprite ());
		} else if (obj.CompareTag ("enemy")) {
			health = health - 10;
			GameObject.Find ("crash_sound").GetComponent<AudioSource> ().Play ();
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

	public void Reset() {
		ammo = 40;
		health = 100;
	}
}