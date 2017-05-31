using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_1_GameController : MonoBehaviour {

	public GameObject Light;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Text_Importer ti;
	private Item_Controller ic;
	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;

	private bool q2_1 = false;
	private bool q2_2 = false;
	private bool q3_1 = false;

	private bool q2_0 = false;
	private bool q3_0 = false;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		textbox_Coco = ti._text_boxes [0];
		textbox_Star = ti._text_boxes [1];
		textbox_Racoon = ti._text_boxes [2];
		textbox_Ivon = ti._text_boxes [3];

		if (!Stage4_Controller.q1) {
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 26) {
			player.transform.position = regen_pos.position;
			Destroy(GameObject.FindWithTag ("NPC"));
		} else {
			ti.NPC_Say_yeah ("이본");
		}
	}

	void Update(){
		if (!Stage4_Controller.q1) {
			Q1_SayGoodnight ();
		}

		if (Stage4_Controller.q1 && !Stage4_Controller.q2) {
			Q2_CheckIvon ();
		}

		if (Stage4_Controller.q3 && !Stage4_Controller.q4) {
			Q3_SayandReturn ();
		}

		if (Stage4_Controller.q7 && !Stage4_Controller.q8) {
			Q5_PutDoll ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller.q2 && !Stage4_Controller.q3) {
			Stage4_Controller.q3 = true; // check if ivon sleep
		}

		if (other.CompareTag ("Player") && Stage4_Controller.q8 && !Stage4_Controller.q9) {
			Stage4_Controller.q9 = true;
			print ("Check Again");
		}
	}

	void Q1_SayGoodnight(){
		if (!textbox_Ivon.activeSelf) {
			Destroy(GameObject.FindWithTag ("NPC"));
			Stage4_Controller.q1 = true;
		}
	}

	void Q2_CheckIvon(){
		if (!q2_0) {
			ti.NPC_Say_yeah ("별감"); // 
			q2_0 = true;
		}
		if (q2_0 && !textbox_Star.activeSelf && !q2_1) {
			ti.NPC_Say_yeah ("코코"); // ivon
			q2_1 = true;
		}
		if (q2_1 && !textbox_Coco.activeSelf && !q2_2) {
			ti.currLineArr [1] += 2; // next line
			ti.NPC_Say_yeah ("별감");
			q2_2 = true;
		}
		if (q2_2 && !textbox_Star.activeSelf) { // end of talking
			Stage4_Controller.q2 = true;
		}
	}

	void Q3_SayandReturn(){
		if (!q3_0) {
			ti.currLineArr [0] += 2; 
			ti.NPC_Say_yeah ("코코"); // music
			q3_0 = true;
		}
		if (q3_0 && !textbox_Coco.activeSelf && !q3_1) {
			ti.currLineArr [1] += 2;
			ti.NPC_Say_yeah ("별감"); // ivon listening music
			q3_1 = true;
			Stage4_Controller.q4 = true;
		}
	}

	void Q5_PutDoll(){
		if (ic._now_used_item == "StarDoll") {
			Stage4_Controller.q8 = true;
			Light.SetActive (false);
			print ("Change Image with doll and bed");
		}
	}
}
