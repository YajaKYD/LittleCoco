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

	void Update () {
		if (bb) {
			s.position = aa.WorldToScreenPoint (bb.transform.position);
		}
	}
}
