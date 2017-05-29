using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; 

public class MenuOnOff : MonoBehaviour, IPointerDownHandler {

	public GameObject _MenuPanel;
	public GameObject _exitpanel;

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			_exitpanel.SetActive (true);
			//Application.Quit ();
		}
	}

	public void OnPointerDown(PointerEventData ped)
	{
		if (_MenuPanel.activeSelf) {
			_MenuPanel.SetActive (false);
		}else {
			_MenuPanel.SetActive (true);
		}
	}

	public void Exit_Game(){
		Application.Quit ();
	}

	public void Return_to_Main(){
		
		_MenuPanel.SetActive (false);
		if (GameObject.FindWithTag ("Player") != null) {
			Destroy (GameObject.FindWithTag ("Player"));
		}
		if (GameObject.FindWithTag ("Dialogue") != null) {
			Destroy (GameObject.FindWithTag ("Dialogue"));
		}
		if (GameObject.FindWithTag ("Controller") != null) {
			Destroy (GameObject.FindWithTag ("Controller"));
		}
		if (GameObject.FindWithTag ("Item_Canvas") != null) {
			Destroy (GameObject.FindWithTag ("Item_Canvas"));
		}
		if (GameObject.FindWithTag ("Setting") != null) {
			Destroy (GameObject.FindWithTag ("Setting"));
		}

		SceneManager.LoadScene (0);
	}
}
