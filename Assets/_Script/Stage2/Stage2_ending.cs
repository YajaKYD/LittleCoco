using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_ending : MonoBehaviour {

	private GameObject player;
	private Moving_by_RLbuttons mbr;

	Text_Importer aa;
	bool a1a1 = false;
	bool a1a2 = false;
	bool a1a3 = false;
	bool a1a4 = false;
	bool a1a5 = false;

	bool test_end = false;

	public GameObject _coco_textbox;
	public GameObject _star_textbox;
	public GameObject portaltoend;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer>();
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			player.transform.position = this.transform.position;
			player.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			//GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1);
			mbr.enabled = false;
			//sound 들어가자.
			aa.currLineArr [1] += 2;
			aa.NPC_Say_yeah ("별감");
			_star_textbox = GameObject.Find ("별감_text");
			a1a1 = true; //ending 시작
		}
	}

	void OnDisable(){
		player.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
		mbr.enabled = true;
	}

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W) && !test_end) {
			print ("goto_select");
			Destroy (GameObject.FindWithTag ("Controller"));
			Destroy (GameObject.FindWithTag ("Dialogue"));
			Selecting_stage._what_stage_now_cleared = 2;
			PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
			portaltoend.transform.position = player.transform.position;
			portaltoend.GetComponent<BoxCollider2D> ().enabled = true;
			test_end = true;
		}
		//

		if (a1a1 && !_star_textbox.activeSelf) { //별감 1번대사 이후
			aa.currLineArr [0] = 18;
			aa.NPC_Say_yeah ("코코");
			_coco_textbox = GameObject.Find ("코코_text");
			a1a1 = false;
			a1a2 = true;
		}
		if (a1a2 && !_coco_textbox.activeSelf) {
			aa.currLineArr [1] += 2;
			aa.NPC_Say_yeah ("별감");
			a1a2 = false;
			a1a3 = true;
		}
		if (a1a3 && !_star_textbox.activeSelf) { //별감 2번대사 이후
			aa.currLineArr [0] += 2;
			aa.NPC_Say_yeah ("코코");
			a1a3 = false;
			a1a4 = true;
		}
//		if (a1a4 && !_coco_textbox.activeSelf) {
//			aa.currLineArr [0] += 2;
//			aa.NPC_Say_yeah ("코코");
//			a1a4 = false;
//			a1a5 = true;
//		}
		if (a1a4 && !_coco_textbox.activeSelf) {
			a1a4 = false;
			Destroy (GameObject.FindWithTag ("Controller"));
			Destroy (GameObject.FindWithTag ("Dialogue"));
			Selecting_stage._what_stage_now_cleared = 2;
			PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
			portaltoend.transform.position = player.transform.position;
			portaltoend.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}
}
