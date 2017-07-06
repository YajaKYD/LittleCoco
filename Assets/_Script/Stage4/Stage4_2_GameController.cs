using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_GameController : MonoBehaviour {

	public BoxCollider2D Star;

	private Transform start_pos;
	public GameObject player, neogul;
	private Text_Importer ti;
	private Item_Controller ic;
	private GameObject itemCanvas;
	private GameObject camera;
	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;

	public bool q13_meetNeogul;
	public bool q13_sayonce;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		itemCanvas = GameObject.FindWithTag ("Item_Canvas");
		ic = itemCanvas.GetComponent<Item_Controller> ();
		textbox_Coco = ti._text_boxes [0];
		textbox_Star = ti._text_boxes [1];
		textbox_Racoon = ti._text_boxes [2];
		textbox_Ivon = ti._text_boxes [3];
	}

	void Start(){

		if (Stage4_Controller.q[5]) {
			Star.enabled = true;
		} 
		if (Stage4_Controller.q[6]) {
			Destroy (Star.gameObject);
		} 
		if (Stage4_Controller.q [11]) {
			Debug.Log ("change background");
		} else {
			neogul.SetActive (false);
		}
	}

	void Update(){
		if (Stage4_Controller.q [5] && !Stage4_Controller.q [6]) {
			Q6_GetaDoll ();
		} else if (Stage4_Controller.q [11] && !Stage4_Controller.q [12]) {
			Q12_CheckRoom2 ();
		} else if (Stage4_Controller.q [12] && !Stage4_Controller.q [13] && q13_meetNeogul) {
			Q13_TalkToNeogul ();
		} 
//		else if (Stage4_Controller.q [13] && !Stage4_Controller.q [14]) {
//			Q14_StartChasing ();
//		}
	}

	void Q6_GetaDoll(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				Stage4_Controller.q[6] = true;
				break;
			}
		}
	}

	void Q12_CheckRoom2(){
		StartCoroutine ("CameraMove");
		Stage4_Controller.q [12] = true;
	}

	void Q13_TalkToNeogul(){
		if (!q13_sayonce) {
			//Debug.Log ("1 sayonce is " + sayonce);
			q13_sayonce = true;
			ti.NPC_Say_yeah ("너굴맨");
			//Debug.Log ("2 sayonce is " + sayonce);
		}

		if (!textbox_Racoon.activeSelf) {
			neogul.SetActive (false);
			Stage4_Controller.q [13] = true;	
		}
		// add conversation
	}

	void Q14_StartChasing(){
//		if (neogul.transform.position.x > -20) {
//			neogul.transform.Translate (Vector3.left * Time.deltaTime * 2);
//		} else {
			neogul.SetActive (false); // add animation
			Stage4_Controller.q [14] = true;
		//}
	}

	IEnumerator CameraMove(){
		camera = GameObject.FindWithTag ("MainCamera");
		camera.GetComponent<CameraManager> ().enabled = false;
		itemCanvas.SetActive (false);

		yield return new WaitForSeconds(1);

		for (float f = 0f; f < 2f; f += Time.deltaTime) {
			//camera.transform.Translate (Vector3.left * Time.deltaTime);
			camera.transform.Translate (new Vector3(-camera.transform.position.x,0,0) * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds(1.5f);
		camera.GetComponent<CameraManager> ().enabled = true;
		itemCanvas.SetActive (true);
	}
}
