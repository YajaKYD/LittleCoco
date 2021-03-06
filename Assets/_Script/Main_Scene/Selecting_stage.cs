﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Selecting_stage : MonoBehaviour, IPointerClickHandler {

	Main_Select_Stage_Controller mssc;
	public int this_num; //stage_controller가 해줌
	public bool xxx;
	public static int _what_stage_now_cleared;
	private Text_Importer aa;

	public GameObject[] _stage2_con_di;

	private Color un_click = new Color (160f, 160f, 160f);
	//private bool once = false;
//	private Color clicked = new Color (255f, 255f, 255f);

	void Awake(){
		
		GetComponent<Image> ().color = new Color (0.5f,0.5f,0.5f);

		mssc = GameObject.Find ("Stage_select_controller").GetComponent<Main_Select_Stage_Controller> ();
		xxx = false;
		aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();

        if (_what_stage_now_cleared == 5)
        {
            SceneManager.LoadScene(43);
            _stage2_con_di[1].SetActive(true);
            DontDestroyOnLoad(_stage2_con_di[1]);
        }
        else if (_what_stage_now_cleared == 0 && this_num == 0) {
			SceneManager.LoadScene (4);
			//_stage2_con_di [0].SetActive (true);
			_stage2_con_di [1].SetActive (true);
			//DontDestroyOnLoad (_stage2_con_di [0]);
			DontDestroyOnLoad (_stage2_con_di [1]);
			//aa.Import (4);
		}
	}

	void Update(){
		

		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.Alpha1)) {
			_what_stage_now_cleared = 0;
		}
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.Alpha2)) {
			_what_stage_now_cleared = 1;
		}
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.Alpha3)) {
			_what_stage_now_cleared = 2;
		}
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.Alpha4)) {
			_what_stage_now_cleared = 3;
		}
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.Alpha5)) {
			_what_stage_now_cleared = 4;
		}
	}

	public void OnPointerClick (PointerEventData eventData) 
	{

		if(xxx && _what_stage_now_cleared == this_num){ //두번 눌림
			//실행
			switch (this_num) {
			case 0:
				SceneManager.LoadScene (4);
				//_stage2_con_di [0].SetActive (true);
				_stage2_con_di [1].SetActive (true);
				//DontDestroyOnLoad (_stage2_con_di [0]);
				DontDestroyOnLoad (_stage2_con_di [1]);
				//aa.Import (4);
				break;
			case 1:
				SceneManager.LoadScene (11);
				//_stage2_con_di [0].SetActive (true);
				_stage2_con_di [1].SetActive (true);
				//DontDestroyOnLoad (_stage2_con_di [0]);
				DontDestroyOnLoad (_stage2_con_di [1]);
				//aa.Import (11);
				break;
			case 2:
				SceneManager.LoadScene (15);
				//_stage2_con_di [0].SetActive (true);
				_stage2_con_di [1].SetActive (true);
				//DontDestroyOnLoad (_stage2_con_di [0]);
				DontDestroyOnLoad (_stage2_con_di [1]);
				//aa.Import (15);
				break;
			case 3:
				SceneManager.LoadScene (25);
				//_stage2_con_di [0].SetActive (true);
				_stage2_con_di [1].SetActive (true);
				//DontDestroyOnLoad (_stage2_con_di [0]);
				DontDestroyOnLoad (_stage2_con_di [1]);
				//aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> (); // why???? only this one????
				//aa.Import (25);
				break;
			case 4:
				SceneManager.LoadScene (32);
				//_stage2_con_di [0].SetActive (true);
				_stage2_con_di [1].SetActive (true);
				//DontDestroyOnLoad (_stage2_con_di [0]);
				DontDestroyOnLoad (_stage2_con_di [1]);
				//aa.Import (32);
				break;
			case 5:  
				SceneManager.LoadScene (43);
				_stage2_con_di [1].SetActive (true);
				DontDestroyOnLoad (_stage2_con_di [1]);
				break;

			default:
				break;
			}
		}

		if (!xxx && _what_stage_now_cleared == this_num) { //처음 눌림
			
			GetComponent<Image> ().color = new Color (1f,1f,1f);
			mssc.Click_Once(this_num);
			xxx = true;
		} 
	}

}


