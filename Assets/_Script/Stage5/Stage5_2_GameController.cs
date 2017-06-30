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
    private GameObject _coco_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

    public Outline _Bed;
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
    public GameObject _stardoll_afterused;
    private GameObject _dogsnack_not_item; // 간식 떨굴 때 나오는 용도.
	//
	private bool q4_a1 = false;
	private bool q5_a1 = false;
	private bool q5_a2 = false;
    private bool q5_a3 = false;
    private bool q5_a4 = false;
    private bool q5_a5 = false;
    private bool q5_a6 = false;
    private bool q6_a1 = false;
    private bool q7_a1 = false;
    private bool q7_a2 = false;
    private bool q7_a3 = false;
    private bool q7_a4 = false;
    private bool q7_a5 = false;
    private bool q7_a6 = false;
    private bool q8_a1 = false;
    private bool q8_a2 = false;
    private bool q8_a3 = false;
    private bool q8_a4 = false;
    private bool q9_a1 = false;
	private bool q9_a2 = false;
    private bool q9_a3 = false;

    private bool afterFadein = false;

    private bool t_e1 = false;
    private bool t_e2 = false;

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
        _dogsnack_not_item = GameObject.Find("DogSnack_notItem");

		player.transform.position = start_pos.position;
        _stardoll_afterused.SetActive(false);
    }

	void Start(){
        _dogsnack_not_item.SetActive(false); // 처음에는 없어야 하므로...
		//		if (GetComponent<Load_data> ()._where_are_you_from == 10) {
		//			player.transform.position = regen_pos.position;
		//		}
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];
        _coco_textbox = ti._text_boxes [2];

		if (Stage5_Controller._Stage5_Quest[3] && !Stage5_Controller._Stage5_Quest[4]) {
			Save_Script.Save_Now_Point ();
		}
		if (Stage5_Controller._Stage5_Quest [6]) {
			_ivon.SetActive (false);
		}
		if (Stage5_Controller._Stage5_Quest[7]) {
			Destroy (_stardoll);
            _stardoll_afterused.SetActive(true);
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
        if (Stage5_Controller._Stage5_Quest[7] && !Stage5_Controller._Stage5_Quest[8])
        {
            Q6_5_coco_talk();
        }
        if (Stage5_Controller._Stage5_Quest[9] && !Stage5_Controller._Stage5_Quest[10]) {
			Q7_check_ivon ();
		}
        if (Stage5_Controller._Stage5_Quest[10] && !Stage5_Controller._Stage5_Quest[11] && tr4) // Fadeout 시키기 위함
        {
            StartCoroutine(Fadeout_black());
        }
        if (Stage5_Controller._Stage5_Quest[11] && !Stage5_Controller._Stage5_Quest[12] && afterFadein) // Fadein 시키기 위함
        {
            Q7_5_After_Fadein();
        }
        if (Stage5_Controller._Stage5_Quest[12] && !Stage5_Controller._Stage5_Quest[13]) {
			Q8_ivon_out ();
		}
		if (Stage5_Controller._Stage5_Quest[14] && !Stage5_Controller._Stage5_Quest[15]) {
			Q9_snack_gotoBall ();
		}
        if (Stage5_Controller._Stage5_Quest[15] && Stage5_Controller._Stage5_Quest[16]) { // 이 곳으로 다시 돌아오면 간식이 없어지도록..
            Destroy(_dogsnack_not_item);
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
            print("시무룩해함");
			ti.currLineArr [2] = 12; // "..."
			ti.NPC_Say_yeah ("코코");
			bed.enabled = true;
			door.enabled = false;
            //Stage5_Controller.q10 = true;
			Stage5_Controller._Stage5_Quest [9] = true;
		}
		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[7] && !Stage5_Controller._Stage5_Quest[8]) {
            //	Auto_ItemUse ();
			print ("방문 닫히는 소리");
			ti.currLineArr [0] = 18;
			ti.NPC_Say_yeah ("별감");
			bed.enabled = false;
			door.enabled = true;
            //Stage5_Controller.q9 = true;
            t_e1 = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[10] && !Stage5_Controller._Stage5_Quest[11] &&  !tr4) {
			print ("잠든다");
            ti.currLineArr[2] = 14;
            ti.NPC_Say_yeah("코코"); // "..." + "zzz"
			mbr.enabled = false;
			bed.enabled = false;
			door.enabled = true;
			//StartCoroutine (Fadeout_black ());
			tr4 = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest[13] && !Stage5_Controller._Stage5_Quest[14]) {
			//ti.currLineArr [0] = 19;
			//ti.NPC_Say_yeah ("별감");
			//Stage5_Controller.q15 = true;
			Stage5_Controller._Stage5_Quest [14] = true;
		}

		if (other.CompareTag ("Player") && Stage5_Controller._Stage5_Quest [12] && !Stage5_Controller._Stage5_Quest [13] && !q8_a1) { // 이본 확인하러 가는 거
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
            q5_a3 = true;
        }
        if (q5_a3 && !q5_a4)
        {
            ti.currLineArr[0] = 14; // 주인님이 돌아오셧
            ti.NPC_Say_yeah("별감");
            q5_a4 = true;
        }
        if (q5_a4 && !q5_a5 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 8;
            ti.NPC_Say_yeah("코코");
            q5_a5 = true;
        }
        if (q5_a5 && !q5_a6 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 16; // 쿠션 다시 가져와..
            ti.NPC_Say_yeah("별감");
            q5_a6 = true;
            Stage5_Controller._Stage5_Quest[6] = true;
        }
    }

    void Q6_get_star()
    {
        /*for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "StarDoll") {
				//Stage5_Controller.q8 = true;
				Stage5_Controller._Stage5_Quest [7] = true;
				break;
			}
		}*/
        if (_Bed.used_or_not_for_retry && !q6_a1)
        {
            print("사용");
            // _stardoll_afterused.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            _stardoll_afterused.SetActive(true);

            q6_a1 = true;
        }
        if (q6_a1)
        {
            Stage5_Controller._Stage5_Quest[7] = true; // 아무것도 없는 상태..
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }
    }
    void Q6_5_coco_talk()
    {
        if (t_e1 && !t_e2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 10;
            ti.NPC_Say_yeah("코코");
            t_e2 = true;
            Stage5_Controller._Stage5_Quest[8] = true;
        }
    }

    void Q7_check_ivon(){
		if (!q7_a1 && !_coco_textbox.activeSelf) {
            //StartCoroutine (sadCoco ());
            ti.currLineArr[0] = 22;
            ti.NPC_Say_yeah("별감");
			q7_a1 = true;
		}
        if (q7_a1 && !q7_a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 12;
            ti.NPC_Say_yeah("코코");
            q7_a2 = true;
        }
		if (q7_a2 && !_coco_textbox.activeSelf) {
            ti.currLineArr[0] = 24;
            ti.NPC_Say_yeah("별감");
			//Stage5_Controller.q11 = true;
			Stage5_Controller._Stage5_Quest [10] = true;
		}
	}

    void Q7_5_After_Fadein()
    {
        if (!q7_a3)
        {
            print("코코 눈뜸");
            ti.currLineArr[2] = 17; // "!"
            ti.NPC_Say_yeah("코코");
            q7_a3 = true;
        }
        if (q7_a3 && !q7_a4 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 26; // 흐아암 졸려
            ti.NPC_Say_yeah("별감");
            q7_a4 = true;
        }
        if (q7_a4 && !q7_a5 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 17; // "!"
            ti.NPC_Say_yeah("코코");
            q7_a5 = true;
        }
        if (q7_a5 && !q7_a6 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 28; // "그래 가보자"
            ti.NPC_Say_yeah("별감");
            q7_a6 = true;
            Stage5_Controller._Stage5_Quest[12] = true;
        }
    }

    void Q8_ivon_out(){
        if (q8_a1 && !q8_a2 && !_ivon_textbox.activeSelf)
        {
            ti.currLineArr[0] = 30;
            ti.NPC_Say_yeah("별감");
            q8_a2 = true;
        }
        if (q8_a2 && !q8_a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 19;
            print("시무룩한 코코");
            ti.NPC_Say_yeah("코코");
            q8_a3 = true;
        }
        if (q8_a3 && !q8_a4 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 33;
            ti.NPC_Say_yeah("별감");
            q8_a4 = true;
        }
		if (q8_a4 && !_star_textbox.activeSelf) {
			bed.enabled = false;
			door.enabled = false;
			snack.enabled = true;
			_dogsnack.SetActive (true);
			//Stage5_Controller.q14 = true;
			Stage5_Controller._Stage5_Quest [13] = true;
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }
    }

	void Q9_snack_gotoBall(){
		if (!q9_a1) {
			for (int i = 0; i < ic._item_list.Length; i++) {
				if (ic._item_name_list [i] == "DogSnack") {
					ti.currLineArr [0] = 35;
					ti.NPC_Say_yeah ("별감");
					q9_a1 = true;
					break;
				}
			}
		}

		if (ic._now_used_item == "DogSnack" && q9_a1 && !q9_a2) {
            _dogsnack_not_item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dogsnack_not_item.transform.position = new Vector3(_dogsnack_not_item.transform.position.x, _dogsnack_not_item.transform.position.y - 1f, 6.5f);
            _dogsnack_not_item.SetActive(true);

            ti.currLineArr [0] = 37;
			ti.NPC_Say_yeah ("별감");
			q9_a2 = true;
		}

		if (q9_a2 && !q9_a3 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 21;
            print("힘이 없게 짖는 코코");
            ti.NPC_Say_yeah("코코");
            q9_a3 = true;
			//Stage5_Controller.q16 = true;
		}
        if (q9_a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 41;
            ti.NPC_Say_yeah("별감");
            Stage5_Controller._Stage5_Quest[15] = true;
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
        if (!_coco_textbox.activeSelf)
        {
            for (float f = 0f; f < 1; f += Time.deltaTime)
            {
                Color c = _blackout.color;
                c.a = f;
                _blackout.color = c;
                yield return null;
            }
            _blackout.color = new Color(0, 0, 0, 1);
            //Stage5_Controller.q12 = true;

            while (true)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(Fadein_black());
                break;
            }
            Stage5_Controller._Stage5_Quest[11] = true;
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
        while (true) {
			yield return new WaitForSeconds (2f);
            afterFadein = true;
            break;
		}
        bed.enabled = false;
        door.enabled = true;
		//Stage5_Controller.q13 = true;
		//Stage5_Controller._Stage5_Quest [12] = true;
	}
}
