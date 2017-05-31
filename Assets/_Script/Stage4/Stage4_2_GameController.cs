using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_GameController : MonoBehaviour {

	public BoxCollider2D Star;

	private Transform start_pos;
	private GameObject player;
	private Text_Importer ti;
	private Item_Controller ic;

	private bool a1a1 = false;
	private bool a1a2 = false;
	private bool a1a3 = false;

	private bool a0a0 = false;
	private bool a0a1 = false;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
		//ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		//s2c = GameObject.Find ("Stage2_Controller").GetComponent<Stage2_Controller> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void Start(){

		if (Stage4_Controller.q6) {
			Star.enabled = true;
		}
		if (Stage4_Controller.q7) {
			Destroy (Star.gameObject);
		}
	}

	void Update(){
		if (Stage4_Controller.q6 && !Stage4_Controller.q7) {
			Q4_GetaDoll ();
		}
	}

	void Q4_GetaDoll(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				Stage4_Controller.q7 = true;
				Save_Script.Save_Quest_Info ();
				break;
			}
		}
	}
}
