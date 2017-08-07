using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_3_GameController : MonoBehaviour {

	public bool[] posAvailable;
	public GameObject[] puzzlePiece;
	public Transform[] emptyPos;
	private GameObject player;
	public int nextSceneNo;
	public SpriteRenderer blackout;
	public GameObject background;
	public Transform startPos;
	private GameObject item_Canvas;
	private Text_Importer ti;
	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;

	public float shakeTimer, shakeAmount;

	private bool q15_0, q15_1;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		player.transform.position = startPos.position;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");

		Stage4_Controller.q[14] = true; //test // need optimization
		//ti.Import(25); // test

		textbox_Coco = ti._text_boxes [0];
		textbox_Star = ti._text_boxes [1];
		textbox_Racoon = ti._text_boxes [2];
		textbox_Ivon = ti._text_boxes [3];

		Save_Script.Save_Now_Point();
	}

	void Update () {
		if (Stage4_Controller.q [14] && !Stage4_Controller.q [15]) {
			Q15_gumPuzzle ();
		} 
	}

	void Q15_gumPuzzle(){
		if (Stage4_Controller.q15 [0] && Stage4_Controller.q15 [1] && !q15_0) {
			q15_0 = true;
			q15_1 = true;
			shakeAmount = 0.1f;
			shakeTimer = 1f;
			StartCoroutine ("FinishGumPuzzle1");
		} else if (Stage4_Controller.q15 [0] && Stage4_Controller.q15 [1] && q15_0 && !q15_1) {
			if (!textbox_Coco.activeSelf) {
				q15_1 = true;
				StartCoroutine ("FinishGumPuzzle2");
				Stage4_Controller.q [15] = true;
			}
		}
	}

	IEnumerator FinishGumPuzzle1(){
		Debug.Log ("finish 1");
		//animation
//		if (shakeTimer >= 0) {
//			Debug.Log ("shake camera");
//			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
//			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + ShakePos.x, Camera.main.transform.position.y + ShakePos.y, Camera.main.transform.position.z); 
//			shakeTimer -= Time.deltaTime;
//			yield return null;
//		}
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + ShakePos.x, Camera.main.transform.position.y + ShakePos.y, Camera.main.transform.position.z); 
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);
		ti.NPC_Say_yeah ("코코");
		q15_1 = false;
	}

	IEnumerator FinishGumPuzzle2(){
		Debug.Log ("finish 2");
		background.GetComponent<BoxCollider2D> ().enabled = false; // player going down
		yield return new WaitForSeconds(1);
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}
		item_Canvas.SetActive (true);
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (nextSceneNo);
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}

	public void JudgePosAvailable(){
		for (int i = 0; i < emptyPos.Length; i++) {
			posAvailable [i] = true;
			for (int j = 0; j < puzzlePiece.Length; j++) {
				if (puzzlePiece [j].transform.position == emptyPos [i].position) {
					posAvailable [i] = false;
				}
			}
		}
	}



}
