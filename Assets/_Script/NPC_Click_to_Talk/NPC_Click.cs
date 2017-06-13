using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_Click : MonoBehaviour {

	private Text_Importer aa;
	public GameObject text_box;

	void Awake(){
		aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
	}

	void OnMouseDown(){
		if (EventSystem.current.IsPointerOverGameObject () || //click
			EventSystem.current.IsPointerOverGameObject (0)) { //mobile
			return;
		}
		aa.NPC_Say_yeah (name);
	}
}
