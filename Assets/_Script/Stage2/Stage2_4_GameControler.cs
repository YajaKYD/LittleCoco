using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_4_GameControler : MonoBehaviour {

	private Transform start_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	public GameObject Thunder_1;

	private bool a1a1 = true;
	private bool b1b1 = false;
	//public bool a2a2 = true;

	public Outline _clockwork_ol;
	public AudioSource _cws;
	public GameObject _mirror_use;
	public GameObject _sparkle;
	public GameObject _Last_wall;
	public GameObject _mirror_use_last;
	public GameObject _sparkle1;
	public BoxCollider2D _star;
	public BoxCollider2D _dogdog;

	public GameObject _shadow;
	public SpriteRenderer _bg;
	public SpriteRenderer _orgel;
	public Sprite _night_bg;
	public Sprite _night_orgel;
	public Sprite _day_bg;
	public Sprite _day_orgel1;
	public Sprite _day_orgel2;
	public Sprite _light_bg;
	public Sprite _light_orgel;
	public Sprite _moode_bg;
	//public GameObject _OrgelSound;

	public GameObject _coco_textbox;
	public GameObject[] _moode_code;
	public Sprite iVon;
	public Sprite sTar;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
	}

	void Start(){
		if (Stage2_Controller._Stage2_Quest[16]) {
			_mirror_use.SetActive (true);
			Destroy (_shadow);
			_bg.sprite = _day_bg;
			_orgel.sprite = _day_orgel1;
		}

		if (Stage2_Controller._Stage2_Quest [18]) {
			_clockwork_ol.GetComponent<BoxCollider2D>().enabled=false;
		}

		if (Stage2_Controller._Stage2_Quest[22]) {
			_bg.sprite = _moode_bg;
			_orgel.sprite = _night_orgel;
		}

		if (Stage2_Controller._Stage2_Quest[19]) {
			//오르골 노래를 켠다.
		}

		if (Stage2_Controller._Stage2_Quest[20]) {
			_moode_code [0].SetActive (false);
			_moode_code [1].SetActive (true);
		}

		if (Stage2_Controller._Stage2_Quest[22] && Stage2_Controller._Stage2_Quest[19]) {
			_Last_wall.SetActive (false);
		}

		if (Stage2_Controller._Stage2_Quest[23]) {
			_mirror_use_last.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_star.enabled = true;
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player && !Stage2_Controller._Stage2_Quest[3]) {
			b1b1 = true;
			_bg.sprite = _light_bg;
			_orgel.sprite = _light_orgel;
			Thunder_1.SetActive (true);
		}

		if (other.gameObject == player && !Stage2_Controller._Stage2_Quest[13]) {
			_bg.sprite = _light_bg;
			_orgel.sprite = _light_orgel;
			Thunder_1.SetActive (true);
			b1b1 = true;
		}

		if (other.gameObject == player && !Stage2_Controller._Stage2_Quest[15] && Stage2_Controller._Stage2_Quest[13]) {
			a1a1 = true;
			Q9_Talk ();
		}

		if (other.gameObject == player && Stage2_Controller._Stage2_Quest[16]) {
			Stage2_Controller._Stage2_Quest[17] = true;
		}


	}

	void Update(){

		if (!Stage2_Controller._Stage2_Quest[3] && b1b1) {
			Q4_Talk ();
		}

		if (Stage2_Controller._Stage2_Quest[12] && !Stage2_Controller._Stage2_Quest[13] && b1b1){
			Q9_Talk ();
		}

		if (!Stage2_Controller._Stage2_Quest[18] && Stage2_Controller._Stage2_Quest[16]) {
			Q13_ClockWork ();
		}

		if (!Stage2_Controller._Stage2_Quest[20] && Stage2_Controller._Stage2_Quest[16]) {
			Q15_turn_modelight ();
		}

		if (!Stage2_Controller._Stage2_Quest[23] && Stage2_Controller._Stage2_Quest[22]  && Stage2_Controller._Stage2_Quest[19]) {
			Q18_using_mirror ();
		}

		if(Stage2_Controller._Stage2_Quest[23] && !Stage2_Controller._Stage2_Quest[24]){
			Q19_put_star ();
		}

	}

	void Q4_Talk(){
		if (a1a1) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");
			_coco_textbox = GameObject.Find ("코코_text");
			a1a1 = false;
			//Stage2_Controller._stage2_q3 = true;
		}

		if (!a1a1 && !_coco_textbox.activeSelf) { //코코 말 끝내면
			mbr.enabled = false;
			StartCoroutine ("Backback");
			Stage2_Controller._Stage2_Quest[3] = true;
			Save_Script.Save_Quest_Info ();
		}

	}

	void Q9_Talk(){
		if (a1a1) {
			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
			int temp = aa.currLineArr [0];
			aa.currLineArr [0] = 6;
			aa.NPC_Say_yeah ("코코");
			aa.currLineArr [0] = temp +1;
			_coco_textbox = GameObject.Find ("코코_text");
			a1a1 = false;
			//Stage2_Controller._stage2_q3 = true;
		}

		if (!a1a1 && !_coco_textbox.activeSelf) { //코코 말 끝내면
			mbr.enabled = false;
			StartCoroutine ("Backback");
			Stage2_Controller._Stage2_Quest[13] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	IEnumerator Backback(){
		for (int i = 0; i < 35; i++) {
			mbr.Moving_Right (8f);
			yield return null;
		}
		mbr.enabled = true;
	}

	void Q13_ClockWork(){
		if (_clockwork_ol.used_or_not_for_retry) {
			Stage2_Controller._Stage2_Quest_intArr[1]++;
			Save_Script.Save_Quest_Info ();
			_cws.Play ();
			_clockwork_ol.used_or_not_for_retry = false;

			if (Stage2_Controller._Stage2_Quest_intArr[1] % 2 == 0) {
				_orgel.sprite = _day_orgel1;
			} else {
				_orgel.sprite = _day_orgel2;
			}

			if (Stage2_Controller._Stage2_Quest_intArr[1] == 3) {
//				_OrgelSound.SetActive (true);
//				_OrgelSound.GetComponent<AudioSource> ().volume = 0.4f;
//				_OrgelSound.transform.SetParent (GameObject.FindWithTag ("Controller").transform);
				_clockwork_ol.GetComponent<BoxCollider2D>().enabled=false;
				GameObject _orgelsound = GameObject.FindWithTag("Controller").transform.GetChild(2).gameObject;
				_orgelsound.SetActive(true);
				_orgelsound.GetComponent<AudioSource> ().volume = 0.4f;


				Item_Controller aa = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
				for (int i = 0; i < 5; i++) {
					if (aa._item_name_list [i] == "remotecon") {
						aa._consumable [i] = true;
						Save_Script.Save_Item_Info ();
						break;
					}
				}
				Stage2_Controller._Stage2_Quest[18] = true;
				Save_Script.Save_Quest_Info ();
			}
		}
	}

	void Q15_turn_modelight(){
		if (_mirror_use.GetComponent<Outline> ().used_or_not_for_retry) {
			_moode_code [0].SetActive (false);
			_moode_code [1].SetActive (true);
			_sparkle.SetActive (true);
			Stage2_Controller._Stage2_Quest[20] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q18_using_mirror(){
		if (_mirror_use_last.GetComponent<Outline> ().used_or_not_for_retry) {

			_sparkle1.SetActive (true);
			_mirror_use_last.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_mirror_use_last.GetComponent<SpriteRenderer> ().sprite = iVon;
			_star.enabled = true;
			Stage2_Controller._Stage2_Quest[23] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q19_put_star(){
		if (_star.gameObject.GetComponent<Outline> ().used_or_not_for_retry) {
			
			_star.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_star.gameObject.GetComponent<SpriteRenderer> ().sprite = sTar;
			_dogdog.enabled = true;
			Stage2_Controller._Stage2_Quest[24] = true;
			Save_Script.Save_Quest_Info ();
		}
	}
}
