using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_4_GameController : MonoBehaviour {

	public Transform from_5_5;
	public Transform from_5_6;
	public Transform from_5_7;
	public Transform from_5_8;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	public SpriteRenderer _blackout; 

	private bool q1a1 = false;
	private bool q1a2 = false;
	private bool q1a3 = false;
	private bool q1a4 = false;
	private bool q1a5 = false;
	private bool q1a6 = false;
	private bool q2a1 = false;
	private bool q2a2 = false;
	private bool q2a3 = false;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		//player.transform.position = start_pos.position;
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 37) {
			player.transform.position = from_5_5.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 38) {
			player.transform.position = from_5_6.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 39) {
			player.transform.position = from_5_7.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 40) {
			player.transform.position = from_5_8.position;
		}

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];

	}

	void Update(){
		if (Stage5_Controller._Stage5_Quest [25] && !Stage5_Controller._Stage5_Quest [26]) {
			Q1_gotoPaper ();
		}
		if (Stage5_Controller._Stage5_Quest [27] && !Stage5_Controller._Stage5_Quest [28]) {
			Q2_Fadeout ();
		}
		if (Stage5_Controller._Stage5_Quest [28] && !Stage5_Controller._Stage5_Quest [29]) {
			
		}
	}

	void Q1_gotoPaper(){
		if (!q1a1) {
			ti.currLineArr [0] = 59;//한적
			ti.NPC_Say_yeah("별감");
			q1a1 = true;
			StartCoroutine (Coco_ddong ());
		}
		if (q1a2 && !q1a3 && !_star_textbox.activeSelf) {
			ti.currLineArr [0] = 61;//얼른
			ti.NPC_Say_yeah("별감");
			q1a3 = true;
		}
		if (q1a3 && q1a6 && !q1a5 && !_star_textbox.activeSelf) {
			ti.currLineArr [0] = 63;//덮을거
			ti.NPC_Say_yeah("별감");
			q1a5 = true;
			Stage5_Controller._Stage5_Quest [26] = true;
		}
	}

	void Q2_Fadeout(){
		if (!q2a1) {
			ti.currLineArr [0] = 65;//조심해
			ti.NPC_Say_yeah("별감");
			q2a1 = true;
		}
		if (q2a1 && !_star_textbox.activeSelf) {
			print ("뭉치덮침");
			StartCoroutine (Fadeout_black ());
			Stage5_Controller._Stage5_Quest [28] = true;
			//save//
			Save_Script.Save_Now_Point ();
			//save//
		}
	}

	void Q3_FadeIn(){
		StartCoroutine (Fadein_black ());
	}

	void Q4_getPaper(){
	}

	void Q5_go5_3(){
	}

	IEnumerator Coco_ddong(){
		mbr.enabled = false;
		print ("CoCo Ani Full");
		while (true) {
			yield return new WaitForSeconds (2f);//ready
			q1a2 = true;
			yield return new WaitForSeconds (3f);//shot
			GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/dogddong"));
			k.transform.position = player.transform.position;
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D> (), k.GetComponent<Collider2D> (), true);
			q1a4 = true;			
			yield return new WaitForSeconds (3f);//after
			q1a6 = true;
			break;
		}
		mbr.enabled = true;
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
			print ("배경 바뀜, 가판대");
			break;
		}
		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			Color bb = new Color (0, 0, 0, 1);
			bb.a = f;
			_blackout.color = bb;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 0);
	}
}
