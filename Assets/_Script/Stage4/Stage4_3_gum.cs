using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_3_gum : MonoBehaviour {

	public GameObject gumPuzzle;
	public GameObject gumPuzzlePrefab;
	private GameObject item_Canvas;

	void Start () {
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
	}

	void OnTriggerEnter2D(){
		if (gumPuzzle == null) {
			gumPuzzle = Instantiate (gumPuzzlePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			gumPuzzle.transform.SetParent (item_Canvas.transform, false);
		}
	}

	void OnTriggerExit2D(){
		Destroy (gumPuzzle);
	}
}
