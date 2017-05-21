using System.Collections.Generic;
using UnityEngine;

public class Stage2_2_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Item_Controller ic;

	private bool a1a1 = true;
	private bool a2a2 = true;
	private bool a3a3 = true;
	private bool a4a4 = true;

	private bool a5a5 = false;
	private bool a6a6 = false;
	private bool drop_tape = false;

	public GameObject _coco_textbox;
	public GameObject _sparkle;
	public GameObject _sparkle_1;
	public BoxCollider2D _remote;
	public Outline[] _multitap;
	public BoxCollider2D _mirror_use;
	public CircleCollider2D _clockwork;
	public Outline _clockwork_ol;
	public AudioSource _cws;
	public GameObject[] _multi_image;

	void Awake(){
		player = GameObject.Find ("Player");
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 13) {
			player.transform.position = regen_pos.position;
		}

		if (Stage2_Controller._Stage2_Quest[6]) { //multi1 사용
			_multitap [0].used_or_not_for_retry = true;
			Destroy (_multitap [0].gameObject);
		}
		if (Stage2_Controller._Stage2_Quest[7]) { //multi2 사용
			_multitap [1].used_or_not_for_retry = true;
			Destroy (_multitap [1].gameObject);
		}

		if ((Stage2_Controller._Stage2_Quest [6] && !Stage2_Controller._Stage2_Quest [7]) ||
			(!Stage2_Controller._Stage2_Quest [6] && Stage2_Controller._Stage2_Quest [7])) { //둘 중 하나만 했을 경우
			_multi_image[0].SetActive(true);
			_multi_image [3].SetActive (false);
		}

		if (Stage2_Controller._Stage2_Quest [6] && Stage2_Controller._Stage2_Quest [7]) {
			_multi_image [3].SetActive (false);
			_multi_image [0].SetActive (false);
			_multi_image[1].SetActive(true);
		}

		if (Stage2_Controller._Stage2_Quest[5]) {
			_multi_image [0].SetActive (false);
			_multi_image [1].SetActive (false);
			_multi_image [2].SetActive(true);
			_mirror_use.enabled = false;
		}

		if (Stage2_Controller._Stage2_Quest[10] && !Stage2_Controller._Stage2_Quest[11]) {
			_remote.enabled = true;
		}

		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "remotecon") {
				Destroy (_remote.gameObject);
			}
		}

		if (Stage2_Controller._Stage2_Quest[11]) {
			Destroy (_remote.gameObject);
		}

		if (Stage2_Controller._Stage2_Quest[13] && !Stage2_Controller._Stage2_Quest[15]) {
			_sparkle_1.SetActive (true);
			_clockwork.enabled = true;
		}

		if (Stage2_Controller._Stage2_Quest[20]) {
			_sparkle_1.SetActive (true);
			_clockwork.enabled = true;
		}

	}

	void Update(){

		if (!Stage2_Controller._Stage2_Quest[1]) {
			Q2_Talk ();
		}

		if (Stage2_Controller._Stage2_Quest[4] && !Stage2_Controller._Stage2_Quest[5]) {
			Q6_use_multiTap ();
		}

		if (GetComponent<Load_data> ()._where_are_you_from == 13 && Stage2_Controller._Stage2_Quest[5] && Stage2_Controller._Stage2_Quest[9] && !Stage2_Controller._Stage2_Quest[10]) {
			//2-3에 다녀와야 함. >> 6_1도 true되어야 함.
			Q7_find_Remote ();
		}

		if (Stage2_Controller._Stage2_Quest[10] && !Stage2_Controller._Stage2_Quest[11]) {
			if (_remote == null) {
				//print ("SS");
				Stage2_Controller._Stage2_Quest[11] = true;
			}
		}

		if (Stage2_Controller._Stage2_Quest[13] && !Stage2_Controller._Stage2_Quest[15]) {
			Q11_ClockWork ();
		}

		if (Stage2_Controller._Stage2_Quest[20] && !Stage2_Controller._Stage2_Quest[21]) {
			Q16_ClockWork ();
		}
			
		//아템떨궈야하는디~ 한번만~
		if(Stage2_Controller._Stage2_Quest[6] && Stage2_Controller._Stage2_Quest[7] && !Stage2_Controller._Stage2_Quest[25] && Item_Drag._NOW_Shaked && !drop_tape){
			//멀티탭 설치완료
			//shake 완료
			//절연테이프 아직 못먹음 >> 1번 떨어트림
			GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/Tape"));
			k.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			k.transform.position = new Vector3 (k.transform.position.x, k.transform.position.y, 0f);
			k.name = "Tape";
			GameObject j = (GameObject)Instantiate(Resources.Load("Prefabs/Tape"));
			j.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			j.transform.position = new Vector3 (j.transform.position.x, j.transform.position.y, 0f);
			j.name = "Tape";
			drop_tape = true;
		}
			
		if(drop_tape && !Stage2_Controller._Stage2_Quest[25]){
			//떨어트림
			//먹었음.
			for (int i = 0; i < ic._item_list.Length; i++) {
				if (ic._item_name_list [i] == "Tape") {
					Stage2_Controller._Stage2_Quest [25] = true;
					Save_Script.Save_Quest_Info ();
				}
			}
		}
	}

	void Q2_Talk(){
		if (a1a1) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");
			_coco_textbox = GameObject.Find ("코코_text");
			a1a1 = false;
		}

		if (a2a2 && !_coco_textbox.activeSelf) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
			aa.NPC_Say_yeah ("별감");
			a2a2 = false;
			Stage2_Controller._Stage2_Quest[1] = true;
			Save_Script.Save_Quest_Info ();
		} // ? 다음 집 설명. 끝.

	}

	void Q6_use_multiTap(){

		if (!Stage2_Controller._Stage2_Quest[6]) { //multi1 사용
			if (_multitap [0].used_or_not_for_retry) {
				Stage2_Controller._Stage2_Quest[6] = true;
				Save_Script.Save_Quest_Info ();
				Destroy (_multitap [0].gameObject);
			}
		}
		if (!Stage2_Controller._Stage2_Quest[7]) { //multi2 사용(왼쪽)
			if (_multitap [1].used_or_not_for_retry) {
				Stage2_Controller._Stage2_Quest[7] = true;
				Save_Script.Save_Quest_Info ();
				Destroy (_multitap [1].gameObject);
			}
		}

		if (((Stage2_Controller._Stage2_Quest [6] && !Stage2_Controller._Stage2_Quest [7]) ||
			(!Stage2_Controller._Stage2_Quest [6] && Stage2_Controller._Stage2_Quest [7])) && !a5a5) { //둘 중 하나만 했을 경우
			_multi_image [3].SetActive (false);
			_multi_image[0].SetActive(true);
			a5a5 = true;
		}

		if (Stage2_Controller._Stage2_Quest [6] && Stage2_Controller._Stage2_Quest [7] && !a6a6) {
			_multi_image [3].SetActive (false);
			_multi_image [0].SetActive (false);
			_multi_image[1].SetActive(true);
			a6a6 = true;
		}

		if (Stage2_Controller._Stage2_Quest[6] && Stage2_Controller._Stage2_Quest[7] && !Stage2_Controller._Stage2_Quest[8]) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
			aa.NPC_Say_yeah ("별감");
			Stage2_Controller._Stage2_Quest[8] = true;
			Save_Script.Save_Quest_Info ();
		}

		if (Stage2_Controller._Stage2_Quest[8] && !_mirror_use.enabled) {
			_mirror_use.enabled = true;
		}

		if (_mirror_use.GetComponent<Outline> ().used_or_not_for_retry) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");
			_sparkle.SetActive (true);
			_multi_image [0].SetActive (false);
			_multi_image [1].SetActive (false);
			_multi_image [2].SetActive(true);
			Stage2_Controller._Stage2_Quest[5] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q7_find_Remote(){
		if (a4a4) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");
			_coco_textbox = GameObject.Find ("코코_text");
			a4a4 = false;
		}
		if (!a4a4 && !_coco_textbox.activeSelf) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
			aa.NPC_Say_yeah ("별감");
			_remote.enabled = true;
			Stage2_Controller._Stage2_Quest[10] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q11_ClockWork(){
		if (_clockwork_ol.used_or_not_for_retry) {
			Stage2_Controller._Stage2_Quest_intArr[0]++;
			_cws.Play ();

			if (Stage2_Controller._Stage2_Quest[16]) {
				Stage2_Controller._Stage2_Quest[15] = true;
				Save_Script.Save_Quest_Info ();
			}

			//_sparkle_1.SetActive (false);
			//_clockwork.enabled = false;
			_clockwork_ol.used_or_not_for_retry = false;
		}
	}


	void Q16_ClockWork(){
		if (_clockwork_ol.used_or_not_for_retry) {
			Stage2_Controller._Stage2_Quest_intArr[2]++;
			_cws.Play ();

			if (Stage2_Controller._Stage2_Quest[22]) {
				Stage2_Controller._Stage2_Quest[21] = true;
				Save_Script.Save_Quest_Info ();
			}

			//_sparkle_1.SetActive (false);
			//_clockwork.enabled = false;
			_clockwork_ol.used_or_not_for_retry = false;
		}
	}
}
