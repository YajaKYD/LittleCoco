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

	public GameObject poster, posterPrefab;

	public bool movePlayer;
	public Transform startPos;
	private GameObject item_Canvas;
	public Vector2 playerPos;
	public Vector2 pos;
	public float speed;
	public float rotationSpeed;
	public SpriteRenderer blackout;

	public bool[] conv;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		player.transform.position = startPos.position;
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		conv = new bool[10];

		//ti.Import(25); // test
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
		if (conv [0] && !textbox_Coco.activeSelf) {
			ti.NPC_Say_yeah ("별감");
			conv [0] = false;
			conv [1] = true;
		} else if (conv [1] && !textbox_Star.activeSelf) {
			poster = Instantiate (posterPrefab, Vector3.forward, Quaternion.identity) as GameObject;
			conv [1] = false;
			conv [2] = true;
		} else if (conv [2]) {
			ti.NPC_Say_yeah ("코코");
			conv [2] = false;
			conv [3] = true;
		} else if (conv [3] && !textbox_Coco.activeSelf) {
			ti.NPC_Say_yeah ("별감");
			conv [3] = false;
			conv [4] = true;
		} else if (conv [4] && !textbox_Star.activeSelf) {
			ti.NPC_Say_yeah ("코코");
			conv [4] = false;
			conv [5] = true;
		} 
	}

	public void TouchPoster(){
		ti.NPC_Say_yeah ("코코");
		conv[0] = true;
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
