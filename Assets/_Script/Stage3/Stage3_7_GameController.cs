﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class Stage3_7_GameController : MonoBehaviour {

//	[MenuItem("MyMenu/for test")]
//	static void DoSomething()
//	{
//		Debug.Log("Doing Something...");
//		GameObject.Find ("Player").SetActive (false);
//		GameObject.Find ("Dialogue_Canvas_").SetActive (false);
//		GameObject.Find ("Item_Canvas").SetActive (false);
//		GameObject.Find ("Stage3_Controller").SetActive (false);
//	}

	public AudioSource leftSound;
	public GameObject portal1, portal2;
	public GameObject startPos;
	public GameObject earphone_message;
	private GameObject Item_Canvas;
	private Text_Importer ti;
	private bool active;
	private bool lookingforIvon;

	void Start () {
		active = true;
		leftSound = GetComponent<AudioSource> ();
		GameObject.FindWithTag ("Player").transform.position = startPos.transform.position;
		Item_Canvas = GameObject.Find ("Item_Canvas");
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
		//ti.Import (15);

		GameObject _park = GameObject.FindWithTag("Controller").transform.GetChild (1).gameObject; // stop bgm
		_park.SetActive(false);


		if (GameObject.Find ("quest12_likelist(Clone)") != null) {
			Destroy (GameObject.Find ("quest12_likelist(Clone)"));
		}
			
		GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (0.6f, 0.6f, 1f);

		if (!Stage3_Controller._Stage3_Quest[20]) {
			Save_Script.Save_Now_Point();
			earphone_message = Instantiate (earphone_message, Vector3.zero, Quaternion.identity) as GameObject;
			earphone_message.transform.SetParent (Item_Canvas.transform, false);
		}
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + "sceneIndex is " + Stage3_Controller.sceneIndex);
		if (Stage3_Controller.sceneIndex >= SceneManager.GetActiveScene ().buildIndex) {
			lookingforIvon = true;
		}
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + "sceneIndex is " + Stage3_Controller.sceneIndex);
	}

	void Update () {
		if (Stage3_Controller._Stage3_Quest[20] && active) {
			StartCoroutine("WaitAndSound");
			active = false;
		}	
	}

	IEnumerator WaitAndSound(){
		yield return new WaitForSeconds(2);
		leftSound.Play ();
		Debug.Log ("left sound");
		yield return new WaitForSeconds(1);
		if (!lookingforIvon) {
			ti.currLineArr [0] = 8; //코코 대사 
			ti.NPC_Say_yeah ("코코");
		}
		yield return new WaitForSeconds(2);
		portal1.GetComponent<BoxCollider2D> ().enabled = true;
		portal2.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
