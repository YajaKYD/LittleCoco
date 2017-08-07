using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_6_GameController : MonoBehaviour {

	private Transform start_pos;
	private GameObject player;
//	private Outline o_l;
	//private GameObject stone;
	private Item_Controller i_c;
	private bool aaa = false;
	private bool aaaa = false;
	private bool bbb = false;
	//private bool ccc = false;
	private bool a1 = false;
	private bool a2 = false;
	private bool a3 = false;
	private bool a4 = false;
	private bool a5 = false;
	private Moving_by_RLbuttons mbr;

	public Mirror_Socket_Controller msc;
	public SpriteRenderer another_stone;
	public GameObject stone;
	public GameObject[] ringandstar;
	public GameObject particle;
	public GameObject portaltoend;
	public GameObject _coco_textbox;
	public GameObject _star_textbox;
	public SpriteRenderer _whiteOut;
	public GameObject[] _yellowThings;
	public GameObject _endingText;

	public GameObject portalto15;
	//private Mirror_Socket_Controller msc;

	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//stone = GameObject.FindWithTag ("HandMirror");
//		o_l = stone.GetComponent<Outline> ();
		i_c = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		//o_l = GameObject.Find ("Mirror_Socket").GetComponent<Outline> ();
		//o_l.used_or_not_for_retry = false;

		player.transform.position = start_pos.position;
	}

	void Start(){
		if (Stage1_Controller._Stage1_Quest [10]) {
			stone.SetActive (false);
			another_stone.gameObject.SetActive (false);
			for (int i = 0; i < _yellowThings.Length; i++) {
				_yellowThings [i].SetActive (true);
			}
		}
	}


	void Update(){

		if (msc.mirror_in_ornot && !aaa) {
			another_stone.gameObject.SetActive (true);
			player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
			StartCoroutine (Mirror_Effect_On (another_stone));
			aaa = true;
		}


//		if (i_c._now_used_item == "Handmirror" && !aaa) {
//			Destroy (stone);
//			if (ringandstar [0].activeSelf) {
//				ringandstar [0].GetComponent<BoxCollider2D> ().enabled = true;
//			}
////			if (ringandstar [1].activeSelf) {
////				ringandstar [1].GetComponent<BoxCollider2D> ().enabled = true;
////			}
//			i_c.cant_pick_during_using = true;
//			aaa = true;
//		}

		if (aaa && !aaaa) {
			for (int i = 0; i < 5; i++) {
				if (i_c._item_name_list [i] == "Star") {
					bbb = true;
				}
//				if (i_c._item_name_list [i] == "Goldring") {
//					ccc = true;
//				}
			}

			if (bbb) {
				particle.SetActive (false);
				aaaa = true;
			}
		}

		if (!stone.activeSelf && !Stage1_Controller._Stage1_Quest[9]) {
			Q3_Last ();
		}


		if (Stage1_Controller._Stage1_Quest[9]) {
			Destroy (GameObject.FindWithTag ("Controller"));
			Destroy (GameObject.FindWithTag ("Dialogue"));
			portaltoend.transform.position = player.transform.position;
			portaltoend.GetComponent<BoxCollider2D> ().enabled = true;
			Selecting_stage._what_stage_now_cleared = 1;//2스테이지 오픈시킴
			//PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
		}

	}

	void Q3_Last(){
		Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
		//aa.currLineArr [1] += 2;//별감 다음대사 치게함.

		if (!a4 && !a5) {
			StartCoroutine ("WhiteOut");
			if (ringandstar [0].activeSelf) {
				ringandstar [0].GetComponent<BoxCollider2D> ().enabled = true;
				ringandstar [1].GetComponent<BoxCollider2D> ().enabled = true;
			}
			a5 = true;
		}

		if (!a3 && a4 && bbb) {
			//print ("1");
			aa.NPC_Say_yeah ("별감");
			_star_textbox = GameObject.Find ("별감_text");
			a3 = true;
		}


		if (!a1 && a3 && !_star_textbox.activeSelf) {
			//print ("2");
			mbr.SetState(CocoState.Bark);
			aa.currLineArr [0] += 2;//코코 다음대사 치게함.
			aa.NPC_Say_yeah ("코코");
			_coco_textbox = GameObject.Find ("코코_text");
			a1 = true;
		}

		if (!a2 && a1 && !_coco_textbox.activeSelf) {
			//print ("3");
			aa.currLineArr [1] += 2;//별감 다음대사 치게함.
			aa.NPC_Say_yeah ("별감");
			a2 = true;
		}

		if (a2 && !_star_textbox.activeSelf) {
			//print ("4");
			Stage1_Controller._Stage1_Quest[9] = true;
		}
	}

	IEnumerator Mirror_Effect_On(SpriteRenderer a){
		//if (a.size.x > 1f) {
		float i = 0f;
		while (true) {
			i += 0.08f;
			a.size = new Vector2 (5.85f, i);
			if (i > 4.06f) {
				a.size = new Vector2 (5.85f, 4.06f);
				break;
			}
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		a.GetComponent<Rigidbody2D> ().gravityScale = 1f;
		//}
	}

	IEnumerator WhiteOut(){
		i_c.GetComponent<Canvas> ().enabled = false;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = false;
		yield return new WaitForSeconds (1f);

		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _whiteOut.color;
			c.a = f;
			_whiteOut.color = c;
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < _yellowThings.Length; i++) {
			_yellowThings [i].SetActive (true);
		}
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

		a4 = true;
		Stage1_Controller._Stage1_Quest [10] = true;
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;

		i_c.GetComponent<Canvas> ().enabled = true;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = true;
		portalto15.SetActive (false);
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
