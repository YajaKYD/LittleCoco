using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4_6_GameController : MonoBehaviour {

	public Transform fraudPanel;
	private bool q22_0, q22_1;
	public int[] number = new int[2];
	public int scorePlayer, scoreNeogulman;
	//public GameObject figure, neogulmanPrefab, starPrefab;
	public Sprite neogulman, star;
	public bool clickable;
	public int roundNo = 0;
	private Text roundBoard;

	public GameObject[] slot;
	private GameObject tempSlot;
	public GameObject[] buttons;
	private Vector3[] velocity;
	public float smoothTime = 0.3f;
	public static bool getTempTransform;
	private Vector3[] tempPos;
	public bool mixDone;
	public int mixTimes;
	private GameObject itemCanvas;

	void Start () {
		itemCanvas = GameObject.FindWithTag ("Item_Canvas");
		Stage4_Controller.q [20] = true;
		roundBoard = GameObject.Find ("Round").GetComponent<Text> ();
		returnRandom ();

		tempPos = new Vector3[2];
		velocity = new Vector3[2];
		for (int i = 0; i < velocity.Length; i++) {
			velocity [i] = Vector3.zero;
		}
		getTempTransform = false;
	}

	void Update () {
		if (Stage4_Controller.q [20] && !Stage4_Controller.q [21]) {
			Q21_popupGame ();
		} else if (Stage4_Controller.q [21] && !Stage4_Controller.q [22]) {
			Q22_startGame ();
		} else if (Stage4_Controller.q [22]) {
			Debug.Log ("q22 complete");
		}
	}

	void Q21_popupGame(){
		//conversation
		itemCanvas.SetActive(false);
		ShowMenu(fraudPanel); // Pop up Game UI
		Stage4_Controller.q [21] = true;
	}

	void Q22_startGame(){
		switch(roundNo){
		case 1:
			Mix (20);
			break;

		case 2:
			PlayerMix ();
			break;

		case 3:
			Mix (20);
			break;

		default:
			break;
		}
	}

	void Mix(int times){
		if (q22_0) {
			if (mixDone) {
				returnRandom ();
			} else if (!mixDone) {
				Mix (number [0], number [1]);
				if (mixTimes >= times) { // Mix Done Completely
					clickable = true;
					q22_0 = false;
				}
			}
		}
	}

	void Mix(int a, int b){
		//button deactive
		if (!getTempTransform) {
			tempPos [0] = slot [a].transform.position;
			tempPos [1] = slot [b].transform.position;
			getTempTransform = true;
		}
		slot [a].transform.position = Vector3.SmoothDamp (slot[a].transform.position, tempPos[1], ref velocity[0], smoothTime);
		slot [b].transform.position = Vector3.SmoothDamp (slot[b].transform.position, tempPos[0], ref velocity[1], smoothTime);

		if (Vector3.Distance (slot [a].transform.position, tempPos [1]) <= 0.1f && Vector3.Distance(slot[b].transform.position, tempPos[0]) <= 0.1f) {
			//button active
			mixTimes++;
			mixDone = true;	
			getTempTransform = false;
			Debug.Log (mixDone + ", " + mixTimes);
			tempSlot = slot [a];
			slot [a] = slot [b];
			slot [b] = tempSlot;
		}
	}


	void PlayerMix(){
		if (!q22_1) {
			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].SetActive (true);
			}
			q22_1 = true;
			mixDone = true;
		} else {
			if (!mixDone) {
				Mix (number [0], number [1]);
			}
		}
	}
		
	public void MixButton(int a){
		switch (a) {
		case 0:
			number [0] = 0;
			number [1] = 1;
			break;
		case 1:
			number [0] = 0;
			number [1] = 2;
			break;
		case 2:
			number [0] = 1;
			number [1] = 2;
			break;
		default:
			number [0] = 0;
			number [1] = 0;
			break;
		}
		mixDone = false;
		mixTimes++;
		StartCoroutine ("ActivateButtons"); // might be changed
	}

	IEnumerator ActivateButtons(){
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].GetComponent<Button> ().enabled = false;
		}
		yield return new WaitForSeconds (0.5f); // time scale?
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].GetComponent<Button> ().enabled = true;
		}
	}

	public void tempComplete(){
		Reset (3);
		switch (roundNo) {
		case 1:
			CopyOneself(neogulman);
			break;
		case 2:
			CopyOneself (star);
			break;
		case 3: 
			CopyOneself(neogulman);
			break;
		default:
			break;
		}
		q22_0 = true;
	}

	public void Reset(int _roundNo){
		for (int i = 0; i < slot.Length; i++) {
			slot [i].GetComponent<Stage4_6_Fraud> ().neogulmanIn = false;
		}
		mixTimes = 0;
		clickable = false;
		roundNo++;
		roundBoard.text = roundNo + " Round";
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetActive (false);
		}
		if (roundNo > _roundNo) {
			Stage4_Controller.q [22] = true;
		}
	}

	void CopyOneself(Sprite figure){
		slot [1].GetComponent<Image> ().sprite = figure;
		slot [1].GetComponent<Stage4_6_Fraud> ().neogulmanIn = true;
		//conversation
		//appear effect
		slot[0].GetComponent<Image> ().sprite = figure;
		slot[2].GetComponent<Image> ().sprite = figure;
	}

	public void ShowAnswer(){
		for (int i = 0; i < slot.Length; i++) {
			if (!slot [i].GetComponent<Stage4_6_Fraud> ().neogulmanIn) {
				slot [i].GetComponent<Image> ().sprite = null;
				//disappear effect
			}
		}
	}

	int[] returnRandom(){
		number [0] = Random.Range (0, 3);
		number [1] = Random.Range (0, 3);
		while (number [0] == number [1]) {
			number [1] = Random.Range (0, 3);
		}
		mixDone = false;
		return number;
	}

	void ShowMenu(Transform menu)
	{
		menu.GetComponent<Animator>().SetBool("show", true);
	}
		


}
