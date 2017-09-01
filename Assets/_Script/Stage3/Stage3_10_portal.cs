using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_10_portal : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("collision");
		if (other.gameObject.tag == "Player") {
			Stage3_Controller.q[19] = true; //?
			//Debug.Log ("perfect");

		}
	}
}
