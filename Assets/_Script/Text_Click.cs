using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Click : MonoBehaviour {

	private Text_Importer2 ti;
	public bool aa = true;


	void Awake(){
		ti = GetComponentInParent<Text_Importer2> ();
	}

	void OnMouseDown(){
		if (transform.parent.gameObject.name == "Coco_text") {
			for (int i = 0; i < ti.cocoDialogue.Length; i++) {
				ti.cocoDialogue [i].SetActive (false);
			}
		}
		transform.parent.gameObject.SetActive (false);
		
		aa = ti.Talk ();

		if (!aa) {
			if (transform.parent.gameObject.name == "Coco_text") {
				for(int i = 0 ; i < ti.cocoDialogue.Length ; i++){
					ti.cocoDialogue [i].SetActive (false);
				}
			}
			transform.parent.gameObject.SetActive (false);
		}
	}
}
