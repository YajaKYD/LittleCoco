using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Instantiate_Message : MonoBehaviour, IPointerClickHandler {
    public int index;
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
			Debug.Log ("collision");
			tc.instantiateMessage (index);
            able_message = true;
        }
    }
}
