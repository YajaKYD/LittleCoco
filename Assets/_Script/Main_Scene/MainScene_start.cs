using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene_start : MonoBehaviour {

	public GameObject player;
	public GameObject item_Canvas;
	public GameObject dialogue_Canvas;
	public GameObject nosaved_Panel;
	public GameObject restart_Check;
	public GameObject[] _Controllers;
	public GameObject _SettingCanvas;

	private Item_Controller ic;
	//private Text_Importer2 ti;

	void Awake(){
		Screen.SetResolution (Screen.width, Screen.width * 9 / 16, true);
		DontDestroyOnLoad (_SettingCanvas);
	}

	public void Change_Scene(string scene_name){
		SceneManager.LoadScene (scene_name);
	}

	public void StartTutorial(){
		player.SetActive (true);
		//item_Canvas.SetActive (true);
		DontDestroyOnLoad (dialogue_Canvas);

		SceneManager.LoadScene ("Tutorial1");
	}

	public void Load_lastGame(){
		
		if (PlayerPrefs.GetInt ("Restart_SceneNum") != 0) {
			restart_Check.SetActive (true);
		} else {
			nosaved_Panel.SetActive (true);
		}
	}

	public void Restart(){

		if (PlayerPrefs.GetInt ("Restart_SceneNum") != 0) {
			player.SetActive (true);
			item_Canvas.SetActive (true);
			if (PlayerPrefs.GetInt ("Restart_SceneNum") != 3) {
				DontDestroyOnLoad (dialogue_Canvas);
			}

			PlayerPrefs.SetInt ("SceneFromWhere", 0); //start pos resetting
			SceneManager.LoadScene (PlayerPrefs.GetInt ("Restart_SceneNum"));

			ic = item_Canvas.GetComponent<Item_Controller> ();
			ic._item_name_list = PlayerPrefsX.GetStringArray ("IC_nameList");
			ic._usable_item = PlayerPrefsX.GetBoolArray ("Usable_item");
			ic._the_number_of_items = PlayerPrefsX.GetIntArray ("NumOfItem");
			ic._interaction_object = PlayerPrefsX.GetStringArray ("Interaction");
			ic._consumable = PlayerPrefsX.GetBoolArray ("Consumable");
			ic._explanations = PlayerPrefsX.GetStringArray ("Explanation");
			for (int xx = 0; xx < ic._item_list.Length; xx++) {
				if (ic._item_name_list [xx] != "") {
					Texture2D assas = (Texture2D)Resources.Load ("ItemPictures/" + ic._item_name_list [xx]);
					Rect r = new Rect (0, 0, assas.width, assas.height);
					ic._item_list [xx].GetComponent<Image> ().sprite = Sprite.Create (assas, r, new Vector2 (0, 0));
					ic._item_list [xx].GetComponent<Image> ().color = new Color (1,1,1,1);
					ic._item_list [xx].transform.parent.GetComponentInChildren<Text> ().text = ic._the_number_of_items [xx].ToString();
					ic._item_list [xx].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 1);
				}
			}


			//ti = dialogue_Canvas.GetComponent<Text_Importer2> ();

			if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 4 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 10) {
				//ti.Import (4);
				_Controllers [0].SetActive (true);
				DontDestroyOnLoad (_Controllers [0]);
			} else if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 11 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 14) {
				player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
				//ti.Import (11);
				_Controllers [1].SetActive (true);
				DontDestroyOnLoad (_Controllers [1]);
				//음악을 달아야 한다.
			} else if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 15 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 24) {
				if (PlayerPrefs.GetInt ("Restart_SceneNum") < 18) {
					player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
				}
				//ti.Import (15);
				_Controllers [2].SetActive (true);
				DontDestroyOnLoad (_Controllers [2]);
				//음악을 달아야 한다.
			} else if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 25 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 31) {
				//ti.Import (25);
				_Controllers [3].SetActive (true);
				DontDestroyOnLoad (_Controllers [3]);
			} else if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 32 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 42) {
				//stage 5
				//ti.Import (32);
				_Controllers [4].SetActive (true);
				DontDestroyOnLoad (_Controllers [4]);
			}
			//ti.currLineArr = PlayerPrefsX.GetIntArray ("CurrArr");

			Stage1_Controller.q = PlayerPrefsX.GetBoolArray ("Stage1_Quest");
			Stage2_Controller.q = PlayerPrefsX.GetBoolArray ("Stage2_Quest");
			Stage2_Controller._Stage2_Quest_intArr = PlayerPrefsX.GetIntArray ("Stage2_Quest_INT");
			Stage3_Controller.q = PlayerPrefsX.GetBoolArray ("Stage3_Quest");
			Stage4_Controller.q = PlayerPrefsX.GetBoolArray ("Stage4_Quest");
			Stage5_Controller.q = PlayerPrefsX.GetBoolArray ("Stage5_Quest");
            Stage6_Controller.q = PlayerPrefsX.GetBoolArray("Stage6_Quest");

			//Stage2 Sound//
			if (_Controllers [1].activeSelf && Stage2_Controller.q [12] && !Stage2_Controller.q[18]) {
				GameObject _rain = _Controllers [1].transform.GetChild (0).gameObject;
				GameObject _classic = _Controllers [1].transform.GetChild (1).gameObject;
				_rain.SetActive (false);
				_classic.SetActive (true);
			}
			if (_Controllers [1].activeSelf && Stage2_Controller.q [18] && !Stage2_Controller.q[19]) {
				GameObject _rain = _Controllers [1].transform.GetChild (0).gameObject;
				GameObject _classic = _Controllers [1].transform.GetChild (1).gameObject;
				GameObject _orgelsound = _Controllers [1].transform.GetChild(2).gameObject;
				_rain.SetActive (false);
				_classic.SetActive (true);
				_orgelsound.SetActive(true);
				_orgelsound.GetComponent<AudioSource> ().volume = 0.4f;
			}
			if (_Controllers [1].activeSelf && Stage2_Controller.q [19]) {
				GameObject _rain = _Controllers [1].transform.GetChild (0).gameObject;
				GameObject _classic = _Controllers [1].transform.GetChild (1).gameObject;
				GameObject _orgelsound = _Controllers [1].transform.GetChild(2).gameObject;
				_rain.SetActive (false);
				_classic.SetActive (false);
				_orgelsound.SetActive(true);
			}
			//Stage2 Sound//

			//Stage3 Sound//
			if (_Controllers [2].activeSelf && PlayerPrefs.GetInt ("Restart_SceneNum") >= 18) {
				GameObject _room = _Controllers [2].transform.GetChild (0).gameObject;
				GameObject _park = _Controllers [2].transform.GetChild (1).gameObject;
				_room.SetActive (false);
				_park.SetActive(true);
			}
			//Stage3 Sound//

			Selecting_stage._what_stage_now_cleared = PlayerPrefs.GetInt ("Stage_Now_Cleared");
			print (Selecting_stage._what_stage_now_cleared);
//			if (PlayerPrefs.GetInt ("Restart_SceneNum") >= 11 && PlayerPrefs.GetInt ("Restart_SceneNum") <= 14) {
//				ti.Import (11);
//			}


		}
	}


}
