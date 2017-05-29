using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeSetting : MonoBehaviour {

	Slider sl;
	public Toggle tog;

	void Awake(){
		sl = GetComponent<Slider> ();
	}

	void Update(){
		if (tog.isOn) {
			AudioListener.volume = 0f;
		} else {
			AudioListener.volume = sl.value;
		}
	}

}
