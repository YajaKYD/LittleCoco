using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_1_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;

	private bool q1a1 = false;
	private bool q1a2 = false;
	private bool q2a1 = false;
	private bool q2a2 = false;
	private bool q2a3 = false;
	private bool q2a4 = false;
	private bool q3a1 = false;
	private bool q3a2 = false;
	private bool q3a3 = false;
	private bool q3a4 = false;
	private bool q3a5 = false;
	private bool q3a6 = false;

	public SpriteRenderer _blackout; 
	public SpriteRenderer _bg;
	public GameObject _ivon;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;

		player.transform.position = start_pos.position;

		if (!Stage5_Controller._Stage5_Quest[0]) {
			mbr.enabled = false;
			print ("엎드려있는ani");
		}
	}

	void Start(){
//		if (GetComponent<Load_data> ()._where_are_you_from == 10) {
//			player.transform.position = regen_pos.position;
//		}
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];

		if (Stage5_Controller._Stage5_Quest[3]) {//after 1st scene end
			_blackout.color = new Color (0,0,0,0);
			_bg.color = new Color (1, 1, 1, 1);
			GetComponent<BoxCollider2D> ().enabled = false;
		}

	}

	void Update(){
		if (!Stage5_Controller._Stage5_Quest[0] && _blackout.color.a <= 0) {
			Q1_starsay1 ();
		}
		if (!Stage5_Controller._Stage5_Quest[2] && Stage5_Controller._Stage5_Quest[1]) {
			Q2_Until_fadeout ();
		}
		if (!Stage5_Controller._Stage5_Quest[3] && Stage5_Controller._Stage5_Quest[2]) {
			Q3_fadein_and_coco ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			mbr.enabled = false;
			//Stage5_Controller.q2 = true;
			Stage5_Controller._Stage5_Quest [1] = true;
		}
	}

	void Q1_starsay1(){
		if (!q1a1 && !_star_textbox.activeSelf) {
			ti.currLineArr [0] = 0;
			ti.NPC_Say_yeah ("별감");
			q1a1 = true;
		}
		if (q1a1 && !q1a2 && !_star_textbox.activeSelf) {
			mbr.enabled = false;
			StartCoroutine (Delay_2sec ());
			q1a2 = true;
		}
	}

	void Q2_Until_fadeout(){
		if (!q2a1) {
			StartCoroutine (Open_Door ());
			q2a1 = true;
		}
		if (q2a1 && q2a2 && q2a3 && !q2a4) {
			ti.currLineArr [1] = 0;
			ti.NPC_Say_yeah ("이본");
			print ("코코 ani stop");
			q2a4 = true;
		}
		if (q2a4 && !_ivon_textbox.activeSelf) {
			mbr.enabled = false;
			StartCoroutine (Fadeout_black ());
			//Stage5_Controller.q3 = true;
			Stage5_Controller._Stage5_Quest [2] = true;
		}
	}

	void Q3_fadein_and_coco(){
		if (!q3a1) {
			StartCoroutine (Fadein_black ());
			q3a1 = true;
		}
		if (q3a1 && q3a2 && !q3a3) {
			ti.currLineArr [1] = 2;
			ti.NPC_Say_yeah ("이본");
			q3a3 = true;
		}
		if (q3a3 && !q3a4 && !_ivon_textbox.activeSelf) {
			mbr.enabled = false;
			print ("idle animation");
			StartCoroutine (idle_2sec ());
			q3a4 = true;
		}
		if (q3a4 && q3a5 && !q3a6) {
			ti.currLineArr [1] = 4;
			ti.NPC_Say_yeah ("이본");
			q3a6 = true;
		}
		if (q3a6 && !_ivon_textbox.activeSelf) {
			//Stage5_Controller.q4 = true;
			Stage5_Controller._Stage5_Quest [3] = true;
		}

	}

	IEnumerator Delay_2sec(){
		while (true) {
			yield return new WaitForSeconds (2f);
			print ("일어남");
			yield return new WaitForSeconds (1f);
			ti.currLineArr [0] = 6;
			ti.NPC_Say_yeah ("별감");
			break;
		}
		//Stage5_Controller.q1 = true;
		Stage5_Controller._Stage5_Quest [0] = true;
	}

	IEnumerator Open_Door(){
		while (true) {
			print ("Open_Sound");
			yield return new WaitForSeconds (2f); //fit to sound length
			print ("앞발들고 선다");
			yield return new WaitForSeconds (1f);
			_ivon.SetActive (true);
			print ("Switch sound");
			StartCoroutine (Fadeout_bg ());
			break;
		}
		q2a2 = true;
	}

	IEnumerator Fadeout_bg(){
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _bg.color;
			c.a = f;
			_bg.color = c;
			yield return null;
		}
		_bg.color = new Color (1, 1, 1, 1);
		q2a3 = true;
	}

	IEnumerator Fadeout_black(){
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 1);
	}

	IEnumerator Fadein_black(){
		while (true) {
			yield return new WaitForSeconds (2f);
			_ivon.SetActive (false);
			GetComponent<BoxCollider2D> ().enabled = false;
			print ("배경 정리됨");
			break;
		}
		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			Color bb = new Color (0, 0, 0, 1);
			bb.a = f;
			_blackout.color = bb;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 0);
		q3a2 = true;
	}

	IEnumerator idle_2sec(){
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		q3a5 = true;
	}
}
