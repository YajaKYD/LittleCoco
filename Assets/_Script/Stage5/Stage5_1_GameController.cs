using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_1_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
    private GameObject _coco_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	private bool q1a1 = false;
	private bool q1a2 = false;
    private bool q1a3 = false;
    private bool q1a4 = false;
    private bool q1a5 = false;
    private bool q1a6 = false;
    private bool q1a7 = false;
    private bool q1a8 = false;
    private bool q1a9 = false;
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
    private bool q10a1 = false;
    private bool q10a2 = false;
    private bool q10a3 = false;
    private bool q10a4 = false;
    private bool q13a1 = false;
	private bool q13a2 = false;
	private bool q13a3 = false;
	private bool q14a1 = false;
    private bool q14a2 = false;
    private bool q14a3 = false;
    private bool q15a1 = false;
	private bool q15a2 = false;
	private bool q15a3 = false;
	private bool q15a4 = false;
	private bool q15a5 = false;
	private bool q15a6 = false;
	private bool q15a7 = false;
    private bool q15a8 = false;
    private bool q15a9 = false;
    private bool q15a10 = false;
    private bool q15a11 = false;

    public SpriteRenderer _blackout; 
	public SpriteRenderer _bg;
	public GameObject _ivon;
	public PolygonCollider2D ball;
	public GameObject portal_to_dream;
	public Transform afterdreampos;

	void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;

		if (!Stage5_Controller._Stage5_Quest[0]) { // 첫 번째 퀘스트 깨기 전이면
			mbr.enabled = false;
			print ("엎드려있는ani");
		}
	}

	void Start(){
		
		if (GetComponent<Load_data> ()._where_are_you_from == 33) { // 스테이지 4에서 온 게 아니라면
			player.transform.position = regen_pos.position;
		}

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];
        _coco_textbox = ti._text_boxes [2];

		if (Stage5_Controller._Stage5_Quest[3]) {//after 1st scene end
			//_blackout.color = new Color (0,0,0,0);
			_bg.color = new Color (1, 1, 1, 1);
			GetComponent<BoxCollider2D> ().enabled = false;
		}

		if (Stage5_Controller._Stage5_Quest [15] && !Stage5_Controller._Stage5_Quest[16]) {
			print ("별감대사부터시작");
			//save point//
			Save_Script.Save_Now_Point ();
			//save point//
			player.transform.position = regen_pos.position;
		}

//		if (Stage5_Controller._Stage5_Quest [17]) {
//			print ("화분 이미지 바뀜 ~ 잠깰때까지");
//		}

		if (Stage5_Controller._Stage5_Quest [22] && !Stage5_Controller._Stage5_Quest[23]) {
			Save_Script.Save_Now_Point ();
			portal_to_dream.gameObject.SetActive(false);
			player.transform.position = afterdreampos.position;
			player.transform.rotation = Quaternion.Euler (0, 180, 0);
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
		if (Stage5_Controller._Stage5_Quest [15] && !Stage5_Controller._Stage5_Quest [16]) {
            Q10_starSay ();
		}
		if (Stage5_Controller._Stage5_Quest [16] && !Stage5_Controller._Stage5_Quest [17]) {
			Q11_putStaronPot ();
		}
		if (Stage5_Controller._Stage5_Quest [17] && !Stage5_Controller._Stage5_Quest [18]) {
			Q12_getTheball ();
		}
		if (Stage5_Controller._Stage5_Quest [20] && !Stage5_Controller._Stage5_Quest [21]) {
			Q13_FadeOUT ();
		}
		if (Stage5_Controller._Stage5_Quest [22] && !Stage5_Controller._Stage5_Quest [23]) {
			Q14_Until_diary ();
		}
		if (Stage5_Controller._Stage5_Quest [23] && !Stage5_Controller._Stage5_Quest [24]) {
			Q15_using_diary ();
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
			ti.currLineArr [0] = 0; // 코코야 별일 없을거라니
			ti.NPC_Say_yeah ("별감");
			q1a1 = true;
		}
        if (q1a1 && !q1a2 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 0; // 낑
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        if (q1a2 && !q1a3 && !_coco_textbox.activeSelf) {
            ti.currLineArr[0] = 2; // 죽긴 누가
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        if (q1a3 && !q1a4 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 2; // 이본
            ti.NPC_Say_yeah("코코");
            q1a4 = true;
        }
        if (q1a4 && !q1a5 && !_coco_textbox.activeSelf) {
            ti.currLineArr[0] = 5; // 응 주인님이 한동안
            ti.NPC_Say_yeah("별감");
            q1a5 = true;
        }
        if (q1a5 && !q1a6 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 4;
            ti.NPC_Say_yeah("코코");
            q1a6 = true;
        }
        if (q1a6 && !q1a7 && !_coco_textbox.activeSelf) {
            ti.currLineArr[0] = 8;
            ti.NPC_Say_yeah("별감");
            q1a7 = true;
        }
        if (q1a7 && !q1a8 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 6;
            ti.NPC_Say_yeah("코코");
            q1a8 = true;
        }
		if (q1a8 && !q1a9 && !_star_textbox.activeSelf) {
			mbr.enabled = false;
			StartCoroutine (Delay_2sec ());
			q1a9 = true;
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

	void Q10_starSay(){
        if (!q10a1)
        {
            ti.currLineArr[0] = 43;//자! 해보자
            ti.NPC_Say_yeah("별감");
            q10a1 = true;
        }
		if (q10a1 && !q10a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 21;
            ti.NPC_Say_yeah("코코");
            q10a2 = true;
        }
        if (q10a2 && !q10a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 46;
            ti.NPC_Say_yeah("별감");
            q10a3 = true;
        }
        if (q10a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 21;
            ti.NPC_Say_yeah("코코");
            ic._consumable [1] = true; //임시로 소모품 만듦
            Stage5_Controller._Stage5_Quest [16] = true;
        }
    }

	void Q11_putStaronPot(){
		if (ic._now_used_item == "Star") {
			print ("화분 이미지 바뀜");
			ball.enabled = true;
			//ti.NPC_Say_yeah ("StarInField");
			Stage5_Controller._Stage5_Quest [17] = true;
		}
	}

	void Q12_getTheball(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "Ball") {
                //save point//
                Save_Script.Save_Now_Point();
                //save point//
                Stage5_Controller._Stage5_Quest [18] = true;
			}
		}
	}

	void Q13_FadeOUT(){
		if (!q13a1) {
            //StartCoroutine (sadcoco_2sec ());
            ti.currLineArr[2] = 23;
            print("상당히 실망한 코코");
            ti.NPC_Say_yeah("코코");
			q13a1 = true;
		}
        if (q13a1 && !q13a2 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 50;
            ti.NPC_Say_yeah("별감");
            q13a2 = true;
        }
        if (q13a2 && !q13a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 23;
            ti.NPC_Say_yeah("코코");
            q13a3 = true;
        }
		if (q13a3 && !_coco_textbox.activeSelf) {
			StartCoroutine (ToDream ());
		}
	}

	void Q14_Until_diary(){
		if (!q14a1) {
			ti.currLineArr [0] = 52;//이게 무슨일
			ti.NPC_Say_yeah("별감");
			q14a1 = true;
		}
		if (q14a1 && !q14a2 && !_star_textbox.activeSelf) {
            ti.currLineArr[2] = 25;
            ti.NPC_Say_yeah("코코");
            q14a2 = true;
		}
        if (q14a2 && !q14a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 55;
            ti.NPC_Say_yeah("별감");
            q14a3 = true;
        }
        if (q14a3 && !_star_textbox.activeSelf)
        {
            StartCoroutine(Diary_enable());
        }
    }

	void Q15_using_diary(){
		if (!q15a1) {
			ti.currLineArr [0] = 57;//마..망했어!!
			ti.NPC_Say_yeah ("별감");
			q15a1 = true;
		}
		if (q15a1 && !q15a2 && !_star_textbox.activeSelf && Item_Drag._NOW_Shaked) {
			ti.currLineArr [0] = 60; //도움이 될 만한.
			ti.NPC_Say_yeah ("별감");
			Item_Drag._NOW_Shaked = false;
			q15a2 = true;
		}
		if (q15a2 && _star_textbox.activeSelf) {
			Item_Drag._NOW_Shaked = false;
		}
		if (q15a2 && !q15a3 && !_star_textbox.activeSelf && Item_Drag._NOW_Shaked) {
			GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/dogddong"));
			k.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			k.transform.position = new Vector3 (k.transform.position.x, k.transform.position.y, 0f);
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D> (), k.GetComponent<Collider2D> (), true);
			Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag> ();
			for (int x = 0; x < ids.Length; x++) {
				ids [x]._diary_usable = false;
			} //change diary image -unusable-
			Item_Drag._NOW_Shaked = false;
			q15a3 = true;
		}
		if (q15a3 && !q15a4) {
			ti.currLineArr [0] = 63;//안돼 똥
			ti.NPC_Say_yeah ("별감");
			q15a4 = true;
		}
        if (q15a4 && !q15a5 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 27; // 하트는 똥으로 바꾸고 no는 절망으로
            ti.NPC_Say_yeah("코코");
            q15a5 = true;
        }
		if (q15a5 && !_coco_textbox.activeSelf && !q15a6) {
			StartCoroutine (Disapointed ());
			q15a6 = true;
		}
        if (q15a6 && q15a7 && !q15a8 && !_star_textbox.activeSelf)
        {
            StartCoroutine(Frustrated()); // 절망에 빠지는 털썩 엎드리는 코코
        }
        if (q15a8 && !q15a9)
        {
            ti.currLineArr[0] = 68; // 코코야 방법이...
            ti.NPC_Say_yeah("별감");
            q15a9 = true;
        }
        if (q15a9 && !q15a10 && !_star_textbox.activeSelf)
        {
            print("일어선다");
            ti.currLineArr[2] = 30;
            ti.NPC_Say_yeah("코코");
            q15a10 = true;
        }
        if (q15a10 && !q15a11 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 72; // 응? 화분을 새로??
            ti.NPC_Say_yeah("별감");
            q15a11 = true;
        }
        if (q15a11 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 30;//멍
            ti.NPC_Say_yeah("코코");
            q15a11 = true;
            Stage5_Controller._Stage5_Quest[24] = true;
        }
	}

	IEnumerator Delay_2sec(){
		while (true) {
			yield return new WaitForSeconds (2f);
			print ("일어남");
			yield return new WaitForSeconds (1f);
			ti.currLineArr [0] = 12;
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

	IEnumerator sadcoco_2sec(){
		mbr.enabled = false;
		print ("시무룩해하는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		ti.currLineArr [0] = 31; //sorry
		ti.NPC_Say_yeah("별감");
		q13a2 = true;
	}

	IEnumerator ToDream(){
		mbr.enabled = false;
		print ("엎드리는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		Stage5_Controller._Stage5_Quest [21] = true;
		portal_to_dream.transform.position = player.transform.position;
		//StartCoroutine (Fadeout_black ());
	}

	IEnumerator Diary_enable(){
		mbr.enabled = false;
		print ("당황하는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag> ();
		for (int k = 0; k < ids.Length; k++) {
			ids [k]._diary_usable = true;
        } //change diary image -usable-
        Stage5_Controller._Stage5_Quest[23] = true;
    }

	IEnumerator Disapointed(){
		mbr.enabled = false;
		print ("좌절하는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		ti.currLineArr [0] = 65;//화분도 깨고 바닥에..
		ti.NPC_Say_yeah ("별감");
		q15a7 = true;
	}

    IEnumerator Frustrated()
    {
        mbr.enabled = false;
        print("좌절하는 코코");
        while (true)
        {
            yield return new WaitForSeconds(2f);
            break;
        }
        q15a8 = true;
    }

//	IEnumerator Fadein_Dream(){
//		while (true) {
//			yield return new WaitForSeconds (2f);
//
//			//GetComponent<BoxCollider2D> ().enabled = false;
//			print ("배경 꿈 전환");
//			break;
//		}
//		for (float f = 1f; f > 0; f -= Time.deltaTime) {
//			Color bb = new Color (0, 0, 0, 1);
//			bb.a = f;
//			_blackout.color = bb;
//			yield return null;
//		}
//		_ivon.SetActive (true);
//		_blackout.color = new Color (0, 0, 0, 0);
//
//	}
}
