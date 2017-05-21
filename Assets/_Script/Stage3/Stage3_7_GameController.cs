using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage3_7_GameController : MonoBehaviour {

	public AudioSource leftSound;
	public GameObject portal1, portal2;
	public GameObject startPos;
	public GameObject earphone_message;
	private GameObject Item_Canvas;
	private Text_Importer ti;
	private bool active;

	void Start () {
		active = true;
		leftSound = GetComponent<AudioSource> ();
		GameObject.FindWithTag ("Player").transform.position = startPos.transform.position;
		Item_Canvas = GameObject.Find ("Item_Canvas");
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();

		if (GameObject.Find ("quest12_likelist(Clone)") != null) {
			Destroy (GameObject.Find ("quest12_likelist(Clone)"));
		}
			
		if (!Stage3_Controller._Stage3_Quest[20]) {
			earphone_message = Instantiate (earphone_message, Vector3.zero, Quaternion.identity) as GameObject;
			earphone_message.transform.SetParent (Item_Canvas.transform, false);
		}

		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		Debug.Log ("sceneIndex is " + Stage3_Controller.sceneIndex);
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
		ti.currLineArr [0] = 8; //코코 대사 
		ti.NPC_Say_yeah ("코코");
		yield return new WaitForSeconds(2);
		portal1.GetComponent<BoxCollider2D> ().enabled = true;
		portal2.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
