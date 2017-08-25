using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_3_gum : MonoBehaviour {

	public GameObject gumPuzzle;
	public GameObject gumPuzzlePrefab;
	public GameObject background;
	public Sprite background4_3;
	private GameObject item_Canvas;
	private GameObject menu;
	private Stage4_3_GameController2 controller;
	private GameObject player;
	private Text_Importer2 ti;

	void Start () {
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
		menu = GameObject.FindWithTag ("Setting");
		controller = GameObject.Find ("Stage4_3_GameController").GetComponent<Stage4_3_GameController2> ();
		player = GameObject.FindWithTag ("Player");
	}
		
	void OnTriggerEnter2D(){
		if(!Stage4_Controller.q[19]) ti.Talk ();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") && Stage4_Controller.q[19]) {
			if (gumPuzzle == null && !Stage4_Controller.q [20]) {
				item_Canvas.SetActive (false);
				player.GetComponent<Moving_by_RLbuttons> ().SetState (CocoState.Idle);
				player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
				gumPuzzle = Instantiate (gumPuzzlePrefab, Vector3.zero, Quaternion.identity) as GameObject;
				gumPuzzle.transform.SetParent (menu.transform, false);
				gumPuzzle.transform.SetAsFirstSibling ();
				controller.emptyPos [0] = gumPuzzle.transform.GetChild (1);
				controller.emptyPos [1] = gumPuzzle.transform.GetChild (2);
				controller.puzzlePiece [0] = gumPuzzle.transform.GetChild (3).gameObject;
				controller.puzzlePiece [1] = gumPuzzle.transform.GetChild (4).gameObject;
				ti.Talk (ti.lineNo + 2);
				//background.GetComponent<SpriteRenderer> ().sprite = null;
			}
		}
	}
		

	void OnTriggerExit2D(){
		//Destroy (gumPuzzle);
		//background.GetComponent<SpriteRenderer> ().sprite = background4_3;
	}
}
