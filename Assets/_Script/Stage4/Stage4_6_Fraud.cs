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
		Debug.Log ("click 1");
		if (controller.clickable && neogulmanIn) {
			//click effect
			transform.GetChild(0).GetComponent<Image>().enabled = true; // if getcomponentinchildren -> affect parent
			GetComponent<Image>().enabled = false;
			controller.clickable = false;
			controller.scorePlayer++;
			scoreBoard.text = "Player " + controller.scorePlayer + " : " + controller.scoreNeogulman + " Neogulman";
		} else if(controller.clickable && !neogulmanIn){
			GetComponent<Image>().enabled = false;
			controller.clickable = false;
			controller.scoreNeogulman++;
			scoreBoard.text = "Player " + controller.scorePlayer + " : " + controller.scoreNeogulman + " Neogulman";
		}
	}
}
