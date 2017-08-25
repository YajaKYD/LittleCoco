using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage4_6_Fraud : MonoBehaviour, IPointerClickHandler {

	public bool neogulmanIn;
	private Stage4_6_GameController2 controller;
	private Text scoreBoard;

	void Start () {
		controller = GameObject.Find ("Stage4_6_GameController").GetComponent<Stage4_6_GameController2> ();
		scoreBoard = GameObject.Find ("Score").GetComponent<Text> ();
	}

	public void OnPointerClick(PointerEventData eventData){
		if (controller.clickable) {
			if (neogulmanIn) {
				controller.scorePlayer++;
				controller.message_answer.GetComponentInChildren<Text> ().text = "날 찾아내다니..!";
				controller.systemMessage.GetComponentInChildren<Text> ().text = controller.roundNo + " 라운드 승리!";
			} else if (!neogulmanIn) {
				controller.scoreNeogulman++;
				controller.message_answer.GetComponentInChildren<Text> ().text = "속았군!!";
				controller.systemMessage.GetComponentInChildren<Text> ().text = controller.roundNo + "라운드 패배!";
			}
			controller.ShowAnswer ();
			controller.clickable = false;
			controller.systemMessage.SetActive (true);
			controller.actionMessage.SetActive (false);
			scoreBoard.text = "Player " + controller.scorePlayer + " : " + controller.scoreNeogulman + " Neogulman";
			StartCoroutine ("MoveOntoNext");
		}
	}

	IEnumerator MoveOntoNext(){
		yield return new WaitForSeconds (2f);
		controller.tempComplete ();
		yield return null;
	}
}
