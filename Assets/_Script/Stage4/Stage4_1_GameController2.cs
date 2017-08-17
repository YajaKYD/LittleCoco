using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_1_GameController2 : Controller {
	public GameObject Light;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Text_Importer2 ti;
	private Item_Controller ic;
	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;
	public GameObject portal2;

	public SpriteRenderer _blackout;
	private Color bb;

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
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		sceneNo = 41;

		if (!Stage4_Controller.q[0]) {
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}
			
		bb = new Color (0f, 0f, 0f, 1f); //검정,불투명
		_blackout.color = bb;

	}

	void Start () {
		ti.Import (sceneNo);
		//Save_Script.Save_Now_Point();
		if (!Stage4_Controller.q [0]) {
			ti.Talk ();
		} else if (Stage4_Controller.q [0]) {
			Destroy (GameObject.FindWithTag ("NPC"));
		}
			
		if (Stage4_Controller.q [10]) {
			Light.SetActive (false);
		}

		if (Stage4_Controller.q [13])
			portal2.SetActive (true);
		
	}

	void Update () {
		if (Stage4_Controller.q [0] && !Stage4_Controller.q [1]) {
			Q1_SayGoodnight ();
		} else if (Stage4_Controller.q [3] && !Stage4_Controller.q [4]) {
			Q4_CheckIvon ();	
		} else if (Stage4_Controller.q [7] && !Stage4_Controller.q [8]) {
			Q8_PutDoll ();
		} else if (Stage4_Controller.q [10] && !Stage4_Controller.q [11]) {
			Q11_CheckIvonAgain ();
		} else if (Stage4_Controller.q [12] && !Stage4_Controller.q [13]) {
			Q13_CheckIvonComplete ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller.q [2] && !Stage4_Controller.q [3]) {
			Stage4_Controller.q [3] = true; // check if ivon sleep
		} else if (other.CompareTag ("Player") && Stage4_Controller.q [11] && !Stage4_Controller.q [12]) {
			Stage4_Controller.q [12] = true;
		}

//		if (other.CompareTag ("Player") && Stage4_Controller.q[7] && !Stage4_Controller.q[8]) {
//			Stage4_Controller.q[8] = true;
//			//ti.currLineArr [0] += 2; 
//			//ti.NPC_Say_yeah ("코코"); // ivon fell asleep
//			print ("Check Again");
//		}
	}

	void Q1_SayGoodnight(){
		Destroy (GameObject.FindWithTag ("NPC"));
		ti.Talk (ti.lineNo + 2);
		Stage4_Controller.q [1] = true;
	}

	void Q4_CheckIvon(){
		ti.Talk (ti.lineNo + 2);
		Stage4_Controller.q [4] = true;
	}

	void Q8_PutDoll(){
		if (ic._now_used_item == "StarDoll") {
			Stage4_Controller.q[8] = true;
			Light.SetActive (false);
			print ("Change Image with doll and bed");
		}
	}

	void Q11_CheckIvonAgain(){
		ti.Talk (ti.lineNo + 2);
		Stage4_Controller.q [11] = true;
	}

	void Q13_CheckIvonComplete(){
		ti.Talk (ti.lineNo + 2);
		Stage4_Controller.q [13] = true;
	}
}
