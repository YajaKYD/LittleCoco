using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_1_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Item_Controller ic;
	private Stage2_Controller s2c;

	private bool a1a1 = true;
	private bool a2a2 = true;
	private bool a3a3 = true;

	public GameObject clockwork;
	public GameObject[] multiTap;
	public GameObject _coco_textbox;

	//Test용//
//	public GameObject a1;
//	public GameObject b1;

	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		s2c = GameObject.Find ("Stage2_Controller").GetComponent<Stage2_Controller> ();

		if (!Stage2_Controller._Stage2_Quest[0]) {
			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();

			aa.Import ();
			//s2c._ti_stage2 = aa;
			s2c.ic = ic;
			//s2c._dialogue_Canvas = GameObject.FindWithTag ("Dialogue");
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 12) {
			player.transform.position = regen_pos.position;
		} else {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.NPC_Say_yeah ("코코");
		}

		if (Stage2_Controller._Stage2_Quest[0]) {
//			Destroy (a1);
//			Destroy (b1);	//이거 안꺼놓으면 nullreference라서 태엽하고 멀티탭 살아남
			Destroy (clockwork);
		}

		if (Stage2_Controller._Stage2_Quest[4]) {
			Destroy (multiTap [0]);
			Destroy (multiTap [1]);
		}
	}

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W)) {
			print ("goto2end");
			SceneManager.LoadScene (14);
		}
		//

		if(!Stage2_Controller._Stage2_Quest[0]){
			Q1_pick_clockwork_up ();
		}
			
		if (Stage2_Controller._Stage2_Quest[3] && !Stage2_Controller._Stage2_Quest[4]) {
			Q5_multiTap ();
		}

	}


	void Q1_pick_clockwork_up(){
		if (a1a1) {
			_coco_textbox = GameObject.Find ("코코_text");
		}

		if (a2a2 && !_coco_textbox) {
			clockwork.GetComponent<Collider2D> ().enabled = true;
			a2a2 = false;
			a1a1 = false;
		} //XoX 대사 넘기면 태엽을 줍는다.

		if (!a2a2) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");

			Stage2_Controller._Stage2_Quest[0] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q5_multiTap(){
		if (a3a3) {
			multiTap [0].GetComponent<BoxCollider2D> ().enabled = true;
			multiTap [1].GetComponent<BoxCollider2D> ().enabled = true;
			a3a3 = false;
		}

		if (!multiTap [0]) {
			Stage2_Controller._Stage2_Quest[4] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

}
