using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_GameStart : MonoBehaviour, IPointerClickHandler{

	public GameObject askpanel;

	public GameObject player;
	public GameObject item_Canvas;
	public GameObject dialogue_Canvas;

	public void OnPointerClick(PointerEventData ped){
		if (PlayerPrefs.GetInt ("Restart_SceneNum") == 0) {
			
			player.SetActive (true);
			//item_Canvas.SetActive (true);
			DontDestroyOnLoad (dialogue_Canvas);

			SceneManager.LoadScene ("Tutorial1");
		} else {
			askpanel.SetActive (true);
		}
	}

}
