using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage3_7_earphone_message : MonoBehaviour, IPointerDownHandler {

	private Text_Importer ti;

	public virtual void OnPointerDown(PointerEventData eventdata){
		gameObject.SetActive (false);
		Stage3_Controller._Stage3_Quest[20] = true;
	}

	void OnDisable(){
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
		ti.currLineArr [0] = 6; //코코 대사 
		ti.NPC_Say_yeah ("코코");
	}
}
