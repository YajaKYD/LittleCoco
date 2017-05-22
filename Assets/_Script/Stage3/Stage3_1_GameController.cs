using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_1_GameController : MonoBehaviour {

	private bool a1;
	private bool a2;
	private GameObject _ivon_textbox;
	private GameObject player;
	private Transform start_pos;
	private Transform regen_pos;

    public GameObject bag;
	//개발 후 삭제//
	public GameObject[] aaaaaa;

	void Awake(){

		//개발 후 삭제//
		if (Stage3_Controller._Stage3_Quest[0]) {
			Destroy (aaaaaa[0]);
			Destroy (aaaaaa[1]);
			Destroy (aaaaaa[2]);
		}
		//개발 후 삭제//

		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;


//		if (!Stage3_Controller._Stage3_Quest[0]) {
//			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.Import ();
//			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
//		}

		if (Stage3_Controller._Stage3_Quest[7])
        {
            Destroy(bag);
        }

	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 16) {
			Debug.Log ("from 16");
			player.transform.position = regen_pos.position;
		} else {
			player.transform.position = start_pos.position;
		}
		if (Stage3_Controller._Stage3_Quest[2])
        {
            bag.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

	void Update(){
		if (!Stage3_Controller._Stage3_Quest[0]) {
			Q1_StartTalk ();
		}


    }

	void Q1_StartTalk(){
		if (!a1) {
			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr[1] = 0;
			aa.NPC_Say_yeah ("이본");
			_ivon_textbox = GameObject.Find ("이본_text");
			a1 = true;
		}

		if (!a2 && a1 && !_ivon_textbox.activeSelf) {
			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr[0] = 0;
			aa.NPC_Say_yeah ("코코");
			Stage3_Controller._Stage3_Quest[0] = true;
			Save_Script.Save_Quest_Info ();
		}
	}
}
