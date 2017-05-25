using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Diary : MonoBehaviour {

	public GameObject Diary_Canvas;
	public GameObject Diary_Bgi;
	public GameObject CloseButton;

	public Moving_by_RLbuttons mbr;
	public GameObject item_canvas;

	public void Diary_Click(){
		if (!Diary_Canvas.activeSelf) {
			if (GameObject.FindWithTag ("Player") != null) {
				mbr = GameObject.FindWithTag ("Player").GetComponent<Moving_by_RLbuttons> ();
				mbr.enabled = false;
			}
			if (GameObject.FindWithTag ("Item_Canvas") != null) {
				item_canvas = GameObject.FindWithTag ("Item_Canvas");
				item_canvas.SetActive (false);
			}
			Diary_Canvas.SetActive (true);
			Diary_Bgi.SetActive (true);
			CloseButton.SetActive (true);
		}
	}


}
