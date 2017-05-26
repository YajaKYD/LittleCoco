using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Load_data : MonoBehaviour {

	public int _current_LINE;
	public int _where_are_you_from;

	void Awake () {
		Load ();
		autoSave_to_restart ();
	}

	public void Load(){
		if (PlayerPrefs.GetInt ("SceneFromWhere") != 0) {
			_where_are_you_from = PlayerPrefs.GetInt ("SceneFromWhere");
		}
	}

	public void autoSave_to_restart(){
		PlayerPrefs.SetInt ("Now_SceneNum",SceneManager.GetActiveScene ().buildIndex);
	}
}
