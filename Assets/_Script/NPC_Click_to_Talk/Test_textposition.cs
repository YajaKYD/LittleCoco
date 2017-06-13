using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_textposition : MonoBehaviour {

	public Camera aa;
	RectTransform s;
	public GameObject bb;

	void Awake () {
		aa = Camera.main;
		s = GetComponent<RectTransform> ();
		if (GameObject.Find ("IvonTextPos") != null) {
			bb = GameObject.Find ("IvonTextPos");
		}
	}

	void OnEnable(){
		aa = Camera.main;
		if (GameObject.Find ("IvonTextPos") != null) {
			bb = GameObject.Find ("IvonTextPos");
		}

		if (bb) {
			s.position = aa.WorldToScreenPoint (bb.transform.position);
		}

	}
}
