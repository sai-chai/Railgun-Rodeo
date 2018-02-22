using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float scrollSpeed;
	private GameObject opposite;
	private Vector3 nextPosition;
	private bool inFront;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		if (gameObject.name.Equals ("space")) {
			opposite = GameObject.Find ("space2");
			inFront = false;
		} else if(gameObject.name.Equals("space2")){
			opposite = GameObject.Find ("space");
			inFront = true;
		}
		nextPosition = new Vector3 (21.9f, 0f, 0f);
		scrollSpeed = -1.25f;
		rb2d.velocity = new Vector2 (scrollSpeed, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!inFront) {
			if (opposite.transform.position.x <= 0) {
				transform.SetPositionAndRotation (nextPosition, Quaternion.identity);
				inFront = true;
				opposite.GetComponent<BgScroller>().inFront = false;
			}
		}
	}
}
