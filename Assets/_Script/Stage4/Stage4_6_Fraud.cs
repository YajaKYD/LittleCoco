using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage4_6_Fraud : MonoBehaviour, IPointerClickHandler {

	public bool neogulmanIn;
	private Stage4_6_GameController controller;
	private Text scoreBoard;

	void Start () {
		controller = GameObject.Find ("Stage4_6_GameController").GetComponent<Stage4_6_GameController> ();
		scoreBoard = GameObject.Find ("Score").GetComponent<Text> ();
	}

	public void OnPointerClick(PointerEventData eventData){
		if (controller.clickable) {
			if (neogulmanIn) {
				controller.scorePlayer++;
				controller.message_answer.GetComponentInChildren<Text> ().text = "날 찾아내다니..!";
			} else if (!neogulmanIn) {
				controller.scoreNeogulman++;
				controller.message_answer.GetComponentInChildren<Text> ().text = "속았군!!";
			}
			controller.ShowAnswer ();
			controller.clickable = false;
			scoreBoard.text = "Player " + controller.scorePlayer + " : " + controller.scoreNeogulman + " Neogulman";
		}
	}
}
