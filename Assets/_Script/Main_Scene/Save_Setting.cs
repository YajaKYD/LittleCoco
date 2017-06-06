using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save_Setting : MonoBehaviour {

	public Toggle tog;
	public Slider sl;

	void OnDisable(){
		PlayerPrefsX.SetBool ("Music_ONOFF", tog.isOn);
		PlayerPrefs.SetFloat ("Music_Volume", sl.value);
		print ("savesetting");
	}

}
