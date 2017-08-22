using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_6_Racoon : MonoBehaviour {

	public Text_Importer2 ti;

	// Use this for initialization
	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
	}

	void OnTriggerEnter2D(){
		if (!Stage4_Controller.q [35]) {
			ti.Talk ();
		}
	}

}
