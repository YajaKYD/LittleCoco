using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Instantiate_Message : MonoBehaviour, IPointerClickHandler {
    public int index;
    public GameObject tutorialMessage;
    public GameObject tutorialMessagePrefab;
    public GameObject canvas;
    private Tutorial_Controller tc;
    private bool able_message;

	//public bool stay_list = false;

    void Start()
    {
		tc = GameObject.FindWithTag("Controller").GetComponent<Tutorial_Controller>();
    }

//	void Update(){
//		if (stay_list) {
//			TurnOnOffItemList il = GameObject.FindWithTag ("Item_Canvas").GetComponentInChildren<TurnOnOffItemList> ();
//			il.OnTime = Time.realtimeSinceStartup;
//		}
//	}

    public void OnPointerClick(PointerEventData eventData)
    {
		tc.instantiateMessage(index);
		//stay_list = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !able_message)
        {
            //Debug.Log("바위를 만났다");
			GameObject ic = GameObject.FindWithTag("Item_Canvas");
            tutorialMessage = Instantiate(tutorialMessagePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tutorialMessage.transform.SetParent(ic.transform, false);
            able_message = true;
        }
    }
}
