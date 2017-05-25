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

	private bool a1a1 = false;
	private bool a1a2 = false;
	private bool a1a3 = false;

	private bool a0a0 = false;
	private bool a0a1 = false;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		//ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		//s2c = GameObject.Find ("Stage2_Controller").GetComponent<Stage2_Controller> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		if (!Stage4_Controller._stage4_q1) {
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 26) {
			player.transform.position = regen_pos.position;
		} else {
			ti.NPC_Say_yeah ("이본");
		}

		if (Stage4_Controller._stage4_q1) {
			GameObject ivon = GameObject.FindWithTag ("NPC");
			ivon.SetActive (false);
		}
	}

	void Update(){
		if (!Stage4_Controller._stage4_q1) {
			Q1_SayGoodnight ();
		}

		if (Stage4_Controller._stage4_q1 && !Stage4_Controller._stage4_q2) {
			Q2_CheckIvon ();
		}

		if (Stage4_Controller._stage4_q2 && Stage4_Controller._stage4_q3 && !Stage4_Controller._stage4_q4) {
			Q3_SayandReturn ();
		}

		if (Stage4_Controller._stage4_q7 && !Stage4_Controller._stage4_q8) {
			Q5_PutDoll ();
		}


	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller._stage4_q2 && !Stage4_Controller._stage4_q3) {
			Stage4_Controller._stage4_q3 = true;
			Save_Script.Save_Quest_Info ();
		}

		if (other.CompareTag ("Player") && Stage4_Controller._stage4_q8 && !Stage4_Controller._stage4_q9) {
			Stage4_Controller._stage4_q9 = true;
			Save_Script.Save_Quest_Info ();
			print ("Check Again");
		}
	}

	void Q1_SayGoodnight(){
		if (!ti._text_boxes [3].activeSelf) {
			GameObject ivon = GameObject.FindWithTag ("NPC");
			ivon.SetActive (false);
			Save_Script.Save_Dialogue_Info ();
			Stage4_Controller._stage4_q1 = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q2_CheckIvon(){
		if (!a0a0) {
			ti.NPC_Say_yeah ("별감");
			Save_Script.Save_Dialogue_Info ();
			a0a0 = true;
		}
		if (!ti._text_boxes [1].activeSelf && !a1a1) {
			ti.NPC_Say_yeah ("코코");
			Save_Script.Save_Dialogue_Info ();
			a1a1 = true;
		}
		if (a1a1 && !ti._text_boxes [0].activeSelf && !a1a2) {
			ti.currLineArr [1] += 2;
			ti.NPC_Say_yeah ("별감");
			Save_Script.Save_Dialogue_Info ();
			a1a2 = true;
		}
		if (a1a2 && !ti._text_boxes [1].activeSelf) {
			Stage4_Controller._stage4_q2 = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q3_SayandReturn(){
		if (!a0a1) {
			ti.currLineArr [0] += 2;
			ti.NPC_Say_yeah ("코코");
			Save_Script.Save_Dialogue_Info ();
			a0a1 = true;
		}
		if (a0a1 && !ti._text_boxes [0].activeSelf && !a1a3) {
			ti.currLineArr [1] += 2;
			ti.NPC_Say_yeah ("별감");
			Save_Script.Save_Dialogue_Info ();
			a1a3 = true;
			Stage4_Controller._stage4_q4 = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q5_PutDoll(){
		if (ic._now_used_item == "StarDoll") {
			Stage4_Controller._stage4_q8 = true;
			Save_Script.Save_Quest_Info ();
			Light.SetActive (false);
			print ("Change Image with doll and bed");
		}
	}
}
