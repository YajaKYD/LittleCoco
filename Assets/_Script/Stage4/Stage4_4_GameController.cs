using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_4_GameController : MonoBehaviour {

	private GameObject player;
	private Text_Importer ti;

	private GameObject textbox_Ivon;
	private GameObject textbox_Coco;
	private GameObject textbox_Star;
	private GameObject textbox_Racoon;

	public Stage4_4_poster poster;

	public bool movePlayer;
	public Transform startPos;
	private GameObject item_Canvas;
	public Vector2 playerPos;
	public Vector2 pos;
	public float speed;
	public float rotationSpeed;
	public SpriteRenderer blackout;


	public bool q16_0, q16_1, q16_2;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		player.transform.position = startPos.position;
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();

		ti.Import(25); // test
		textbox_Coco = ti._text_boxes [0];
		textbox_Star = ti._text_boxes [1];
		textbox_Racoon = ti._text_boxes [2];
		textbox_Ivon = ti._text_boxes [3];

		Save_Script.Save_Now_Point();
		Stage4_Controller.q [16] = false; // test
	}

	void Update(){
		
	}

	void FixedUpdate(){
		if (!Stage4_Controller.q [16]) {
			Q16_PosterPuzzle ();
		}
	}

	void Q16_PosterPuzzle(){
		if (q16_0) {
			ti.NPC_Say_yeah ("코코");
			q16_0 = false;
			q16_1 = true;
		} else if (q16_1 && !textbox_Coco.activeSelf) {
			poster.DisappearPoster ();
			ti.NPC_Say_yeah ("별감");
			q16_1 = false;
			q16_2 = true;
		} else if (q16_2 && !textbox_Star.activeSelf) {
			StartCoroutine ("FinishPosterPuzzle");
		}
	}

//	void Q16_PosterPuzzle(){
//		if (movePlayer) {
//			Debug.Log ("Fixed Update" + playerPos + ", " + pos + " difference is " + (playerPos-pos));
//			player.GetComponent<Rigidbody2D> ().AddForce ((pos-playerPos) * speed * Time.deltaTime);
//			player.transform.Rotate (Vector3.forward * rotationSpeed);
//
//			if (Vector3.Distance (player.transform.position, pos) <= 1) {
//				movePlayer = false;
//				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
//				player.GetComponent<Rigidbody2D> ().angularVelocity = 0;
//				GameObject.Find ("NewBackground(Clone)").SetActive (false);
//				StartCoroutine ("FinishPosterPuzzle");
//				Stage4_Controller.q [16] = true;
//			}
//		}
//	}

	IEnumerator FinishPosterPuzzle(){
		Debug.Log ("finish 4_4");
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}
}
