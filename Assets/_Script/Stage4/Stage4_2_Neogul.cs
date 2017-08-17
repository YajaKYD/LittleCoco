using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_Neogul : MonoBehaviour {

	private Text_Importer2 ti;
	private Stage4_2_GameController controller;

	// Use this for initialization
	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		controller = GameObject.Find ("Stage4_2_GameController").GetComponent<Stage4_2_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller.q[16] && !Stage4_Controller.q[17]) {
			ti.Talk (ti.lineNo + 2);
		}
	}
}
