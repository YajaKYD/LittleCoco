using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Jump_MouseDown : MonoBehaviour, IPointerDownHandler {

	public virtual void OnPointerDown(PointerEventData ped){
//		OnDrag (ped);
//		onoffList.OnTime = Time.realtimeSinceStartup;
		GameObject pl = GameObject.FindWithTag("Player");
		Moving_by_RLbuttons aa = pl.GetComponent<Moving_by_RLbuttons>();
		if (aa.enabled && pl.GetComponent<Rigidbody2D>().velocity == Vector2.zero) {
			aa.Jumping ();
		}
	}

}
