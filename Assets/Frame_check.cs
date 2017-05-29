using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame_check : MonoBehaviour {

	Text aa;

	void Start () {
		aa = GetComponentInChildren<Text> ();
		DontDestroyOnLoad (gameObject);
	}

	void Update () {
		aa.text = (1 / Time.deltaTime).ToString();
//		if(Input.GetKeyDown(KeyCode.Escape)){
//			Application.Quit ();
//		}
	}
}
