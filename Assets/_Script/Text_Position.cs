using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Position : MonoBehaviour {

	private Camera mainCam;
	RectTransform rectTransform;
	RectTransform starPos;
	private int starItemIndex;
	private GameObject textPos;
	public string nameFind;
	private Item_Controller ic;
	private TurnOnOffItemList itemlist;

	void Awake () {
		mainCam = Camera.main;
		rectTransform = GetComponent<RectTransform> ();
		//Debug.Log (nameFind);
	}

	void Start(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		itemlist = ic.GetComponentInChildren<TurnOnOffItemList> ();
	}

	void OnEnable(){
		mainCam = Camera.main;

		if (GameObject.Find (nameFind + "TextPos") != null) { // for Coco, Ivon, Racoon
			textPos = GameObject.Find (nameFind + "TextPos");
			Debug.Log ("rect position 1 " + rectTransform.position + ", " + nameFind + "textPos " + textPos.transform.position);
			rectTransform.position = mainCam.WorldToScreenPoint (textPos.transform.position);
			Debug.Log ("rect position 2 " + rectTransform.position + "textPos " + textPos.transform.position);
		}

		if (nameFind == "Star") {
			ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
			for (int i = 0; i < ic._item_list.Length; i++) {
				if (ic._item_name_list [i] == "Star") {
					starItemIndex = i;
					starPos = GameObject.Find ("Item_button_" + i).GetComponent<RectTransform> ();
					rectTransform.position = new Vector3 (starPos.position.x - 50, starPos.position.y, starPos.position.z);
					itemlist.OnTime = Time.realtimeSinceStartup;
					break;
				}
			}
		}

	}

	void Update(){
		if (this.gameObject.activeSelf && nameFind == "Star") {
			starPos = GameObject.Find ("Item_button_" + starItemIndex).GetComponent<RectTransform> ();
			rectTransform.position = new Vector3 (starPos.position.x - 50, starPos.position.y, starPos.position.z);
			itemlist.OnTime = Time.realtimeSinceStartup;
		}

	}
}
