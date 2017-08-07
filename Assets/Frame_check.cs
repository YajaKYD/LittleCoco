using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame_check : MonoBehaviour {

	Text aa;

	void Start () {
		aa = GetComponentInChildren<Text> ();
		DontDestroyOnLoad (gameObject);

		Cursor.visible = false;
	}

	void Update () {
		aa.text = "테스트입니다. 계속 40 이하이면 말씀해주세요 : " + ((int)(1 / Time.deltaTime)).ToString();
//		if(Input.GetKeyDown(KeyCode.Escape)){
//			Application.Quit ();
//		}
	}
}
