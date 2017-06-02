using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_Diary : MonoBehaviour {

	public GameObject Diary_Canvas;
	public GameObject mbr;
	public GameObject item_canvas;

	private Image img;
	private Button but;

	void Awake(){
		img = GetComponent<Image> ();
		but = GetComponent<Button> ();
	}

	void Update(){
		if (SceneManager.GetActiveScene ().buildIndex < 11) {
			img.color = new Color (1, 1, 1, 0.5f);
			but.enabled = false;
		} else {
			img.color = new Color (1, 1, 1, 1);
			but.enabled = true;
		}
	}

	public void Diary_Click(){
		if (!Diary_Canvas.activeSelf) {
			if (GameObject.FindWithTag ("Player") != null) {
				mbr = GameObject.FindWithTag ("Player");
				mbr.GetComponent<Moving_by_RLbuttons> ().enabled = false;
			}
			if (GameObject.FindWithTag ("Item_Canvas") != null) {
				item_canvas = GameObject.FindWithTag ("Item_Canvas");
				item_canvas.GetComponent<Canvas> ().enabled = false;
			}
			Diary_Canvas.SetActive (true);
			if (PlayerPrefs.GetInt ("Stage_Now_Cleared") != 5) {
				Diary_Canvas.GetComponentInChildren<BookPro> ().CurrentPaper = PlayerPrefs.GetInt ("Stage_Now_Cleared") + 1;
			} else { //page 6은 없으므로 5에 고정
				Diary_Canvas.GetComponentInChildren<BookPro> ().CurrentPaper = PlayerPrefs.GetInt ("Stage_Now_Cleared");
			}
		}
	}

	public void Diary_Close(){
		mbr.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		item_canvas.GetComponent<Canvas> ().enabled = true;
	}
}
