using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_2_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	public GameObject _ivon;
	public BoxCollider2D bed;
	public BoxCollider2D door;
	public BoxCollider2D snack;
	public SpriteRenderer _blackout;
	public GameObject _dogsnack;
	public Transform sleep_pos;
	public BoxCollider2D gooutportal;
	//temp
	public GameObject _stardoll; 
	//
	private bool q4_a1 = false;
	private bool q5_a1 = false;
	private bool q5_a2 = false;
	private bool q7_a1 = false;
	private bool q8_a1 = false;
	private bool q9_a1 = false;
	private bool q9_a2 = false;

	private bool tr1 = false;
	private bool tr2 = false;
	private bool tr3 = false;
	private bool tr4 = false;
	private bool tr5 = false;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;
	}

	void Start(){
		//		if (GetComponent<Load_data> ()._where_are_you_from == 10) {
		//			player.transform.position = regen_pos.position;
		//		}
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];

		if (Stage5_Controller._Stage5_Quest[3] && !Stage5_Controller._Stage5_Quest[4]) {
			Save_Script.Save_Now_Point ();
		}
		if (Stage5_Controller._Stage5_Quest [6]) {
			_ivon.SetActive (false);
		}
		if (Stage5_Controller._Stage5_Quest[7]) {
			Destroy (_stardoll);
		}
		if (Stage5_Controller._Stage5_Quest [8] && !Stage5_Controller._Stage5_Quest [9]) {
			
		}

		if (Stage5_Controller._Stage5_Quest[11] && !Stage5_Controller._Stage5_Quest[12]) {//savepoint
			player.transform.position = sleep_pos.position;
			StartCoroutine (Fadein_black ());
			bed.enabled = false;
			door.enabled = true;
		}

		if (Stage5_Controller._Stage5_Quest [12] && !Stage5_Controller._Stage5_Quest [13]) {
			bed.enabled = false;
			door.enabled = true;
		}

		if (Stage5_Controller._Stage5_Quest [24]) {
			gooutportal.enabled = true;
		}

	}

	void Update () {

		if (!Stage5_Controller._Stage5_Quest[4]) {
			Q4_move_to_bed ();
		}
		if (Stage5_Controller._Stage5_Quest[5] && !Stage5_Controller._Stage5_Quest[6]) {
			Q5_Until_ivonOut ();
		}
		if (Stage5_Controller._Stage5_Quest[6] && !Stage5_Controller._Stage5_Quest[7]) {
			Q6_get_star ();
		}
		if (Stage5_Controller._Stage5_Quest[9] && !Stage5_Controller._Stage5_Quest[10]) {
			Q7_check_ivon ();
		}
		if (Stage5_Controller._Stage5_Quest[12] && !Stage5_Controller._Stage5_Quest[13]) {
			Q8_ivon_out ();
		}
		if (Stage5_Controller._Stage5_Quest[14] && !Stage5_Controller._Stage5_Quest[15]) {
			Q9_snack_gotoBall ();
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[4] && !Stage5_Controller._Stage5_Quest[5]) {
			ti.currLineArr [1] = 8;
			ti.NPC_Say_yeah ("이본");
			//Stage5_Controller._Stage5_Quest[5] = true;
			Stage5_Controller._Stage5_Quest [5] = true;
		}
		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[8] && !Stage5_Controller._Stage5_Quest[9]) {

			ti.currLineArr [0] = 13;
			ti.NPC_Say_yeah ("별감");
			bed.enabled = true;
			door.enabled = false;
			//Stage5_Controller.q10 = true;
			Stage5_Controller._Stage5_Quest [9] = true;
		}
		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[7] && !Stage5_Controller._Stage5_Quest[8]) {

			Auto_ItemUse ();


			print ("방문 닫히는 소리");
			ti.currLineArr [0] = 8;
			ti.NPC_Say_yeah ("별감");
			bed.enabled = false;
			door.enabled = true;
			//Stage5_Controller.q9 = true;
			Stage5_Controller._Stage5_Quest [8] = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[10] && !Stage5_Controller._Stage5_Quest[11] &&  !tr4) {
			print ("잠든다");
			mbr.enabled = false;
			bed.enabled = false;
			door.enabled = true;
			StartCoroutine (Fadeout_black ());
			tr4 = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[13] && !Stage5_Controller._Stage5_Quest[14]) {
			ti.currLineArr [0] = 19;
			ti.NPC_Say_yeah ("별감");
			//Stage5_Controller.q15 = true;
			Stage5_Controller._Stage5_Quest [14] = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest [12] && !Stage5_Controller._Stage5_Quest [13] && !q8_a1) {
			if (!_star_textbox.activeSelf) {
				ti.currLineArr [1] = 14;
				ti.NPC_Say_yeah ("이본");
				q8_a1 = true;
			}
		}
	}

	void Auto_ItemUse(){
		print ("DSF");
		ic._item_name_list [3] = "";
		ic._usable_item [3] = false;
		ic._interaction_object [3] = "";
		ic._the_number_of_items [3] = 0;
		ic._item_list [3].GetComponent<Image> ().color = new Color (1, 1, 1, 0);
		ic._item_list [3].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 0);
		ic._explanations [3] = "";
	}

	void Q4_move_to_bed(){
		if (!q4_a1) {
			ti.currLineArr [1] = 6;
			ti.NPC_Say_yeah ("이본");
			q4_a1 = true;
		}
		if (q4_a1 && !_ivon_textbox.activeSelf) {
			//Stage5_Controller.q5 = true;
			Stage5_Controller._Stage5_Quest [4] = true;
		}
	}

	void Q5_Until_ivonOut(){
		if (!q5_a1 && !_ivon_textbox.activeSelf) {
			StartCoroutine (Kick_star ());
			q5_a1 = true;
		}
		if (q5_a2 && !_ivon_textbox.activeSelf) {
			_ivon.SetActive (false);
			//Stage5_Controller.q7 = true;
			Stage5_Controller._Stage5_Quest [6] = true;
		}
	}

	void Q6_get_star(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				//Stage5_Controller.q8 = true;
				Stage5_Controller._Stage5_Quest [7] = true;
				break;
			}
		}
	}

	void Q7_check_ivon(){
		if (!q7_a1 && !_star_textbox.activeSelf) {
			StartCoroutine (sadCoco ());
			q7_a1 = true;
		}
		if (q7_a1 && !_star_textbox.activeSelf) {
			//Stage5_Controller.q11 = true;
			Stage5_Controller._Stage5_Quest [10] = true;
		}
	}

	void Q8_ivon_out(){
		
		if (q8_a1 && !_ivon_textbox.activeSelf) {
			bed.enabled = false;
			door.enabled = false;
			snack.enabled = true;
			_dogsnack.SetActive (true);
			//Stage5_Controller.q14 = true;
			Stage5_Controller._Stage5_Quest [13] = true;
		}
	}

	void Q9_snack_gotoBall(){
		if (!q9_a1) {
			for (int i = 0; i < ic._item_list.Length; i++) {
				if (ic._item_name_list [i] == "DogSnack") {
					ti.currLineArr [0] = 22;
					ti.NPC_Say_yeah ("별감");
					q9_a1 = true;
					break;
				}
			}
		}

		if (ic._now_used_item == "DogSnack" && q9_a1 && !q9_a2) {
			ti.currLineArr [0] = 24;
			ti.NPC_Say_yeah ("별감");
			q9_a2 = true;
		}

		if (q9_a2 && !_star_textbox.activeSelf) {
			//Stage5_Controller.q16 = true;
			Stage5_Controller._Stage5_Quest [15] = true;
		}
	}

	IEnumerator Kick_star(){
		mbr.enabled = false;
		while (true) {
			print ("발로 참");
			_stardoll.SetActive (true);
			yield return new WaitForSeconds (2f);
			ti.currLineArr [1] = 10;
			ti.NPC_Say_yeah ("이본");
			break;
		}
		q5_a2 = true;
	}

	IEnumerator sadCoco(){
		mbr.enabled = false;
		print ("시무룩해함");
		while (true) {
			yield return new WaitForSeconds (2f);
			ti.currLineArr [0] = 15;
			ti.NPC_Say_yeah ("별감");
			break;
		}
	}

	IEnumerator Fadeout_black(){

		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 1);
		//Stage5_Controller.q12 = true;
		Stage5_Controller._Stage5_Quest [11] = true;

		//save point//
		Save_Script.Save_Now_Point();
		//save point//

		while (true) {
			yield return new WaitForSeconds (2f);
			StartCoroutine (Fadein_black ());
			break;
		}
	}

	IEnumerator Fadein_black(){
		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			Color bb = new Color (0, 0, 0, 1);
			bb.a = f;
			_blackout.color = bb;
			yield return null;
		}
		_blackout.color = new Color (0, 0, 0, 0);
		mbr.enabled = false;
		print ("코코 눈뜸");
		while (true) {
			yield return new WaitForSeconds (1f);
			break;
		}
		bed.enabled = false;
		door.enabled = true;
		ti.currLineArr [0] = 17;
		ti.NPC_Say_yeah ("별감");
		//Stage5_Controller.q13 = true;
		Stage5_Controller._Stage5_Quest [12] = true;
	}
}
