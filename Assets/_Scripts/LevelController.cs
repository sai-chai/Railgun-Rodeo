using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	private GameController game;
	private Transform[] spawnArray;
	private System.Random randomGen;
	private GameObject bigEnemy;
	private bool isDone;
	public bool gameOver;
	public GameObject smallPrefab;
	public GameObject midPrefab;
	public GameObject bigPrefab;
	public GameObject asteroidPrefab;
	public int score;
	public bool paused;
	private GameObject pausePanel;
	public bool inGame;
	public bool levelOver;
	private GameObject startPanel;
	private GameObject endPanel;
	private GameObject miscUI;
	private float startTime;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Global").GetComponent<GameController> ();
		inGame = false;
		isDone = false;
		bigEnemy = null;
		gameOver = false;
		score = game.score;
		spawnArray = gameObject.GetComponentsInChildren<Transform> ();
		randomGen = new System.Random();
		paused = false;
		GameObject canvas = GameObject.Find ("Canvas");
		pausePanel = canvas.transform.GetChild(0).gameObject;
		startPanel = canvas.transform.GetChild(2).gameObject;
		endPanel = canvas.transform.GetChild(3).gameObject;
		miscUI = canvas.transform.GetChild(1).gameObject;
		levelOver = false;
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {

		if (!gameOver) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				paused = !paused;
				if (paused) {
					Time.timeScale = 0f;
					pausePanel.SetActive (true);
				} else {
					Time.timeScale = 1f;
					pausePanel.SetActive (false);
				}
			}
				
			if (SceneManager.GetActiveScene().name.Equals("Level 1") && Time.timeScale != 0f) {
				if (!isDone) {
					if (TimeSinceLevelLoad() % 1.5f <= 0.05f) {
						Instantiate (smallPrefab, spawnArray [randomGen.Next (1, 10)]);
					}
					if ((TimeSinceLevelLoad() + 0.055f) % 10f <= 0.05f) {
						Instantiate (midPrefab, spawnArray [randomGen.Next (1, 10)]);
					}
					if (TimeSinceLevelLoad() >= 60f) {
						bigEnemy = Instantiate (bigPrefab, spawnArray [randomGen.Next (1, 10)]);
						isDone = true;
					}
				} else if (bigEnemy == null) {
					levelOver = true;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
					if (enemies != null && enemies.Length > 0) {
						foreach (GameObject enemy in enemies) {
							Destroy (enemy, 0f);
						}
					}
					game.score = score;
					endPanel.SetActive (true);
				}
			}

			if (SceneManager.GetActiveScene().name.Equals("Level 2") && Time.timeScale != 0f) {
				if (!isDone) {
					if (TimeSinceLevelLoad() % 1.3f <= 0.05f) {
						Instantiate (smallPrefab, spawnArray [randomGen.Next (1, 10)]);
					}
					if ((TimeSinceLevelLoad() + 0.055f) % 10f <= 0.05f) {
						Instantiate (midPrefab, spawnArray [randomGen.Next (1, 10)]);
					}
					if ((TimeSinceLevelLoad() + 0.055f) % 7f <= 0.05f) {
						GameObject asteroid = Instantiate (asteroidPrefab, spawnArray [randomGen.Next (1, 10)]);
						asteroid.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-1.5f, 0f), ForceMode2D.Impulse);
					}
					if (TimeSinceLevelLoad() >= 120f) {
						bigEnemy = Instantiate (bigPrefab, spawnArray [randomGen.Next (1, 10)]);
						isDone = true;
					}
				} else if (bigEnemy == null) {
					levelOver = true;
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
					if (enemies != null && enemies.Length > 0) {
						foreach (GameObject enemy in enemies) {
							Destroy (enemy, 0f);
						}
					}
					GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("asteroid");
					if (asteroids != null && asteroids.Length > 0) {
						foreach (GameObject asteroid in asteroids) {
							Destroy (asteroid, 0f);
						}
					}
					endPanel.SetActive (true);
				}
			}
		}
		if (gameOver) {
			Time.timeScale = 0;
			endPanel.SetActive (true);
		}
	}

	float TimeSinceLevelLoad(){
		return Time.time - startTime;
	}

	public void RestartLevel(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ResetGame(){
		Time.timeScale = 1;
		game.ammo = 0;
		game.health = 0;
		game.score = 0;
		SceneManager.LoadScene ("Level 1");
	}

	public void NextLevel(){
		ShipController ship = GameObject.Find ("user_craft").GetComponent<ShipController> ();
		game.ammo = ship.ammo;
		game.health = ship.health;
		game.score = score;
		SceneManager.LoadScene ("Level 2");
	}

	public void Quit(){
		Application.Quit ();
	}
}
