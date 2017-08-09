using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Position : MonoBehaviour {

	private Camera mainCam;
	RectTransform rectTransform;
	private GameObject textPos;
	public string nameFind;

	void Awake () {
		mainCam = Camera.main;
		rectTransform = GetComponent<RectTransform> ();
		Debug.Log (nameFind);
	}

	void OnEnable(){
		mainCam = Camera.main;

		if (GameObject.Find (nameFind + "TextPos") != null) {
			textPos = GameObject.Find (nameFind + "TextPos");
			//Debug.Log ("rect position 1 " + rectTransform.position + ", " + nameFind + "textPos " + textPos.transform.position);
			rectTransform.position = mainCam.WorldToScreenPoint (textPos.transform.position);
			//Debug.Log ("rect position 2 " + rectTransform.position + "textPos " + textPos.transform.position);
		}

	}
}
