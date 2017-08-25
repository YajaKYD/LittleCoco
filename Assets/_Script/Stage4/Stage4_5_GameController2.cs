using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; 

public class Stage4_5_GameController2 : MonoBehaviour {

	public GameController controller;
	public Item_Controller ic;
	private GameObject player;
	public GameObject card;
	public Transform startPos;
	private bool q19_0, q19_1;

	private Text_Importer2 ti;
	public SpriteRenderer blackout;
	public GameObject blackout2;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		controller = GameObject.Find ("PuzzleController").GetComponent<GameController> ();
	}

	void Start () {
		player.transform.Translate (startPos.position);
		ti.Import (45);
		ti.Talk ();
	}

	void Update () {
		if (Stage4_Controller.q [31] && !Stage4_Controller.q [32]) {
			Q32_getCard ();
		} else if (Stage4_Controller.q [32] && !Stage4_Controller.q [33]) {
			Q33_cardPuzzle ();
		} else if (Stage4_Controller.q [33] && !Stage4_Controller.q [34]) {
			Q34_completeScene ();
		}
	}

	void Q32_getCard(){
		if (!Stage4_Controller.q32 [0]) {
			card.SetActive (true);
			//conversation
			//if get card -> q18[0] = true;
		} else if (!Stage4_Controller.q32 [1]) {
			//conversation
			//if coco get card -? q18[1] = true;
			if (ic._now_used_item == "Card") {
				Stage4_Controller.q32 [1] = true;
			}
		} else if (Stage4_Controller.q32 [0] && Stage4_Controller.q32 [1]) {
			Stage4_Controller.q [32] = true;
		}
	}
		
	void Q33_cardPuzzle(){
		if (!q19_1) {
			controller.StartGame (0);
			//ti.Talk (ti.lineNo + 2); //conversation
			q19_1 = true;

		}
		// if puzzle solved, q[33] = true;
	}
		
	public void tempComplete(){
		Stage4_Controller.q [33] = true;
	}

	void Q34_completeScene(){
		controller.gamePanel.Find("Coco").gameObject.SetActive(false);	//need animation
		StartCoroutine ("CompleteScene");
		Stage4_Controller.q [34] = true;
	}

	IEnumerator CompleteScene(){
		yield return new WaitForSeconds (1);
		Debug.Log ("finish 4_5");
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		blackout2.SetActive(true);
		Color c = blackout2.GetComponent<Image> ().color;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			c.a = f;
			Debug.Log (f);
			blackout2.GetComponent<Image> ().color = c;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}

}
