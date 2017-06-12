using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Jump_MouseDown : MonoBehaviour, IPointerDownHandler {

	public virtual void OnPointerDown(PointerEventData ped){
//		OnDrag (ped);
//		onoffList.OnTime = Time.realtimeSinceStartup;
		Moving_by_RLbuttons aa = GameObject.FindWithTag("Player").GetComponent<Moving_by_RLbuttons>();
		if (aa.enabled) {
			aa.Jumping ();
		}
	}

}
