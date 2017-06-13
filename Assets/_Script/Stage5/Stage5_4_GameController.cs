using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_4_GameController : MonoBehaviour {

	public Transform from_5_5;
	public Transform from_5_6;
	public Transform from_5_7;
	public Transform from_5_8;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;
	private Item_Controller ic;


	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		//player.transform.position = start_pos.position;
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 37) {
			player.transform.position = from_5_5.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 38) {
			player.transform.position = from_5_6.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 39) {
			player.transform.position = from_5_7.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 40) {
			player.transform.position = from_5_8.position;
		}

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];

	}
}
