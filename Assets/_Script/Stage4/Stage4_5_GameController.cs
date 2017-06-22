using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_GameController : MonoBehaviour {

	public GameController controller;
	private GameObject player;
	public Transform startPos;
	private bool q19_1;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		player.transform.Translate (startPos.position);
	}

	void Start () {
		controller = GameObject.Find ("PuzzleController").GetComponent<GameController> ();
		//controller.StartGame (1);
	}

	void Update () {
		if (Stage4_Controller.q [17] && !Stage4_Controller.q [18]) {
			Q18_getCard ();
		} else if(Stage4_Controller.q[18] && !Stage4_Controller.q[19]){
			Q19_cardPuzzle ();
		}
	}

	void Q18_getCard(){
		if (!Stage4_Controller.q18 [0]) {
			//conversation
			//if get card -> q18[0] = true;
		} else if (!Stage4_Controller.q18 [1]) {
			//conversation
			//if coco get card -? q18[1] = true;
		} else if (Stage4_Controller.q18 [0] && Stage4_Controller.q18 [1]) {
			Stage4_Controller.q [18] = true;
		}
	}

	void Q19_cardPuzzle(){
		if (!q19_1) {
			controller.StartGame (1);
			q19_1 = true;
		}
		// if puzzle solved, q[19] = true;
	}

}
