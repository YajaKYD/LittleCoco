using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_Neogul : MonoBehaviour {

	private Text_Importer ti;
	private Stage4_2_GameController controller;

	// Use this for initialization
	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		controller = GameObject.Find ("Stage4_2_GameController").GetComponent<Stage4_2_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(){
		controller.q13_meetNeogul = true;
	}
}
