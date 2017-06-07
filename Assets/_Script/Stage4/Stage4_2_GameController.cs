using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_GameController : MonoBehaviour {

	public BoxCollider2D Star;

	private Transform start_pos;
	private GameObject player;
	private Text_Importer ti;
	private Item_Controller ic;
	private GameObject camera;

	private bool a1a1 = false;
	private bool a1a2 = false;
	private bool a1a3 = false;

	private bool a0a0 = false;
	private bool a0a1 = false;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
		//ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		//s2c = GameObject.Find ("Stage2_Controller").GetComponent<Stage2_Controller> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
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
		}
	}

	void Update(){
		if (Stage4_Controller.q[5] && !Stage4_Controller.q[6]) {
			Q6_GetaDoll ();
		}
		if (Stage4_Controller.q [11] && !Stage4_Controller.q [12]) {
			Q12_CheckRoom2 ();
		}
	}

	void Q6_GetaDoll(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				Stage4_Controller.q[6] = true;
				Save_Script.Save_Quest_Info ();
				break;
			}
		}
	}

	void Q12_CheckRoom2(){
		StartCoroutine ("CameraMove");
		Stage4_Controller.q [12] = true;
	}

	IEnumerator CameraMove(){
		camera = GameObject.FindWithTag ("MainCamera");
		camera.GetComponent<CameraManager> ().enabled = false;
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;

		yield return new WaitForSeconds(1);
		for (float f = 0f; f < 2f; f += Time.deltaTime) {
			//camera.transform.Translate (Vector3.left * Time.deltaTime);
			camera.transform.Translate (new Vector3(-camera.transform.position.x,0,0) * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds(1.5f);
		camera.GetComponent<CameraManager> ().enabled = true;
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
	}
}
