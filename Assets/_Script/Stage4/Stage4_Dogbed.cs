using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_Dogbed : MonoBehaviour{
	public GameObject player;
	public int _To_Scene;
	public GameObject light;

	public bool enter_;
	public bool exit_ = false;

	private Text_Importer2 ti;

	public SpriteRenderer blackout;
	private Color bb;

	public GameObject portaltoleft;

	void Awake(){
		player = GameObject.Find ("Player");
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
	}

	void OnMouseDown(){
//
//		if (Stage4_Controller.q[4] && !Stage4_Controller.q4[0]) { //땅파기
//			print("Dig");
//			Stage4_Controller.q4 [0] = true;
//			return;
//		}
//
//		if (Stage4_Controller.q4[0] && !Stage4_Controller.q4[1]) {
//			print ("smell");
//			Stage4_Controller.q4 [1] = true;
//			Stage4_Controller.q [5] = true;
//		}
//
//		if (Stage4_Controller.q[8] && !Stage4_Controller.q[9]) {
//			print ("Sleep");
//			Stage4_Controller.q[9] = true;
//		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Player")){
			if (Stage4_Controller.q [5] && !Stage4_Controller.q [6]) {
				ti.Talk (11);
				portaltoleft.SetActive (true);
			} else if (Stage4_Controller.q [8] && !Stage4_Controller.q [9]) {
				Debug.Log ("coco animation digging " + ti.lineNo);
				ti.Talk (18);
			} else if (Stage4_Controller.q [13] && !Stage4_Controller.q [14]) {
				Q14_SleepAndWake ();
			}
		} 
	}

	void Q14_SleepAndWake(){
		StartCoroutine ("FadeOutandIn");
		Stage4_Controller.q [14] = true;
	}

	IEnumerator FadeOutandIn(){

		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}

		yield return new WaitForSeconds(2);

		light.SetActive (true);
		light.transform.position = new Vector3 (-20, 0, 0);
		light.transform.localScale = new Vector3 (1, 1, 1);

		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			bb.a = f;
			blackout.color = bb;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		player.transform.rotation = Quaternion.Euler (0, 180, 0);

		ti.Talk (27);
		// add sound effect 
	}



}
