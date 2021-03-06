﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4_6_GameController2 : MonoBehaviour {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private Text_Importer2 ti;
	private GameObject itemCanvas;
	public GameObject player;
	public Transform startPos;

	public Transform fraudPanel;
	private bool q22_0, q22_1;
	public int[] number = new int[2];
	public int scorePlayer, scoreNeogulman;
	public Sprite neogulman, star;
	public bool clickable;
	public int roundNo = 0;
	private Text roundBoard, scoreBoard, timeBoard;

	public GameObject[] slot;
	private GameObject tempSlot;
	public GameObject[] buttons;
	public GameObject restart;
	private Vector3[] velocity;
	public float smoothTime = 0.3f;
	public static bool getTempTransform;
	private Vector3[] tempPos;
	public GameObject[] fixedPos;
	public bool mixDone;
	private bool startCountSeconds;
	private float remainSeconds;
	public int mixTimes;
	private int tempScorePlayer;

	public GameObject message_answer;
	public GameObject systemMessage, actionMessage;

	// Use this for initialization
	void Start () {
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage4_6");

        player = GameObject.FindWithTag ("Player");
		itemCanvas = GameObject.FindWithTag ("Item_Canvas");
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		ti.Import (46); // temp code

		roundBoard = GameObject.Find ("Round").GetComponent<Text> ();
		scoreBoard = GameObject.Find ("Score").GetComponent<Text> ();
		timeBoard = GameObject.Find ("Time").GetComponent<Text> ();
		returnRandom ();

		player.transform.Translate (startPos.position);

		tempPos = new Vector3[2];
		//fixedPos = new Vector3[3];
		velocity = new Vector3[2];
		for (int i = 0; i < velocity.Length; i++) {
			velocity [i] = Vector3.zero;
		}
		getTempTransform = false;
		systemMessage.SetActive (false);
		actionMessage.SetActive (false);
		restart.SetActive (false);
		timeBoard.gameObject.SetActive (false);
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetActive (false);
		}

		//ti.Talk ();
		//Stage4_Controller.q[35] = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Stage4_Controller.q [35] && !Stage4_Controller.q [36]) {
			Q36_popupGame ();
		} else if (Stage4_Controller.q [36] && !Stage4_Controller.q [37]) {
			//Debug.Log ("q[37] is " + Stage4_Controller.q [37]);
			Q37_startGame ();
		} else if (Stage4_Controller.q [37] && !Stage4_Controller.q [38]) {
			Q38_finishGame ();
		}
	}

	void Q36_popupGame(){
			//conversation
			itemCanvas.SetActive (false);
			ShowMenu (fraudPanel); // Pop up Game UI
			Stage4_Controller.q [36] = true;
	}

	void Q37_startGame(){
		switch(roundNo){
		case 1:
			Mix (5); // change int variables
			break;

		case 2:
			PlayerMix ();
			break;

		case 3:
			Mix (5);
			break;

		default:
			break;
		}
	}

	void Q38_finishGame(){
		ti.Talk (ti.lineNo + 2);
		Stage4_Controller.q [38] = true;
	}

	void Mix(int times){
		if (q22_0) {
			if (mixDone) {
				returnRandom ();
			} else if (!mixDone) {
				Mix2 (number [0], number [1]);
				if (mixTimes >= times) { // Mix Done Completely
					clickable = true;
					q22_0 = false;
					actionMessage.SetActive (true);
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

	void Mix2(int a, int b){
		if (roundNo == 2) {
			for (int i = 0; i < buttons.Length; i++) {
				//buttons [i].GetComponent<Button> ().enabled = false;
				buttons [i].SetActive (false);
			}
		}

		//if (startCountSeconds) {
			slot [a].transform.position = Vector3.SmoothDamp (slot [a].transform.position, fixedPos [b].transform.position, ref velocity [0], smoothTime);
			slot [b].transform.position = Vector3.SmoothDamp (slot [b].transform.position, fixedPos [a].transform.position, ref velocity [1], smoothTime);

			if (Vector3.Distance (slot [a].transform.position, fixedPos [b].transform.position) <= 0.2f && Vector3.Distance (slot [b].transform.position, fixedPos [a].transform.position) <= 0.2f) {
				//button active
				tempSlot = slot [a];
				slot [a] = slot [b];
				slot [b] = tempSlot;
				mixTimes++;
				mixDone = true;	
				if (roundNo == 2) {
					for (int i = 0; i < buttons.Length; i++) {
						//buttons [i].GetComponent<Button> ().enabled = false;
						buttons [i].SetActive (true);
					}
				}
			}
		//}
	}
		
	void PlayerMix(){
		if (!q22_1) {
			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].SetActive (true);
				//buttons [i].GetComponent<Button> ().enabled = true;
			}
			Debug.Log ("mix true");
			systemMessage.SetActive (true);
			systemMessage.GetComponentInChildren<Text> ().text = "10초 동안 섞어주세요!";
			q22_1 = true;
			mixDone = true;
			//conversation
		} else {
			if (!mixDone) {
				Mix2 (number [0], number [1]);
			}
		}

		if (startCountSeconds && timeBoard.gameObject.activeSelf) {
			remainSeconds -= Time.deltaTime;
			timeBoard.GetComponent<Text> ().text = "남은 시간 : " + Mathf.RoundToInt (remainSeconds) + " 초";
		}
	}

	IEnumerator JudgeWinning(){
		Debug.Log ("start coroutine");
		yield return new WaitForSeconds (2f);

		Debug.Log ("mix false");
		systemMessage.SetActive (false);
		timeBoard.gameObject.SetActive (true);
		startCountSeconds = true;

		yield return new WaitForSeconds (10);

		//StopCoroutine ("ActivateButtons");
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetActive (false);
			//buttons [i].GetComponent<Button> ().enabled = false;
		}
		startCountSeconds = false;
		timeBoard.gameObject.SetActive (false);

		yield return new WaitForSeconds (1f);

		while (!mixDone) {
			Debug.Log ("yield return null");
			yield return null;
		}

		int answer = 0;
		for (int i = 0; i < slot.Length; i++) {
			if (slot [i].GetComponent<Stage4_6_Fraud> ().neogulmanIn) {
				answer = i;
				Debug.Log ("answer 1 is " + i);
				break;
			}
		}

		tempScorePlayer = scorePlayer;

		if (mixTimes >= 5) {
			int randomNo = Random.Range (0, 3);
			PickAnswer (randomNo);
			if (randomNo == answer) {
				Debug.Log ("neogulman won");
				scoreNeogulman++;
			} else {
				Debug.Log ("player won");
				scorePlayer++;
			}
		} else if (mixTimes < 5) {
			Debug.Log ("neogulman won");
			scoreNeogulman++;
			PickAnswer (answer);
		}
		yield return new WaitForSeconds (2f);

		if (scorePlayer > tempScorePlayer) {
			message_answer.GetComponentInChildren<Text> ().text = "하하 틀렸다!";
			systemMessage.GetComponentInChildren<Text> ().text = "2 라운드 승리!";
		} else {
			message_answer.GetComponentInChildren<Text> ().text = "아니 어떻게!?";
			systemMessage.GetComponentInChildren<Text> ().text = "2 라운드 패배!";
		}
		systemMessage.SetActive (true);
		message_answer.transform.SetParent (slot [answer].transform);
		message_answer.transform.localPosition = new Vector3 (-40, 70, 0);

		ShowAnswer ();
		scoreBoard.text = "Player " + scorePlayer + " : " + scoreNeogulman + " Neogulman";

		StartCoroutine ("MoveOntoNext");
	}

	public void MixButton(int a){
		if (mixDone) {
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
		}
		mixDone = false;
		mixTimes++;
		//StartCoroutine ("ActivateButtons"); // might be changed
	}

//	IEnumerator ActivateButtons(){
//		for (int i = 0; i < buttons.Length; i++) {
//			//buttons [i].GetComponent<Button> ().enabled = false;
//			buttons[i].SetActive(false);
//		}
//		yield return new WaitForSeconds (0.5f); // time scale?
//		for (int i = 0; i < buttons.Length; i++) {
//			//buttons [i].GetComponent<Button> ().enabled = true;
//			buttons[i].SetActive(true);
//		}
//	}

	public void tempComplete(){
		Reset ();
		switch (roundNo) {
		case 1:
			message_answer.GetComponentInChildren<Text> ().text = "내가 너굴맨!";
			StartCoroutine(CopyOneself(neogulman));
			break;
		case 2:
			message_answer.GetComponentInChildren<Text> ().text = "내가 별감!";
			StartCoroutine(CopyOneself (star));
			StartCoroutine ("JudgeWinning");
			break;
		case 3: 
			if (!Stage4_Controller.q [37]) {
				message_answer.GetComponentInChildren<Text> ().text = "내가 너굴맨!";
				StartCoroutine (CopyOneself (neogulman));
			}
			break;
		default:
			break;
		}
		//q22_0 = true;
	}

	public void RestartButton(){
		restart.SetActive (false);
		Stage4_Controller.q [37] = false;
		roundNo = 0;
		scoreNeogulman = 0;
		scorePlayer = 0;
		scoreBoard.text = "Player " + scorePlayer + " : " + scoreNeogulman + " Neogulman";
		q22_1 = false;
		tempComplete ();
	}

	public void Reset(){

		systemMessage.SetActive (false);
		actionMessage.SetActive (false);

		if (scorePlayer >= 2) {
			HideMenu (fraudPanel);
			systemMessage.GetComponentInChildren<Text> ().text = "최종 승리!";
			systemMessage.SetActive (true);
			Stage4_Controller.q [37] = true;
			roundNo = 0;
			//conversation
		} else if (scoreNeogulman >= 2) {
			restart.SetActive (true);
			systemMessage.GetComponentInChildren<Text> ().text = "최종 패배!";
			systemMessage.SetActive (true);
			Stage4_Controller.q [37] = true;
			roundNo = 0;
			//conversation
		} else {
			roundNo++;
			roundBoard.text = roundNo + " Round";
		}

		for (int i = 0; i < slot.Length; i++) {
			slot [i].GetComponent<Stage4_6_Fraud> ().neogulmanIn = false;
			slot [i].GetComponent<Image> ().sprite = null;
		}
		message_answer.SetActive(false);

		q22_0 = false;
		mixTimes = 0;
		remainSeconds = 10;
		clickable = false;
		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].SetActive (false);
		}
	}

	IEnumerator CopyOneself(Sprite figure){
		slot [1].GetComponent<Image> ().sprite = figure;
		slot [1].GetComponent<Stage4_6_Fraud> ().neogulmanIn = true;
		message_answer.transform.SetParent (slot [1].transform);
		message_answer.transform.localPosition = new Vector3 (-40, 70, 0);
		message_answer.SetActive (true);

		yield return new WaitForSeconds (2);
		//effect
		message_answer.SetActive(false);
		slot [0].GetComponent<Image> ().sprite = figure;
		slot [2].GetComponent<Image> ().sprite = figure;

		yield return new WaitForSeconds (2);
		q22_0 = true;
	}

	void PickAnswer(int i){ //show message
		message_answer.transform.SetParent (slot [i].transform);
		message_answer.transform.localPosition = new Vector3 (-40, 70, 0);
		message_answer.GetComponentInChildren<Text> ().text = "이 녀석이지!";
		message_answer.SetActive(true);
	}

	public void ShowAnswer(){

		for (int j = 0; j < slot.Length; j++) {
			if (!slot [j].GetComponent<Stage4_6_Fraud> ().neogulmanIn) {
				slot [j].GetComponent<Image> ().sprite = null;
				//disappear effect
			} else {
				Debug.Log ("answer 2 is " + j);
			}
		}
		message_answer.SetActive (true);
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

	IEnumerator MoveOntoNext(){
		yield return new WaitForSeconds (2f);
		tempComplete ();
		yield return null;
	}

	void ShowMenu(Transform menu)
	{
		menu.GetComponent<Animator>().SetBool("show", true);
	}

	void HideMenu(Transform menu)
	{
		menu.GetComponent<Animator>().SetBool("show", false);
	}
}
