using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_2_GameController2 : Controller {

	public BoxCollider2D Star;

	private Transform start_pos;
	public GameObject player, racoon;
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
		sceneNo = 42;
	}

	void Start () {
		ti.Import (sceneNo);

		if (Stage4_Controller.q[6]) {
			Star.enabled = true;
		} 
		if (Stage4_Controller.q[7]) {
			Destroy (Star.gameObject);
		} 
		if (Stage4_Controller.q [14]) {
			racoon.SetActive (true);
		} else {
			racoon.SetActive (false);
		}
	}

	void Update () {
		if (Stage4_Controller.q [6] && !Stage4_Controller.q [7]) {
			Q7_GetaDoll ();
		} else if (Stage4_Controller.q [15] && !Stage4_Controller.q [16]) {
			Q16_CheckRoom ();
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

	void Q16_CheckRoom(){
		StartCoroutine ("CameraMove");
		Stage4_Controller.q [16] = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage4_Controller.q [9] && !Stage4_Controller.q [10]) {
			ti.Talk ();
		}
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
