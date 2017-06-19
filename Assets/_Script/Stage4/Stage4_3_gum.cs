using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_3_gum : MonoBehaviour {

	public GameObject gumPuzzle;
	public GameObject gumPuzzlePrefab;
	private GameObject item_Canvas;
	private Stage4_3_GameController controller;

	void Start () {
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
		controller = GameObject.Find ("Stage4_3_GameController").GetComponent<Stage4_3_GameController> ();
	}

	void OnTriggerEnter2D(){
		if (gumPuzzle == null) {
			gumPuzzle = Instantiate (gumPuzzlePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			gumPuzzle.transform.SetParent (item_Canvas.transform, false);
			controller.emptyPos [0] = gumPuzzle.transform.GetChild (1);
			controller.emptyPos [1] = gumPuzzle.transform.GetChild (2);
			controller.puzzlePiece [0] = gumPuzzle.transform.GetChild (3).gameObject;
			controller.puzzlePiece [1] = gumPuzzle.transform.GetChild (4).gameObject;
		}
	}

	void OnTriggerExit2D(){
		Destroy (gumPuzzle);
	}
}
