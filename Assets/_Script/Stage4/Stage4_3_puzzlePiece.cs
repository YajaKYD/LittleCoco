using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage4_3_puzzlePiece : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	public Transform emptyPos;
	private Transform defaultPos;

	void Start () {
		defaultPos.position = transform.position;
		Debug.Log ("start" + transform.position);
	}

	void Update () {
		
	}

	public bool JudgePuzzle(){
		Debug.Log ("Distance is " + Vector3.Distance (transform.position, emptyPos.position));
		if (Vector3.Distance (transform.position, emptyPos.position) <= 50) {
			transform.position = emptyPos.position;
			return true;
		} else {
			return false;
		}
	}

	public virtual void OnDrag(PointerEventData ped){
		transform.position = Input.mousePosition;
		JudgePuzzle ();
	}

	public virtual void OnPointerDown(PointerEventData ped){
	}

	public virtual void OnPointerUp(PointerEventData ped){
		//Debug.Log ("Pointer up" + defaultPos.position);
		if (!JudgePuzzle ()) {
			Debug.Log ("false");
			//transform.position = defaultPos.position;
		}

	}
}
