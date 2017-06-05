using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_GameStart : MonoBehaviour, IPointerClickHandler{

	public GameObject askpanel;

	public void OnPointerClick(PointerEventData ped){
		if (PlayerPrefs.GetInt ("Restart_SceneNum") == 0) {
			SceneManager.LoadScene ("Tutorial1");
		} else {
			askpanel.SetActive (true);
		}
	}

}
