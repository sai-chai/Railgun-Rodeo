using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public ShipController ship;
	public LevelController level;
	public Text health;
	public Text ammo;
	public Text score;

	// Use this for initialization
	void Start () {
		ship = GameObject.FindWithTag ("spaceship").GetComponent<ShipController>();
		level = GameObject.FindWithTag ("game").GetComponent<LevelController>();
		health.text = "Health: " + ship.health.ToString();
		ammo.text = "Ammo: " + ship.ammo.ToString();
		score.text = "Score: " + level.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		health.text = "Health: " + ship.health.ToString();
		ammo.text = "Ammo: " + ship.ammo.ToString();
		score.text = "Score: " + level.score.ToString ();
	}
}
