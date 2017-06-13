using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_3_gum : MonoBehaviour {

	public GameObject gumPuzzle;
	private GameObject item_Canvas;

	void Start () {
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
	}

	void OnTriggerEnter2D(){
		gumPuzzle = Instantiate (gumPuzzle, Vector3.zero, Quaternion.identity) as GameObject;
		gumPuzzle.transform.SetParent (item_Canvas.transform, false);
	}
}
