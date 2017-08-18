using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_3_GameController2 : MonoBehaviour {

	public bool[] posAvailable;
	public GameObject[] puzzlePiece;
	public Transform[] emptyPos;
	private GameObject player;
	public int nextSceneNo;
	public SpriteRenderer blackout;
	public GameObject background;
	public Transform startPos;
	private GameObject item_Canvas;
	private Text_Importer2 ti;

	public float shakeTimer, shakeAmount;

	private bool q20_0, q20_1;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		player.transform.position = startPos.position;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		item_Canvas = GameObject.FindWithTag ("Item_Canvas");
		Save_Script.Save_Now_Point();

		ti.Import(43); 

	}

	void Update () {
		//19 true 20 false
		if (!Stage4_Controller.q [20]) {
			Q20_gumPuzzle ();
		}
	}

	void Q20_gumPuzzle(){
		if (Stage4_Controller.q20 [0] && Stage4_Controller.q20 [1] && !Stage4_Controller.q[21]) {
			Stage4_Controller.q [21] = true;
			shakeAmount = 0.1f;
			shakeTimer = 1f;
			StartCoroutine ("FinishGumPuzzle1");
		} else if (Stage4_Controller.q20 [0] && Stage4_Controller.q20 [1] && Stage4_Controller.q[21]) {
			if (Stage4_Controller.q [22]) {
				StartCoroutine ("FinishGumPuzzle2");
				Stage4_Controller.q [20] = true;
			}
		}
	}

	IEnumerator FinishGumPuzzle1(){
		Debug.Log ("finish 1");
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x + ShakePos.x, Camera.main.transform.position.y + ShakePos.y, Camera.main.transform.position.z); 
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);
		ti.Talk (); //Stage4_Controller.q [21] = true;
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
