using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stage3_7_earphone_message : MonoBehaviour, IPointerDownHandler {

	private Text_Importer2 ti;

    void Start()
    {
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(37);
    }
    public virtual void OnPointerDown(PointerEventData eventdata){
		gameObject.SetActive (false);
		Stage3_Controller.q[20] = true;
	}

	void OnDisable(){
        ti.Talk(); // munch
	}
}
