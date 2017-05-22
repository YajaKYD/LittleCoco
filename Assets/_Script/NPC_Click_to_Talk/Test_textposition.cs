using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_textposition : MonoBehaviour {

	public Camera aa;
	RectTransform s;
	public GameObject bb;

	void Start () {
		aa = Camera.main;
		s = GetComponent<RectTransform> ();
	}

	void OnEnable(){
		aa = Camera.main;
		bb = GameObject.Find ("IvonTextPos");
		if (bb) {
			s.position = aa.WorldToScreenPoint (bb.transform.position);
		}
	}
}
