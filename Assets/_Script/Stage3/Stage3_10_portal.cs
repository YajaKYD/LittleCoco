﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_10_portal : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("collision");
		if (other.gameObject.tag == "Player") {
			Stage3_Controller._Stage3_Quest[19] = true;
			Save_Script.Save_Quest_Info ();
			Debug.Log ("perfect");
		}
	}
}