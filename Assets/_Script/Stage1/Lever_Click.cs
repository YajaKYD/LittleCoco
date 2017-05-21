using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Click : MonoBehaviour {

	void OnMouseDown(){
		GameObject.Find ("Lever_Puzzle").GetComponent<Lever_Puzzle> ().Click_Lever (int.Parse (name));
	}
}
