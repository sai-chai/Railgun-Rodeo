using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTraversal{

	/**
	 *	GetChildren : GameObject[]
	 *	index : ref GameObject 
	 *	NOTE: requires pass-by-reference
	 **/
	public static GameObject[] GetChildren(ref GameObject index){
		int childCount = index.transform.childCount;
		GameObject[] result = new GameObject[childCount];
		for (int i = 0; i < childCount; i++) {
			result [i] = index.transform.GetChild (i).gameObject;
		}
		return result;
	}

	/**
	 *	GetParent : GameObject[]
	 *	index : ref GameObject 
	 *	NOTE: requires pass-by-reference
	 **/
	public static GameObject GetParent(ref GameObject index){
		return index.GetComponentInParent<RectTransform> ().gameObject;
	}

	/**
	 *	GetChild : GameObject[]
	 *	current : ref GameObject
	 *	index : int 
	 *	NOTE: requires pass-by-reference
	 **/
	public static GameObject GetChild(ref GameObject current, int index){
		return current.GetComponentsInChildren<RectTransform> (true)[index].gameObject;
	}
}
