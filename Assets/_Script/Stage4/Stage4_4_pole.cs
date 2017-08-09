using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_4_pole : MonoBehaviour {

	public GameObject posterPrefab;
	public GameObject poster;
	private GameObject item_Canvas;
	private Text_Importer2 ti;
	private Stage4_4_GameController2 controller;

	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		controller = GameObject.Find ("Stage4_4_GameController").GetComponent<Stage4_4_GameController2>();
	}

	void OnTriggerEnter2D(){
		if (Stage4_Controller.q [30] && !Stage4_Controller.q [31]) {
			ti.Talk (ti.lineNo + 2);
		} else {
			ti.Talk ();
		}
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
