using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_GameController2 : MonoBehaviour {

	public BoxCollider2D Star;

	private Transform start_pos;
	public GameObject player, neogul;
	private Text_Importer2 ti;
	private Item_Controller ic;
	private GameObject itemCanvas;
	private GameObject camera;

	public bool q13_meetNeogul;
	public bool q13_sayonce;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		itemCanvas = GameObject.FindWithTag ("Item_Canvas");
		ic = itemCanvas.GetComponent<Item_Controller> ();
	}

	void Start () {
		if (Stage4_Controller.q[6]) {
			Star.enabled = true;
		} 
		if (Stage4_Controller.q[7]) {
			Destroy (Star.gameObject);
		} 
	}

	void Update () {
		if (Stage4_Controller.q [6] && !Stage4_Controller.q [7]) {
			Q7_GetaDoll ();
		}
	}

	void Q7_GetaDoll(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				Stage4_Controller.q[7] = true;
				break;
			}
		}
	}

}
