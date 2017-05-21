using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FORTEST_SkipStage : MonoBehaviour {

	public GameObject player;
	public GameObject item_Canvas;
	public GameObject dialogue_Canvas;
	public GameObject _stage3_Controller;

	private Item_Controller ic;
	private Text_Importer ti;

	public void Skip_to(){
		player.SetActive (true);
		item_Canvas.SetActive (true);
		DontDestroyOnLoad (dialogue_Canvas);
		ic = item_Canvas.GetComponent<Item_Controller> ();
		ti = dialogue_Canvas.GetComponent<Text_Importer> ();

		SceneManager.LoadScene (15);

		ic._item_name_list [0] = "Diary";
		ic._item_name_list [1] = "Star";
		ic._usable_item [0] = false;
		ic._usable_item [1] = true;
		ic._the_number_of_items [0] = 1;
		ic._the_number_of_items [1] = 1;
		ic._interaction_object [0] = "";
		ic._interaction_object [1] = "Player";
		ic._consumable [0] = false;
		ic._consumable [1] = false;
		ic._explanations [0] = "Test용으로 넘어옴";
		ic._explanations [1] = "Test용으로 넘어옴";
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

		ti.Import (15);
		_stage3_Controller.SetActive (true);
		DontDestroyOnLoad (_stage3_Controller);

	}
}
