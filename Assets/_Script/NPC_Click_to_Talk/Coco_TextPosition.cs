using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coco_TextPosition : MonoBehaviour {

	public Camera aa;
	RectTransform s;
	public GameObject bb;

	void Awake () {
		bb = GameObject.Find ("Player");
		s = GetComponent<RectTransform> ();
	}

	void Update () {
		aa = Camera.main;
		s.position = aa.WorldToScreenPoint(bb.transform.GetChild(3).gameObject.transform.position);
		if(this.name == "munch"){ 
			s.rotation = bb.transform.rotation;

		}
	}
}
