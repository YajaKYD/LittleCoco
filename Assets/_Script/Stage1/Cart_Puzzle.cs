using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Puzzle : MonoBehaviour {

	public GameObject dog_in_a_cart;

	private GameObject player;

	void Awake(){
		player = GameObject.Find ("Player");
	}



	void OnMouseDown(){
		GameObject.Find ("Main Camera").GetComponent<CameraManager> ().FocusObject = dog_in_a_cart;
		player.SetActive (false);
		dog_in_a_cart.SetActive (true);
		gameObject.SetActive (false);
	}
}
