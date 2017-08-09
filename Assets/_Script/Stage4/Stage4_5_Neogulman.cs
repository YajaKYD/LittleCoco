using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_Neogulman : MonoBehaviour {

	private Text_Importer2 ti;

	void Start () {
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
	}

	void Update(){
		if (!Stage4_Controller.q[17] && Stage4_Controller.q [37]) {
			StartCoroutine ("Disappear");
		}
	}

	void OnTriggerEnter2D(){
		if (!Stage4_Controller.q [17]) {
			ti.Talk (ti.lineNo + 2); //conversation
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	IEnumerator Disappear(){
		this.gameObject.SetActive(false); // add animation
		Stage4_Controller.q[17] = true;
		ti.Talk (ti.lineNo + 2);
		yield return null;
	}
}
