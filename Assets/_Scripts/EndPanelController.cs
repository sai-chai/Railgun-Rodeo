using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanelController : MonoBehaviour {

	public LevelController level;
	private bool done = false;

	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf && !done) {
			GameObject.Find("miscUI").SetActive(false);
			done = true;
			if(SceneManager.GetActiveScene().name.Equals("Level 1")){
				GameObject temp = gameObject;
				if(!level.gameOver){
					Traversal.GetChild(ref temp, 3).SetActive(true);
					Traversal.GetChild(ref temp, 1).GetComponent<Text> ().text = "Score: " + level.score.ToString();
				} else {
					Traversal.GetChild(ref temp, 2).SetActive(true);
					Traversal.GetChild(ref temp, 4).SetActive(true);
				}
			}
			if(SceneManager.GetActiveScene().name.Equals("Level 2")){
				GameObject temp = gameObject;
				if(!level.gameOver){
					Traversal.GetChild(ref temp, 3).SetActive(true);
					Traversal.GetChild(ref temp, 1).GetComponent<Text> ().text = "Score: " + level.score.ToString();
				} else {
					Traversal.GetChild(ref temp, 1).GetComponent<Text> ().text = "Score: " + level.score.ToString();
					Traversal.GetChild(ref temp, 3).SetActive(true);
					Traversal.GetChild(ref temp, 2).SetActive(true);
				}
			}
		} 
	}
}
