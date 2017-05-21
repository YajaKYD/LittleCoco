using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_2_GameController : MonoBehaviour {

	private GameObject player;
	private Transform start_pos;
	private Transform regen_pos;
	private Item_Controller _ic;

	public GameObject[] _toys;
	public Outline _air;

	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		_ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller>();
		player.transform.position = start_pos.position;
	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 17) {
			player.transform.position = regen_pos.position;
		}

		if (Stage3_Controller._Stage3_Quest[1] && !Stage3_Controller._Stage3_Quest[2]) {
			_toys [0].GetComponent<Toys> ()._pickable = true;
			_toys [1].GetComponent<Toys> ()._pickable = true;
			_toys [2].GetComponent<Toys> ()._pickable = true; 
		}

		if (Stage3_Controller._Stage3_Quest[2]) { //공을 가져다준 후엔 주울 수 없음.
			_toys [0].GetComponent<Toys> ()._pickable = false;
			_toys [1].GetComponent<Toys> ()._pickable = false;
			_toys [2].GetComponent<Toys> ()._pickable = false;
        }

		if (Stage3_Controller._Stage3_Quest[3]) {
			Destroy (_toys [0]);
		}
		if (Stage3_Controller._Stage3_Quest[4]) {
			Destroy (_toys [1]);
		}
		if (Stage3_Controller._Stage3_Quest[5]) {
			Destroy (_toys [2]);
		}
	}

	void Update(){

		if (Stage3_Controller._Stage3_Quest[1] && !Stage3_Controller._Stage3_Quest[2]) {
			Q3_pick_them_up ();
		}

	}

	void Q3_pick_them_up(){
		if (_air.used_or_not_for_retry) { //떨굴 때
			print(_ic._now_used_item);

			GameObject aa = (GameObject)Instantiate(Resources.Load("Prefabs/Hokyun/" + _ic._now_used_item));
			aa.GetComponent<Toys> ()._pickable = true;
			aa.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aa.transform.position = new Vector3 (aa.transform.position.x, aa.transform.position.y, 6.5f);
			if (aa.GetComponent<Toys> ()._this_num == 11) {
				Stage3_Controller._Stage3_Quest[3] = false;
				Save_Script.Save_Quest_Info ();
			} else if (aa.GetComponent<Toys> ()._this_num == 12) {
				Stage3_Controller._Stage3_Quest[4] = false;
				Save_Script.Save_Quest_Info ();
			} else {
				Stage3_Controller._Stage3_Quest[5]	 = false;
				Save_Script.Save_Quest_Info ();
			}
			_air.used_or_not_for_retry = false;
		}
	}


}
