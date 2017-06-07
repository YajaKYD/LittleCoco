using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_1_GameController : MonoBehaviour {

	public GameObject Light;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Text_Importer ti;
	private Item_Controller ic;
	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;

	public SpriteRenderer _blackout;
	private Color bb;

	private bool q2_1 = false;
	private bool q2_2 = false;
	private bool q3_1 = false;

	private bool q2_0 = false;
	private bool q3_0 = false;

	//private GameObject camera;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		textbox_Coco = ti._text_boxes [0];
		textbox_Star = ti._text_boxes [1];
		textbox_Racoon = ti._text_boxes [2];
		textbox_Ivon = ti._text_boxes [3];

		if (!Stage4_Controller.q[1]) {
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}

		bb = new Color (0f, 0f, 0f, 1f); //검정,불투명
		_blackout.color = bb;

	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 26) {
			player.transform.position = regen_pos.position;
			Destroy(GameObject.FindWithTag ("NPC"));
		} else {
			ti.NPC_Say_yeah ("이본");
		}

		//for test
		//StartCoroutine ("CameraMove");
	}

	void Update(){
		if (!Stage4_Controller.q[1]) {
			Q1_SayGoodnight ();
		}

		if (Stage4_Controller.q[1] && !Stage4_Controller.q[2]) {
			Q2_CheckIvon ();
		}

		if (Stage4_Controller.q[3] && !Stage4_Controller.q[4]) {
			Q4_SayandReturn ();
		}

		if (Stage4_Controller.q[6] && !Stage4_Controller.q[7]) {
			Q7_PutDoll ();
		}

		if (Stage4_Controller.q[9] && !Stage4_Controller.q[10]) {
			Q10_Neogulman ();
		}

		if (Stage4_Controller.q[10] && !Stage4_Controller.q[11]) {
			Q11_CheckRoom ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller.q[2] && !Stage4_Controller.q[3]) {
			Stage4_Controller.q[3] = true; // check if ivon sleep
		}

		if (other.CompareTag ("Player") && Stage4_Controller.q[7] && !Stage4_Controller.q[8]) {
			Stage4_Controller.q[8] = true;
			ti.currLineArr [0] += 2; 
			ti.NPC_Say_yeah ("코코"); // ivon fell asleep
			print ("Check Again");
		}
	}

	void Q1_SayGoodnight(){
		if (!textbox_Ivon.activeSelf) {
			Destroy(GameObject.FindWithTag ("NPC"));
			Stage4_Controller.q[1] = true;
		}
	}

	void Q2_CheckIvon(){
		if (!q2_0) {
			ti.NPC_Say_yeah ("별감"); // 
			q2_0 = true;
		}
		if (q2_0 && !textbox_Star.activeSelf && !q2_1) {
			ti.NPC_Say_yeah ("코코"); // ivon
			q2_1 = true;
		}
		if (q2_1 && !textbox_Coco.activeSelf && !q2_2) {
			ti.currLineArr [1] += 2; // next line
			ti.NPC_Say_yeah ("별감");
			q2_2 = true;
		}
		if (q2_2 && !textbox_Star.activeSelf) { // end of talking
			Stage4_Controller.q[2] = true;
		}
	}

	void Q4_SayandReturn(){
		if (!q3_0) {
			ti.currLineArr [0] += 2; 
			ti.NPC_Say_yeah ("코코"); // music
			q3_0 = true;
		}
		if (q3_0 && !textbox_Coco.activeSelf && !q3_1) {
			ti.currLineArr [1] += 2;
			ti.NPC_Say_yeah ("별감"); // ivon listening music
			q3_1 = true;
			Stage4_Controller.q[4] = true;
		}
	}

	void Q7_PutDoll(){
		if (ic._now_used_item == "StarDoll") {
			Stage4_Controller.q[7] = true;
			Light.SetActive (false);
			print ("Change Image with doll and bed");
		}
	}

	void Q10_Neogulman(){
		StartCoroutine ("FadeOutandIn");

		Stage4_Controller.q [10] = true;
	}

	void Q11_CheckRoom(){
		if (player.transform.position.x <= -16) {
			ti.NPC_Say_yeah ("코코"); // !!!!
			// add sound effect 
			Stage4_Controller.q [11] = true;	
		}
	}

	IEnumerator FadeOutandIn(){

		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}

		yield return new WaitForSeconds(2);

		Light.SetActive (true);
		Light.transform.position = new Vector3 (-20, 0, 0);
		Light.transform.localScale = new Vector3 (1, 1, 1);


		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			bb.a = f;
			_blackout.color = bb;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;

		ti.currLineArr [0] += 4; 
		ti.NPC_Say_yeah ("코코"); // !!!!
	}
//	//for test
//	IEnumerator CameraMove(){
//		camera = GameObject.FindWithTag ("MainCamera");
//		camera.GetComponent<CameraManager> ().enabled = false;
//
//		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
//		for (float f = 0f; f < 1f; f += Time.deltaTime) {
//			camera.transform.Translate (new Vector3(-camera.transform.position.x,0,0) * Time.deltaTime);
//			yield return null;
//		}
//
//		yield return new WaitForSeconds(1);
//		camera.GetComponent<CameraManager> ().enabled = true;
//		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
//	}
}
