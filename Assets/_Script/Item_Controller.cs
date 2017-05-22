using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Controller : MonoBehaviour {

	public TextAsset csv_Item_list;
	public GameObject[] _item_list;
	public string[] _item_name_list;
	public bool[] _usable_item;
	public int[] _the_number_of_items;
	public string[] _interaction_object;
	public bool[] _consumable;
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public string[] _explanations;
	public Item_Drag[] _itemDrags;
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//public Text _get_item_name;

	public string _now_used_item;
	//public int _current_line;
	public string _now_object_item_used;
	public bool _to_run_once = false;
	public AudioSource _getItem;

	private int now_clicked_item;

	public TurnOnOffItemList onofflist;
	public bool cant_pick_during_using; //아이템 사용을 위해 활성화 시 다른 아템을 먹을 수 없다.

	void Awake(){
		
		onofflist = GetComponentInChildren<TurnOnOffItemList> ();
		DontDestroyOnLoad (transform.gameObject);
		cant_pick_during_using = true;
		for (int i = 0; i < 5; i++) {
			_item_name_list [i] = "";
			_usable_item[i] = false;
			_the_number_of_items[i] = 0;
			_interaction_object [i] = "";
			_consumable [i] = false;
			_item_list [i].GetComponent<Image> ().color = new Color (255, 255, 255, 0);
			_item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (255, 255, 255, 0);
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			_explanations[i] = "";
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}

	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void Get_Item(GameObject item ,string item_name, Sprite item_image, bool usable, string interaction, bool consumable, string explanation){
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		if (cant_pick_during_using) {
			_getItem.Play ();

			for (int i = 0; i < 5; i++) {
				if (_item_name_list [i] == item_name) {//이미 같은 이름의 아이템이 있을 경우
					_the_number_of_items [i]++;//갯수 하나 늘림
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().text = _the_number_of_items [i].ToString ();
					Destroy (item);

					if (onofflist.OnOffButton.localScale.x == 1) { //아이템창이 꺼져있는 상태면
						onofflist.TurnOnOffitemList();
					}

					Save_Script.Save_Item_Info ();
					return;
					//StartCoroutine (Item_name_popup (item_name));
				}
			}
			for (int i = 0; i < 5; i++) {
				if (_item_name_list [i] == "") {//빈 아이템 창일 경우
					_item_name_list [i] = item_name;//이름 입력
					_the_number_of_items [i]++;//갯수 1
					_interaction_object [i] = interaction;// 인터랙션 가능한 물체 입력
					_usable_item [i] = usable;//사용가능여부 입력
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().text = _the_number_of_items [i].ToString ();
					_item_list [i].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 1);
					_item_list [i].GetComponent<Image> ().sprite = item_image;//이미지 입력
					_consumable [i] = consumable;
					//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					_explanations [i] = explanation;
					//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					Destroy (item);
					_itemDrags [i].enabled = true;
					if (onofflist.OnOffButton.localScale.x == 1) { //아이템창이 꺼져있는 상태면
						onofflist.TurnOnOffitemList();
					}

					Save_Script.Save_Item_Info ();
					return;
				}
			}

		}
		return;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public bool Get_Item_Auto(int _item_numbering, Sprite item_image){
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		if (cant_pick_during_using) {
			_getItem.Play ();

			char lineSeperator = '\n';
			char fieldSeperator = ',';

			string[] records = csv_Item_list.text.Split (lineSeperator);
			string[] fields = records[_item_numbering].Split(fieldSeperator);

			string item_name = fields[1];
			bool usable;
			string interaction = fields[3];
			bool consumable;
			string explanation = fields[5];

			if(fields[2] == "0"){
				usable = false;
			}else{
				usable = true;
			}
			if (fields [4] == "1") {
				consumable = true;
			} else {
				consumable = false;
			}




			for (int i = 0; i < 5; i++) {
				if (_item_name_list [i] == item_name) {//이미 같은 이름의 아이템이 있을 경우
					_the_number_of_items [i]++;//갯수 하나 늘림
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().text = _the_number_of_items [i].ToString ();

					if (onofflist.OnOffButton.localScale.x == 1) { //아이템창이 꺼져있는 상태면
						onofflist.TurnOnOffitemList();
					}

					Save_Script.Save_Item_Info ();
					return true;
					//StartCoroutine (Item_name_popup (item_name));
				}
			}
			for (int i = 0; i < 5; i++) {
				if (_item_name_list [i] == "") {//빈 아이템 창일 경우
					_item_name_list [i] = item_name;//이름 입력
					//print (_the_number_of_items [i]);
					_the_number_of_items [i]++;//갯수 1
					//print (_the_number_of_items [i]);
					_interaction_object [i] = interaction;// 인터랙션 가능한 물체 입력
					_usable_item [i] = usable;//사용가능여부 입력
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().text = _the_number_of_items [i].ToString ();
					_item_list [i].GetComponent<Image> ().color = new Color (255, 255, 255, 255);
					_item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (255, 255, 255, 255);
					_item_list [i].GetComponent<Image> ().sprite = item_image;//이미지 입력
					_consumable [i] = consumable;
					//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					_explanations [i] = explanation;
					//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					if (onofflist.OnOffButton.localScale.x == 1) { //아이템창이 꺼져있는 상태면
						onofflist.TurnOnOffitemList();
					}
					_itemDrags [i].enabled = true;
					Save_Script.Save_Item_Info ();
					return true;
				}
			}
		}
		return false;
	}

	void Update(){
		if (_now_used_item == "별감" && _now_object_item_used == "Player" && _to_run_once) {//별감이용대화
			//대화진행코드
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.NPC_Say_yeah ("별감");
			//stop
			_to_run_once = false;
		}
	}


	/*
	public void Using_Item(int i){ // 아이템 사용 상태
		if (_item_name_list [i] != null && _usable_item[i]) {
			//해당 아이템 사용이 가능한 오브젝트를 활성화해야함, 이미 활성화되어있다면 취소
			GameObject[] aa = GameObject.FindGameObjectsWithTag(_interaction_object[i]);
			for ( int x = 0 ; x < aa.Length ; x ++){
				if (!aa [x].GetComponent<Outline> ().eraseRenderer) { //기 활성
					Camera.main.GetComponent<OutlineManager>().enabled = false;
					Time.timeScale = 1;//사용취소 시 퍼즈 풀림
					aa [x].GetComponent<Outline> ().eraseRenderer = true;
					cant_pick_during_using = true; //해제 시 새아이템 줍기 가능
				} else if (aa [x].GetComponent<Outline> ().eraseRenderer && aa [x].GetComponent<Outline> ().used_or_not_for_retry) { //활성 안되어있지만 이미 아이템이 들어가 있음
					//Nothing happen! haha!
				} else {
					Time.timeScale = 0;//아이템 사용 시 게임 퍼즈
					Camera.main.GetComponent<OutlineManager>().enabled = true;
					aa [x].GetComponent<Outline> ().eraseRenderer = false;
					aa [x].GetComponent<Outline> ()._item_num = i;
					cant_pick_during_using = false; //아이템 사용 대기 시 새아이템 줍기 불가능
				}
			}
		}
	}
	*/


}
