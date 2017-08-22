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
	public SpriteRenderer ivon;
	public Sprite withcoco;

	private Text_Importer2 ti;


	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer>();

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (24);
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag("Player")){
			player.transform.position = this.transform.position;
			player.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			//GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1);
			ivon.sprite = withcoco;
			mbr.enabled = false;
			//sound 들어가자.
//			aa.currLineArr [1] += 2;
//			aa.NPC_Say_yeah ("별감");
//			_star_textbox = GameObject.Find ("별감_text");
			ti.Talk(13);
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
			//Destroy (GameObject.FindWithTag ("Dialogue"));
			Selecting_stage._what_stage_now_cleared = 2;
			PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
			portaltoend.transform.position = player.transform.position;
			portaltoend.GetComponent<BoxCollider2D> ().enabled = true;
			test_end = true;
		}
		//
		if (!a1a4 && Stage2_Controller.q[33]) {
			a1a4 = true;
			Destroy (GameObject.FindWithTag ("Controller"));
			//Destroy (GameObject.FindWithTag ("Dialogue"));
			Selecting_stage._what_stage_now_cleared = 2;
			PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
			portaltoend.transform.position = player.transform.position;
			portaltoend.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}
}
