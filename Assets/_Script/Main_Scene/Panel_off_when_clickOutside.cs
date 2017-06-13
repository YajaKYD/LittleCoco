using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_off_when_clickOutside : MonoBehaviour {

	void Update () {

		if (Input.GetMouseButtonDown (0) &&
			!RectTransformUtility.RectangleContainsScreenPoint (
				this.GetComponent<RectTransform> (), Input.mousePosition)) {
			this.gameObject.SetActive (false);
		}
	}
}
