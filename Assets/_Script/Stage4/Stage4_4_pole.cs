using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_4_pole : MonoBehaviour {

	public GameObject posterPrefab;
	public GameObject poster;
	private GameObject item_Canvas;

	void Start () {
		//item_Canvas = GameObject.FindWithTag ("Item_Canvas");
	}

	void OnMouseDown(){
		Debug.Log ("mouse down");
		if (poster == null) {
			poster = Instantiate (posterPrefab, Vector3.forward, Quaternion.identity) as GameObject;
			//poster.transform.SetParent (Item_Canvas.transform, false);
		} else {
			poster.SetActive (!poster.activeSelf);
		}
	}
}
