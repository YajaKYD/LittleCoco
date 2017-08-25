using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_4_Collider : MonoBehaviour {

	private GameObject player;
	private Moving_by_RLbuttons mbr;

	private Text_Importer2 ti;


	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (21);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player) {
			mbr.enabled = false;
			StartCoroutine ("Backback");
		}
	}

	IEnumerator Backback(){
		for (int i = 0; i < 30; i++) {
			mbr.Moving_Right (8f);
			yield return null;
		}
//		Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//		int temp = aa.currLineArr [0];
//		aa.currLineArr [0] = 6;
//		aa.NPC_Say_yeah ("코코");
//		//mbr.enabled = false;
//		aa.currLineArr [0] = temp +1;
		//mbr.enabled = true;
		ti.Talk(1);
	}
}
