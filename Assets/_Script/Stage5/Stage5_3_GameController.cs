using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_3_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	public BoxCollider2D goto_5_5;
	public GameObject warning;

	private bool q1a1 = false;
	private bool q1a2 = false;
	private bool q1a3 = false;
	private bool q1a4 = false;
	private bool q1a5 = false;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;
	}

	void Start(){

//		if (GetComponent<Load_data> ()._where_are_you_from == 33) {
//			player.transform.position = regen_pos.position;
//		}

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];

		if (Stage5_Controller._Stage5_Quest [24] && !Stage5_Controller._Stage5_Quest [25]) {
			Save_Script.Save_Now_Point ();
			//시작할 때 저장
		}
		if (Stage5_Controller._Stage5_Quest [25]) {
			goto_5_5.enabled = true;
		}
	}

	void Update(){
		if (Stage5_Controller._Stage5_Quest [24] && !Stage5_Controller._Stage5_Quest [25]) {
			Q1_firstcon ();
		}
	}

	void Q1_firstcon(){
		if (!q1a1) {
			StartCoroutine (Warigari ());
			q1a1 = true;
		}
		if (q1a2 && !_star_textbox.activeSelf) {
			goto_5_5.enabled = true;
			Stage5_Controller._Stage5_Quest [25] = true;
		}
	}

	IEnumerator Warigari(){
		print ("전전긍긍");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		ti.currLineArr [0] = 53; //도대체 어디로
		ti.NPC_Say_yeah("별감");
		q1a2 = true;
	}
}
