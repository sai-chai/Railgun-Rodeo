using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController singleton;
	public int health;
	public int ammo;
	public int score;

	void Awake ()   
	{
		if (singleton == null)
		{
			DontDestroyOnLoad(gameObject);
			singleton = this;
		}
		else if (singleton != this)
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		health = 0;
		ammo = 0;
		score = 0;
	}

}
