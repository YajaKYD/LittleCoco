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

	void Start () {
		player = GameObject.FindWithTag ("Player");
		//test
		Stage4_Controller.q[14] = true;
		//Save_Script.Save_Now_Point();
	}

	void Update () {
		if (Stage4_Controller.q [14] && !Stage4_Controller.q [15]) {
			Q15_gumPuzzle ();
		} 
	}

	void Q15_gumPuzzle(){
		if (Stage4_Controller.q15 [0] && Stage4_Controller.q15 [1]) {
			StartCoroutine("FinishGumPuzzle");
			Stage4_Controller.q [15] = true;
		}
	}

	IEnumerator FinishGumPuzzle(){
		Debug.Log ("finish 4_3");
		background.GetComponent<BoxCollider2D> ().enabled = false; // player going down
		yield return new WaitForSeconds(1);
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}
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
