using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_3_GameController : Controller {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private GameObject portal_to_2_4;
	private Moving_by_RLbuttons mbr;

	public Outline _Audio;
	//public GameObject _classical;
	public GameObject _sparkle;
	public GameObject _Background_to_turn;
	public BoxCollider2D _switch;
	public GameObject _SoundEffect;
	public GameObject _LightFromSide;
	public AudioSource _2_3_thunder;

	private bool a1a1 = true;
	private bool a2a2 = false;

	private bool temp_to_saymunch = false;
	private GameObject cocobox;
	private int temp;

	private Item_Controller ic;
	private bool drop_tape = false;

	private Text_Importer2 ti;


	//public GameObject _coco_textbox;

	void Awake(){
		player = GameObject.Find ("Player");
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		portal_to_2_4 = GameObject.Find ("Portal_to_#2-4");

		sceneNo = 23;

	}

	void Start(){

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (23);

		if (Stage2_Controller.q [3] && !Stage2_Controller.q [4]) {
			//4th save point//
			Save_Script.Save_Now_Point ();
			print ("Saved");
			//4th save point//
		}

		if (GetComponent<Load_data> ()._where_are_you_from == 14) {
			player.transform.position = regen_pos.position;
		}

		if (Stage2_Controller.q[3] && !Stage2_Controller.q[29]) {
			portal_to_2_4.SetActive (false);
			GetComponent<BoxCollider2D> ().enabled = true;
			ti.Talk (3);
		}

		if (Stage2_Controller.q [3]) {
			portal_to_2_4.SetActive (false);
			GetComponent<BoxCollider2D> ().enabled = true;
		}

		if (!Stage2_Controller.q[11]) {
		//	Destroy (_classical);
		}

		if (Stage2_Controller.q[12]) {
			_SoundEffect.SetActive (true);
			portal_to_2_4.SetActive (true);
			GetComponent<BoxCollider2D> ().enabled = false;
		//	Destroy (_classical);//노래가 계속 붙어있으면 너무 로딩이 느림
		}

		if (Stage2_Controller.q [13] && !Stage2_Controller.q [31]) {
			ti.Talk (7);
		}

		if (Stage2_Controller.q[13] && !Stage2_Controller.q[15]) {
			_sparkle.SetActive (true);
			_Background_to_turn.transform.Rotate (0f, 0f, Stage2_Controller._Stage2_Quest_intArr[0] * (-90f));

			if (Stage2_Controller._Stage2_Quest_intArr[0] % 4 == 3) {
				portal_to_2_4.SetActive (false);
				_switch.enabled = true;
			} else {
				portal_to_2_4.SetActive (false);
				_switch.enabled = false;
			}
		}

		if (Stage2_Controller.q[15] && Stage2_Controller.q[16]) {
			portal_to_2_4.SetActive (true);
		}

		if (Stage2_Controller.q [16]) { //switch 켜서 불빛 나옴
			_LightFromSide.SetActive (true);
		}
		if (Stage2_Controller.q [22]) { //switch 꺼서 불빛 꺼짐
			_LightFromSide.SetActive (false);
		}


		if (Stage2_Controller.q [19]) {
			_SoundEffect.SetActive (false);
		}

		if (Stage2_Controller.q[17] && Stage2_Controller.q[20] && !Stage2_Controller.q[21]) {
			_sparkle.SetActive (true);
			_Background_to_turn.transform.Rotate (0f, 0f, Stage2_Controller._Stage2_Quest_intArr[2] * (-90f));

			if (Stage2_Controller._Stage2_Quest_intArr[2] % 4 == 3) {
				portal_to_2_4.SetActive (false);
				_switch.enabled = true;
			} else if (Stage2_Controller._Stage2_Quest_intArr[2] % 4 == 0) {
				portal_to_2_4.SetActive (true);
				_switch.enabled = false;
			} else {
				portal_to_2_4.SetActive (false);
				_switch.enabled = false;
			}
		}

	}

	void Update(){
		if (Stage2_Controller.q[1] && !Stage2_Controller.q[2]) {
			Q3_Talk ();
		}

		if (Stage2_Controller.q[5] && !Stage2_Controller.q[9]) {
			Q6_1_Say_munch ();
		}

		if (!Stage2_Controller.q[12]) {
			Q8_Turn_it_on ();
		}

//		if (!Stage2_Controller.q[14] && Stage2_Controller.q[13]) {
//			Q10_Talk_bySpeaker ();
//		}

		if (Stage2_Controller.q[18] && !Stage2_Controller.q[19]) {
			Q14_Turn_off_Audio ();
		}

//		if (temp_to_saymunch) {
//			if (!cocobox.activeSelf) {
//				Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//				aa.currLineArr [0] = temp;
//			}
//		}

	}

	void Q3_Talk(){
		if (a1a1) {
			//여기에 천둥소리 약하게 하나 넣어야 함.
			_2_3_thunder.Play();
			ti.Talk ();
			a1a1 = false;
			Stage2_Controller.q[2] = true;
		}
	}

	void Q6_1_Say_munch(){
		if (a2a2) {
			Stage2_Controller.q[9] = true;
		}
	}

	void Q8_Turn_it_on(){
		if (_Audio.used_or_not_for_retry) {
			_SoundEffect.SetActive (true);
		//	_classical.SetActive (true);
		//	_classical.transform.SetParent (GameObject.Find ("Stage2_Controller").transform);
			GameObject _rain = GameObject.FindWithTag("Controller").transform.GetChild(0).gameObject;
			GameObject _classic = GameObject.FindWithTag("Controller").transform.GetChild(1).gameObject;
			_rain.SetActive (false);
			_classic.SetActive(true);

			portal_to_2_4.SetActive (true);
			GetComponent<BoxCollider2D> ().enabled = false;
			Stage2_Controller.q[12] = true;

//			//6th save point//
//			Save_Script.Save_Now_Point ();
//			print ("Saved");
//			//6th save point//
		}
	}

//	void Q10_Talk_bySpeaker(){
//		if (Input.GetMouseButtonDown (0)) {
//			Vector2 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			Ray2D ray = new Ray2D (wp, Vector2.zero);
//			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);
//
//			if (hit.collider != null) {
//				if (hit.collider.CompareTag ("Audio")) {
//					Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//					//aa.currLineArr [0] += 2;//코코 다음대사 치게함.
//					//aa.NPC_Say_yeah ("코코");
//					Stage2_Controller.q[14] = true;
//				}
//			}
//		}
//	}

	void Q14_Turn_off_Audio(){
		if (_Audio.used_or_not_for_retry) {
			//Destroy (GameObject.Find ("Classical"));
			GameObject _audiosound = GameObject.FindWithTag("Controller").transform.GetChild(1).gameObject;
			_audiosound.SetActive(false);

			_SoundEffect.SetActive (false);
			//GameObject.FindWithTag ("Controller").GetComponentInChildren<AudioSource> ().volume = 1f;
			GameObject _orgelsound = GameObject.FindWithTag("Controller").transform.GetChild(2).gameObject;
			_orgelsound.GetComponent<AudioSource> ().volume = 1f;

			Stage2_Controller.q[19] = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player) {
			//말하고 뒤로 자동으로 움직임?
			mbr.enabled = false;
			StartCoroutine ("Backback");
			if (Stage2_Controller.q[5] && !Stage2_Controller.q[9]) {
				//6_1q 하기위해서
				a2a2 = true;
			}
		}
	}

	IEnumerator Backback(){
		for (int i = 0; i < 20; i++) {
			mbr.Moving_Right (8f);
			yield return null;
		}
		ti.Talk (1);
		temp_to_saymunch = true;
	}
}
