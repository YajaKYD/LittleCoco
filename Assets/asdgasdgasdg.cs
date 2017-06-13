using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class asdgasdgasdg : Button {

	Moving_by_RLbuttons aa;
	private GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player");
		aa = player.GetComponent<Moving_by_RLbuttons> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (IsPressed() && player.activeSelf) { //Player active해제 시 안먹힘
			aa.Jumping ();
		}

	}

	public void AA(){
		aa.in_air = true;
	}
}
