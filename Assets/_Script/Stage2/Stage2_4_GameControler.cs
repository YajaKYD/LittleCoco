using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_4_GameControler : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

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


	public SpriteRenderer _whiteOut;
	public GameObject _endingText;
	public static bool whiteOut = false;

	private Text_Importer2 ti;


	void Awake(){
        googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[1]);
        googleAnalytics.LogScreen("Stage2_4");

        player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;

		sceneNo = 24;

	}

	void Start(){

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (24);

		if (Stage2_Controller.q[16]) {
			_mirror_use.SetActive (true);
			Destroy (_shadow);
			_bg.sprite = _day_bg;
			_orgel.sprite = _day_orgel1;
		}

		if (Stage2_Controller.q [18]) {
			_clockwork_ol.GetComponent<BoxCollider2D>().enabled=false;
		}

		if (Stage2_Controller.q[22]) {
			_bg.sprite = _moode_bg;
			_orgel.sprite = _night_orgel;
		}

		if (Stage2_Controller.q[19]) {
			//오르골 노래를 켠다.
		}

		if (Stage2_Controller.q[20]) {
			_moode_code [0].SetActive (false);
			_moode_code [1].SetActive (true);
		}

		if (Stage2_Controller.q[22]) {
			Item_Controller aa = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
			for (int i = 0; i < 5; i++) {
				if (aa._item_name_list [i] == "Clockwork") {
					aa._consumable [i] = true;
					break;
				}
			}
		}

		if (Stage2_Controller.q[22] && Stage2_Controller.q[19]) {
			_Last_wall.SetActive (false);
			Stage2_Controller.q [23] = true;
			if (!whiteOut) {
				StartCoroutine ("WhiteOut");
				whiteOut = true;
			}
		}

		if (Stage2_Controller.q [22] && Stage2_Controller.q [19] && !Stage2_Controller.q[24]) {
			//8th save point//
			Save_Script.Save_Now_Point ();
			print ("Saved");
			//8th save point//
		}

		if (Stage2_Controller.q[23]) {
			_mirror_use_last.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_star.enabled = true;
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player && !Stage2_Controller.q[3]) {
			b1b1 = true;
			_bg.sprite = _light_bg;
			_orgel.sprite = _light_orgel;
			Thunder_1.SetActive (true);
		}

		if (other.gameObject == player && !Stage2_Controller.q[13]) {
			_bg.sprite = _light_bg;
			_orgel.sprite = _light_orgel;
			Thunder_1.SetActive (true);
			b1b1 = true;
		}

		if (other.gameObject == player && !Stage2_Controller.q[15] && Stage2_Controller.q[13]) {
			a1a1 = true;
			Q9_Talk ();
		}

		if (other.gameObject == player && Stage2_Controller.q[16]) {
			Stage2_Controller.q[17] = true;
		}


	}

	void Update(){

		if (!Stage2_Controller.q[3] && b1b1) {
			Q4_Talk ();
		}

		if (Stage2_Controller.q[12] && !Stage2_Controller.q[13] && b1b1){
			Q9_Talk ();
		}

		if (!Stage2_Controller.q[18] && Stage2_Controller.q[16]) {
			Q13_ClockWork ();
		}

		if (!Stage2_Controller.q[20] && Stage2_Controller.q[16]) {
			Q15_turn_modelight ();
		}

		if (!Stage2_Controller.q[23] && Stage2_Controller.q[22]  && Stage2_Controller.q[19]) {
			Q18_using_mirror ();
		}

		if(Stage2_Controller.q[23] && !Stage2_Controller.q[24]){
			Q19_put_star ();
		}

	}

	void Q4_Talk(){
		if (a1a1) {
//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
//			aa.NPC_Say_yeah ("코코");
//			_coco_textbox = GameObject.Find ("코코_text");
			ti.Talk();
			a1a1 = false;
			//말 끝나고 27 트루로 함. 26까지가 기존. 27부터 새로 추가, 수정
			//Stage2_Controller._stage2_q3 = true;
		}

		if (!a1a1 && Stage2_Controller.q[27]) { //코코 말 끝내면
			mbr.enabled = false;
			StartCoroutine ("Backback");
			Stage2_Controller.q[3] = true;
		}

	}

	void Q9_Talk(){
		if (a1a1) {
//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			int temp = aa.currLineArr [0];
//			aa.currLineArr [0] = 6;
//			aa.NPC_Say_yeah ("코코");
//			aa.currLineArr [0] = temp +1;
//			_coco_textbox = GameObject.Find ("코코_text");

			ti.Talk (3);
			a1a1 = false;
			//Stage2_Controller._stage2_q3 = true;
		}

		if (!a1a1 && Stage2_Controller.q[30]) { //코코 말 끝내면
			mbr.enabled = false;
			StartCoroutine ("Backback");
			Stage2_Controller.q[13] = true;
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
			_cws.Play ();
			_clockwork_ol.used_or_not_for_retry = false;

			if (Stage2_Controller._Stage2_Quest_intArr[1] % 2 == 0) {
				if (!Stage2_Controller.q [22]) {
					_orgel.sprite = _day_orgel1;
				}
			} else {
				if (!Stage2_Controller.q [22]) {
					_orgel.sprite = _day_orgel2;
				}
			}

			if (Stage2_Controller._Stage2_Quest_intArr[1] == 1) {
//				_OrgelSound.SetActive (true);
//				_OrgelSound.GetComponent<AudioSource> ().volume = 0.4f;
//				_OrgelSound.transform.SetParent (GameObject.FindWithTag ("Controller").transform);
				ti.Talk(5);

				_clockwork_ol.GetComponent<BoxCollider2D>().enabled=false;
				GameObject _orgelsound = GameObject.FindWithTag("Controller").transform.GetChild(2).gameObject;
				_orgelsound.SetActive(true);
				_orgelsound.GetComponent<AudioSource> ().volume = 0.4f;


				Item_Controller aa = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
				for (int i = 0; i < 5; i++) {
					if (aa._item_name_list [i] == "remotecon") {
						aa._consumable [i] = true;
						break;
					}
				}
				Stage2_Controller.q[18] = true;
			}
		}
	}

	void Q15_turn_modelight(){
		if (_mirror_use.GetComponent<Outline> ().used_or_not_for_retry) {
			ti.Talk (9);

			_moode_code [0].SetActive (false);
			_moode_code [1].SetActive (true);
			_sparkle.SetActive (true);
			Stage2_Controller.q[20] = true;
		}
	}

	void Q18_using_mirror(){
		if (_mirror_use_last.GetComponent<Outline> ().used_or_not_for_retry) {

			_sparkle1.SetActive (true);
			_mirror_use_last.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_mirror_use_last.GetComponent<SpriteRenderer> ().sprite = iVon;
			_star.enabled = true;
			Stage2_Controller.q[23] = true;
		}
	}

	void Q19_put_star(){
		if (_star.gameObject.GetComponent<Outline> ().used_or_not_for_retry) {
			
			_star.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
			_star.gameObject.GetComponent<SpriteRenderer> ().sprite = sTar;
			_dogdog.enabled = true;
			Stage2_Controller.q[24] = true;
		}
	}

	IEnumerator WhiteOut(){
		GameObject.FindWithTag("Item_Canvas").GetComponent<Canvas> ().enabled = false;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = false;
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		yield return new WaitForSeconds (1f);

		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _whiteOut.color;
			c.a = f;
			_whiteOut.color = c;
			yield return null;
		}
		yield return new WaitForSeconds (1f);
//		for (int i = 0; i < _yellowThings.Length; i++) {
//			_yellowThings [i].SetActive (true);
//		}
		PopUpEndingText ();
		StartCoroutine ("WhiteIn");
	}

	IEnumerator WhiteIn(){

		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			Color c = _whiteOut.color;
			c.a = f;
			_whiteOut.color = c;
			yield return null;
		}

		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;

		GameObject.FindWithTag("Item_Canvas").GetComponent<Canvas> ().enabled = true;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = true;
		//글자띄우고.
	}

	void PopUpEndingText(){
		_endingText.SetActive (true);
		Invoke ("DestroyEndingText", 3f);
	}

	void DestroyEndingText(){
		Destroy (_endingText);
	}
}
