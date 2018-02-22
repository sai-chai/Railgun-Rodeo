using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanelController : MonoBehaviour {

	public LevelController level;


	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf) {
			gameObject.transform.GetChild (1).GetComponent<Text> ().text = "Score: " + level.score.ToString();
		}
	}
}
