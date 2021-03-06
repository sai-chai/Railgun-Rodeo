﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartPanelController : MonoBehaviour {

	private GameObject[] children;
	private LevelController level;
	private GameObject[] intro;
	public AudioMixer audio;
	private int introIndex;
	public GameObject miscUI;


	// Use this for initialization
	void Start () {
		level = GameObject.Find ("level").GetComponent<LevelController> ();
		GameObject temp = gameObject;
		children = Traversal.GetChildren (ref temp);
		intro = Traversal.GetChildren (ref children [3]);
		gameObject.SetActive (true);
		audio.FindSnapshot ("Pause").TransitionTo (0f);
		Time.timeScale = 0;
		introIndex = 0;
	}

	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene ().name.Equals ("Level 1")){
			if (introIndex < intro.Length-1 && Input.anyKeyDown) {
				intro [introIndex].SetActive(false);
				introIndex++;
				intro [introIndex].SetActive(true);
				if (introIndex == 3) {
					children [1].SetActive (false);
					children [4].SetActive (true);
					children [5].SetActive (true);
				}
			}
		}

		if (SceneManager.GetActiveScene ().name.Equals ("Level 2")) {
			if (introIndex < intro.Length-1 && Input.anyKeyDown) {
				intro [introIndex].SetActive(false);
				introIndex++;
				intro [introIndex].SetActive(true);
				if (introIndex == 1) {
					children [1].SetActive (false);
					children [4].SetActive (true);
					children [5].SetActive (true);
				}
			}
		}
	}

	public void StartGame(){
		gameObject.SetActive (false);
		Time.timeScale = 1;
		audio.FindSnapshot ("Play").TransitionTo (0f);
		miscUI.SetActive(true);
	}

	public void ShowDirections(){
		if (!children [2].activeSelf) {
			children [2].SetActive (true);
		} else {
			children [2].SetActive (false);
		}
	}
}
