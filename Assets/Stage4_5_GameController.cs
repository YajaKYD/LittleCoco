using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_GameController : MonoBehaviour {

	public GameController controller;

	void Start () {
		controller = GameObject.Find ("PuzzleController").GetComponent<GameController> ();
		controller.StartGame (1);
	}

	void Update () {
		
	}
}
