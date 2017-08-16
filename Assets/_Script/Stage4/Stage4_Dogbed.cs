using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_Dogbed : MonoBehaviour{
	public GameObject player;
	public int _To_Scene;

	public bool enter_;
	public bool exit_ = false;

	private Text_Importer2 ti;

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
		if (other.gameObject.CompareTag("Player") && Stage4_Controller.q [5] && !Stage4_Controller.q [6]) {
			ti.Talk (ti.lineNo + 2);
		}
	}



}
