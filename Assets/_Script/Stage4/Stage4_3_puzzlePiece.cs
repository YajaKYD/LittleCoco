using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage4_3_puzzlePiece : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Stage4_3_GameController2 gameController;
	public Transform[] emptyPos;
	public int puzzleNo;
	private Vector3 defaultPos;

	void Start () {
		gameController = GameObject.Find ("Stage4_3_GameController").GetComponent<Stage4_3_GameController2> ();
		Debug.Log ("start" + transform.position);
		defaultPos = transform.position;
	}

	void Update () {
		
	}

	public bool JudgePuzzle(){
		bool result = false;
		for (int i = 0; i < emptyPos.Length; i++) {
			if (Vector3.Distance (transform.position, emptyPos[i].position) <= 50 && gameController.posAvailable[i]) {
				transform.position = emptyPos[i].position;
				if (puzzleNo == i) {
					Stage4_Controller.q20 [puzzleNo] = true;
					Debug.Log ("quest19 " + Stage4_Controller.q20 [puzzleNo]);
				}
				result = true;
				break;
			} else {
				result = false;
			}
		}
		return result;
	}

	public virtual void OnDrag(PointerEventData ped){
		transform.position = Input.mousePosition;
		//JudgePuzzle ();
	}

	public virtual void OnPointerDown(PointerEventData ped){
	}

	public virtual void OnPointerUp(PointerEventData ped){
		if (JudgePuzzle ()) {

		} else {
			Debug.Log ("false");
			transform.position = defaultPos;
			Stage4_Controller.q20 [puzzleNo] = false;
			Debug.Log ("quest19 " + Stage4_Controller.q20 [puzzleNo]);
		}

		gameController.JudgePosAvailable ();
	}
}
