using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage5Paper : MonoBehaviour {

	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private bool q1 = false;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && !q1 && Stage5_Controller._Stage5_Quest[26]) {
			StartCoroutine (Paper ());
			q1 = true;
		}
	}

	IEnumerator Paper(){
		mbr.enabled = false;
		print ("신문뒤짐");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		Stage5_Controller._Stage5_Quest [27] = true;
	}

}
