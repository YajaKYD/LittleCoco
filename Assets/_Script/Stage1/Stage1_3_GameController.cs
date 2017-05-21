using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1_3_GameController : MonoBehaviour {

	private Transform start_pos;
	private GameObject player;
	public Outline o_l_1;
	public Outline o_l_2;
	private Item_Controller ic;
	public bool puzzle_end;
	public GameObject dog_in_a_cart;
	public GameObject cart;
	public GameObject portal_to_next;
	public GameObject _coco_textbox;

	public GameObject[] wheels;

	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;

		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		o_l_1.used_or_not_for_retry = true;
		o_l_2.used_or_not_for_retry = true;

		player.transform.position = start_pos.position;

		for (int i = 0; i < 5; i++) { //s3 시작 시 템 없어야함. 카트에서 거울먹고 종료 후 이어하기 시 거울이 있ㅡㄴ 것ㅡㄹ 방지함
			ic._item_name_list [i] = "";
			ic._usable_item[i] = false;
			ic._the_number_of_items[i] = 0;
			ic._interaction_object [i] = "";
			ic._consumable [i] = false;
			ic._item_list [i].GetComponent<Image> ().color = new Color (1, 1, 1, 0);
			ic._item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 0);
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			ic._explanations[i] = "";
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}


	}

	void Update(){
		
		if (ic.cant_pick_during_using) {
			o_l_1.eraseRenderer = true;
			o_l_2.eraseRenderer = true;
		}

		if (puzzle_end) {
			Vector3 ss = Vector3.zero;
			wheels[0].transform.Rotate(new Vector3(0,0,-10f));
			wheels[1].transform.Rotate(new Vector3(0,0,-10f));	
			dog_in_a_cart.transform.position = Vector3.SmoothDamp (dog_in_a_cart.transform.position, new Vector3 (13.5f, 0, 7f), ref ss,0.2f);
			if (Vector3.Distance (dog_in_a_cart .transform.position, new Vector3 (13.5f, 0, 7f)) < 0.5f) {
				puzzle_end = false;
				dog_in_a_cart.SetActive (false);
				cart.SetActive (true);
				cart.transform.position = new Vector3 (10.5f, -1.37f, 7f);
				player.SetActive (true);
				player.transform.position = new Vector3 (14.5f, -1.37f, 7f);
				Stage1_Controller._Stage1_Quest[3] = true;
				Save_Script.Save_Quest_Info ();
				GameObject.Find ("Main Camera").GetComponent<CameraManager> ().FocusObject = player;
			}
		}

		if (_coco_textbox != null) {
			if (Stage1_Controller._Stage1_Quest [4] && !_coco_textbox.activeSelf) { //이동포인트 도착 후 대사 끝냄
				portal_to_next.SetActive (true);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){ //이동포인트에 도착.
//		if (other.gameObject == player) {
//			//말하고 뒤로 자동으로 움직임?
//			mbr.enabled = false;
//			StartCoroutine ("Backback");
//		}
		Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		aa.currLineArr [0] = 5;//코코 다음대사로 넘김
		aa.NPC_Say_yeah ("코코");
		Stage1_Controller._Stage1_Quest[4] = true;

		Save_Script.Save_Quest_Info ();
		_coco_textbox = GameObject.Find ("코코_text");
	}

}
