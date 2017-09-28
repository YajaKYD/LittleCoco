using System.Collections.Generic;
using UnityEngine;

public class Stage2_2_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

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
	public Transform clockwork;
	public GameObject portalto1;
	public GameObject portalto2;

	private Text_Importer2 ti;

	void Awake(){
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage2_2");

        player = GameObject.Find ("Player");
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;

		sceneNo = 22;

	}

	void Start(){

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (22);

		if (GetComponent<Load_data> ()._where_are_you_from == 13) {
			player.transform.position = regen_pos.position;
		}

		if (Stage2_Controller.q[6]) { //multi1 사용
			_multitap [0].used_or_not_for_retry = true;
			Destroy (_multitap [0].gameObject);
		}
		if (Stage2_Controller.q[7]) { //multi2 사용
			_multitap [1].used_or_not_for_retry = true;
			Destroy (_multitap [1].gameObject);
		}

		if ((Stage2_Controller.q [6] && !Stage2_Controller.q [7]) ||
			(!Stage2_Controller.q [6] && Stage2_Controller.q [7])) { //둘 중 하나만 했을 경우
			_multi_image[0].SetActive(true);
			_multi_image [3].SetActive (false);
		}

		if (Stage2_Controller.q [6] && Stage2_Controller.q [7]) {
			_multi_image [3].SetActive (false);
			_multi_image [0].SetActive (false);
			_multi_image[1].SetActive(true);
		}

		if (Stage2_Controller.q[5]) {
			_multi_image [0].SetActive (false);
			_multi_image [1].SetActive (false);
			_multi_image [2].SetActive(true);
			_mirror_use.enabled = false;
		}

		if (Stage2_Controller.q[10] && !Stage2_Controller.q[11]) {
			_remote.enabled = true;
		}

		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "remotecon") {
				Destroy (_remote.gameObject);
			}
		}

		if (Stage2_Controller.q[11]) {
			Destroy (_remote.gameObject);
		}

		if (Stage2_Controller.q[13] && !Stage2_Controller.q[15]) {
			_sparkle_1.SetActive (true);
			_clockwork.enabled = true;
		}

		if (!Stage2_Controller.q [15]) {
			clockwork.rotation = Quaternion.Euler (new Vector3( 0, 0, Stage2_Controller._Stage2_Quest_intArr [0] * 90f));
		}

		if (!Stage2_Controller.q [21]) {
			clockwork.rotation = Quaternion.Euler (new Vector3( 0, 0, Stage2_Controller._Stage2_Quest_intArr [2] * 90f));
		}

		if (Stage2_Controller.q[20]) {
			_sparkle_1.SetActive (true);
			_clockwork.enabled = true;
		}

		if (Stage2_Controller.q[22]) {
			//_sparkle_1.SetActive (false);
			//_clockwork.enabled = false;
			//이거어디야?
		}

		if (Stage2_Controller.q [31] && !Stage2_Controller.q [32]) {
			ti.Talk (13);
		}

		if (Stage2_Controller.q[21]) {
			_sparkle_1.SetActive (false);
		}

	}

	void Update(){

		if (!Stage2_Controller.q[1]) {
			Q2_Talk ();
		}

		if (Stage2_Controller.q[4] && !Stage2_Controller.q[5]) {
			Q6_use_multiTap ();
		}

		if (GetComponent<Load_data> ()._where_are_you_from == 13 && Stage2_Controller.q[5] && Stage2_Controller.q[9] && !Stage2_Controller.q[10]) {
			//2-3에 다녀와야 함. >> 6_1도 true되어야 함.
			Q7_find_Remote ();
		}

		if (Stage2_Controller.q[10] && !Stage2_Controller.q[11]) {
			if (_remote == null) {
				//print ("SS");
				Stage2_Controller.q[11] = true;
			}
		}

		if (Stage2_Controller.q[13] && !Stage2_Controller.q[15]) {
			Q11_ClockWork ();
		}

		if (Stage2_Controller.q[20] && !Stage2_Controller.q[21]) {
			Q16_ClockWork ();
		}
			
		//아템떨궈야하는디~ 한번만~
		if(Stage2_Controller.q[6] && Stage2_Controller.q[7] && !Stage2_Controller.q[25] && Item_Drag._NOW_Shaked && !drop_tape){
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

			Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag> ();
			for (int x = 0; x < ids.Length; x++) {
				ids [x]._diary_usable = false;
			} //change diary image -unusable-
			Item_Drag._NOW_Shaked = false;
			drop_tape = true;
		}
			
		if(drop_tape && !Stage2_Controller.q[25]){
			//떨어트림
			//먹었음.
			for (int i = 0; i < ic._item_list.Length; i++) {
				if (ic._item_name_list [i] == "Tape") {
					portalto1.SetActive (true);
					portalto2.SetActive (true);
					Stage2_Controller.q [25] = true;
				}
			}
		}
	}

	void Q2_Talk(){
		if (a1a1) {
			ti.Talk ();
			a1a1 = false;
		}
	}

	void Q6_use_multiTap(){

		if (!Stage2_Controller.q[6]) { //multi1 사용
			if (_multitap [0].used_or_not_for_retry) {
				Stage2_Controller.q[6] = true;
				Destroy (_multitap [0].gameObject);
			}
		}
		if (!Stage2_Controller.q[7]) { //multi2 사용(왼쪽)
			if (_multitap [1].used_or_not_for_retry) {
				Stage2_Controller.q[7] = true;
				Destroy (_multitap [1].gameObject);
			}
		}

		if (((Stage2_Controller.q [6] && !Stage2_Controller.q [7]) ||
			(!Stage2_Controller.q [6] && Stage2_Controller.q [7])) && !a5a5) { //둘 중 하나만 했을 경우
			_multi_image [3].SetActive (false);
			_multi_image[0].SetActive(true);
			a5a5 = true;
		}

		if (Stage2_Controller.q [6] && Stage2_Controller.q [7] && !a6a6) {
			_multi_image [3].SetActive (false);
			_multi_image [0].SetActive (false);
			_multi_image[1].SetActive(true);
			a6a6 = true;
		}

		if (Stage2_Controller.q[6] && Stage2_Controller.q[7] && !Stage2_Controller.q[8]) {
//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
//			aa.NPC_Say_yeah ("별감");
			ti.Talk(4);
			portalto1.SetActive (false);
			portalto2.SetActive (false);
			Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag> ();
			for (int k = 0; k < ids.Length; k++) {
				ids [k]._diary_usable = true;
			} //change diary image -usable-
			Stage2_Controller.q[8] = true;
		}

		if (Stage2_Controller.q[8] && !_mirror_use.enabled) {
			_mirror_use.enabled = true;
		}

		if (_mirror_use.GetComponent<Outline> ().used_or_not_for_retry) {
			ti.Talk (8);
			_sparkle.SetActive (true);
			_multi_image [0].SetActive (false);
			_multi_image [1].SetActive (false);
			_multi_image [2].SetActive(true);
			Stage2_Controller.q[5] = true;

			//5th save point//
			Save_Script.Save_Now_Point ();
			print ("Saved");
			//5th save point//
		}
	}

	void Q7_find_Remote(){
		ti.Talk (10);
		_remote.enabled = true;
		Stage2_Controller.q[10] = true;
	}

	void Q11_ClockWork(){
		if (_clockwork_ol.used_or_not_for_retry) {
			Stage2_Controller._Stage2_Quest_intArr[0]++;
			clockwork.rotation = Quaternion.Euler (new Vector3( 0, 0, Stage2_Controller._Stage2_Quest_intArr [0] * 90f));
			_cws.Play ();

			if (Stage2_Controller.q[16]) {
				Stage2_Controller.q[15] = true;

				//7th save point//
				Save_Script.Save_Now_Point ();
				print ("Saved");
				//7th save point//
			}

			//_sparkle_1.SetActive (false);
			//_clockwork.enabled = false;
			_clockwork_ol.used_or_not_for_retry = false;
		}
	}


	void Q16_ClockWork(){
		if (_clockwork_ol.used_or_not_for_retry) {
			Stage2_Controller._Stage2_Quest_intArr[2]++;
			clockwork.rotation = Quaternion.Euler (new Vector3( 0, 0, Stage2_Controller._Stage2_Quest_intArr [2] * 90f));
			_cws.Play ();

			if (Stage2_Controller.q[22]) {
				_sparkle_1.SetActive (false);
				Stage2_Controller.q[21] = true;
			}

			//_sparkle_1.SetActive (false);
			//_clockwork.enabled = false;
			_clockwork_ol.used_or_not_for_retry = false;
		}
	}
}
