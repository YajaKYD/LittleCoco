using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_1_GameController : Controller {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private Text_Importer2 ti;
	private Item_Controller ic;

    private bool GGALJAKI1, GGALJAKI2, GGALJAKI3,
        q1a1,
        q2a1, q2a2, q2a3, q2a4,
        q3a1, q3a2, q3a3, q3a4, q3a5, q3a6,
        q10a1,
        q13a1,
        q14a1, q14a2,
        q15a1, q15a2, q15a3, q15a4, q15a5, q15a6, q15a7, q15a8,
        q16a1, q16a2;

    public SpriteRenderer _blackout; 
	public SpriteRenderer _bg;
	public GameObject _ivon;
	public PolygonCollider2D ball;
    public GameObject ball_object;
	public GameObject portal_to_dream;
    public BoxCollider2D portaltoend;
	public Transform afterdreampos;
    public BoxCollider2D portal; // 일기장 흔들기 전에 못나가게
    public Camera mainCamera;
    
    void Awake(){
        sceneNo = 51;
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;

		if (!Stage5_Controller.q[0]) { // 첫 번째 퀘스트 깨기 전이면
			mbr.enabled = false;
			print ("엎드려있는ani");
		}
	}

	void Start(){
		
		if (GetComponent<Load_data> ()._where_are_you_from == 33) { // 스테이지 4에서 온 게 아니라면
			player.transform.position = regen_pos.position;
		}
        else if (GetComponent<Load_data>()._where_are_you_from == 36)
        { 
            player.transform.position = start_pos.position;
        }

        ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(51);

        if (Stage5_Controller.q[3]) {//after 1st scene end
			//_blackout.color = new Color (0,0,0,0);
			_bg.color = new Color (1, 1, 1, 1);
			GetComponent<BoxCollider2D> ().enabled = false;
            GGALJAKI1 = true; GGALJAKI2 = true; GGALJAKI3 = true;
        }

		if (Stage5_Controller.q [15] && !Stage5_Controller.q[16]) {
			print ("별감대사부터시작");
			player.transform.position = regen_pos.position;
		}

        //		if (Stage5_Controller.q [17]) {
        //			print ("화분 이미지 바뀜 ~ 잠깰때까지");
        //		}

        if (Stage5_Controller.q[18] && !Stage5_Controller.q[20])
        {
            ball_object.SetActive(false);
        }

        if (Stage5_Controller.q[20]) ball_object.transform.position = new Vector2(-12.91054f, -3.294492f);

        if (Stage5_Controller.q [22] && !Stage5_Controller.q[23]) {
			portal_to_dream.gameObject.SetActive(false);
			player.transform.position = afterdreampos.position;
			player.transform.rotation = Quaternion.Euler (0, 180, 0);
		}

	}

	void Update(){
		if (!Stage5_Controller.q[0] && _blackout.color.a <= 0) {
			Q1_starsay1 ();
		}
        else if (Stage5_Controller.q[0] && !GGALJAKI1 && !GGALJAKI2)
        {
            mbr.enabled = false;
            StartCoroutine(Delay_2sec());
            GGALJAKI1 = true;
        }
		else if (!Stage5_Controller.q[2] && Stage5_Controller.q[1] && GGALJAKI2) {
			Q2_Until_fadeout ();
		}
        else if (Stage5_Controller.q[2] && !GGALJAKI3)
        {
            mbr.enabled = false;
			StartCoroutine (Fadeout_black ());
            GGALJAKI3 = true;
        }
		else if (!Stage5_Controller.q[3] && Stage5_Controller.q[2]) {
			Q3_fadein_and_coco ();
		}
        else if (Stage5_Controller.q[3] && !Stage5_Controller.q[51])
        {
            Q4_goto5_2();
        }
		else if (Stage5_Controller.q [15] && !Stage5_Controller.q [16]) {
            Q10_starSay ();
		}
		else if (Stage5_Controller.q [16] && !Stage5_Controller.q [17]) {
			Q11_putStaronPot ();
		}
		else if (Stage5_Controller.q [17] && !Stage5_Controller.q [18]) {
			Q12_getTheball ();
		}
		else if (Stage5_Controller.q [20] && !Stage5_Controller.q [21]) {
			Q13_FadeOUT ();
		}
		else if (Stage5_Controller.q [22] && !Stage5_Controller.q [23]) {
			Q14_Until_diary ();
		}
		else if (Stage5_Controller.q [23] && !Stage5_Controller.q [24]) {
			Q15_using_diary ();
		}
        else if (Stage5_Controller.q[49] && !Stage5_Controller.q[50]) {
            Q16_Talk_Finish();
        }
        else if (Stage5_Controller.q[50] && !Stage5_Controller.q[83])
        {
            mbr.Moving_Right(8f);
            _ivon.transform.position = new Vector2(_ivon.transform.position.x + 0.1f, _ivon.transform.position.y);
        }

    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			mbr.enabled = false;
			//Stage5_Controller.q2 = true;
			Stage5_Controller.q [1] = true;
		}
	}

	void Q1_starsay1(){
		if (!q1a1)
        {
            ti.Talk(); // 별일 없을거라니까 왜 그래
            q1a1 = true;
        }
	}

	void Q2_Until_fadeout(){
		if (!q2a1) {
            mbr.enabled = false;
			StartCoroutine (Open_Door ());
			q2a1 = true;
		}
		else if (q2a2 && q2a3 && !q2a4) {
            ti.Talk(16); // 코코!!
            print("코코 ani stop");
            q2a4 = true;
		}
	}

	void Q3_fadein_and_coco(){
		if (!q3a1) {
            mbr.enabled = false;
			StartCoroutine (Fadein_black ());
			q3a1 = true;
		}
		else if (q3a2 && !q3a3) {
            ti.Talk(18); // 코코~!
            q3a3 = true;
		}
	}

    void Q4_goto5_2()
    {
        if (!q3a4)
        {
            mbr.enabled = false;
            print("idle animation");
            StartCoroutine(idle_2sec());
            q3a4 = true;
        }
        else if (q3a4 && q3a5 && !q3a6)
        {
            ti.Talk(20); // 코코~!
            q3a6 = true;
        }
    }

    void Q10_starSay(){
        if (!q10a1)
        {
            ti.Talk(22);
            q10a1 = true;
            ic._consumable[1] = true; // 임시로 소모품 만듦
        }
        mbr.enabled = false;
    }

	void Q11_putStaronPot(){
		if (ic._now_used_item == "Star") {
			print ("화분 이미지 바뀜");
			ball.enabled = true;
			//ti.NPC_Say_yeah ("StarInField");
			Stage5_Controller.q [17] = true;
		}
	}

	void Q12_getTheball(){
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "Ball") {
                Stage5_Controller.q [18] = true;
			}
		}
	}

	void Q13_FadeOUT(){
		if (!q13a1) {
            //StartCoroutine (sadcoco_2sec ());
            ti.Talk(30); //미..미안...
			q13a1 = true;
		}
		else if (Stage5_Controller.q[55]) {
			StartCoroutine (ToDream ());
		}
	}

	void Q14_Until_diary(){
		if (!q14a1) {
            portal.enabled = false;
            ti.Talk(33); // 이게 무슨 일이야!!
			q14a1 = true;
		}
        else if (Stage5_Controller.q[58] && !q14a2)
        {
            StartCoroutine(Diary_enable());
            q14a2 = true;
        }
        mbr.enabled = false;
    }

	void Q15_using_diary(){
		if (!q15a1 && Item_Drag._NOW_Shaked) {
            ti.Talk(40); // 도움이 될 만한
			q15a1 = true;
        }
        else if (Stage5_Controller.q[59] && !q15a2)
        {
            Item_Drag._NOW_Shaked = false;
            q15a2 = true;
        }
		else if (q15a2 && !q15a3 && Item_Drag._NOW_Shaked) {
			GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/dogddong"));
			k.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            k.transform.position = new Vector3(k.transform.position.x, k.transform.position.y, 0f);
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D> (), k.GetComponent<Collider2D> (), true);
			Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag> ();
			for (int x = 0; x < ids.Length; x++) {
				ids [x]._diary_usable = false;
			} //change diary image -unusable-
			Item_Drag._NOW_Shaked = false;
			q15a3 = true;
		}
		else if (q15a3 && !q15a4) {
            ti.Talk(43); // 이런 씨바아앙ㄹ!
            q15a4 = true;
		}
		else if (Stage5_Controller.q[60] && !q15a5) {
			StartCoroutine (Disapointed ());
			q15a5 = true;
		}
        else if (Stage5_Controller.q[61] && !q15a6)
        {
            StartCoroutine(Frustrated()); // 절망에 빠지는 털썩 엎드리는 코코
            q15a6 = true;
        }
        else if (q15a7 && !q15a8)
        {
            ti.Talk(49); // 코코야 방법이..
            print("일어선다");
            portal.enabled = true;
            q15a8 = true;
            //save point//
          //  Save_Script.Save_Now_Point();
            //save point//
        }
    }

    void Q16_Talk_Finish()
    {
        if (!q16a1)
        {
            ti.Talk(57); // Coco: ...
            q16a1 = true;
        }
        else if (Stage5_Controller.q[82] && !q16a2)
        {
            _ivon.SetActive(true);
            _ivon.transform.position = new Vector2(0.8f, _ivon.transform.position.y);
            ti.Talk(59); // 목욕해서 개운햐
            q16a2 = true;
            portaltoend.enabled = true;
            portal.enabled = false;
            mainCamera.GetComponent<CameraManager>().enabled = false;
            Selecting_stage._what_stage_now_cleared = 5;
        }
    }

    IEnumerator Delay_2sec(){
		while (true) {
			yield return new WaitForSeconds (2f);
			print ("일어남");
			yield return new WaitForSeconds (1f);
            ti.Talk(14); // 주인님이 오셧구나
			break;
		}
        //Stage5_Controller.q1 = true;
        mbr.enabled = false;
		GGALJAKI2 = true;
	}

	IEnumerator Open_Door(){
		while (true) {
            mbr.enabled = false;
			print ("Open_Sound");
			yield return new WaitForSeconds (2f); //fit to sound length
            mbr.enabled = false;
			print ("앞발들고 선다");
			yield return new WaitForSeconds (1f);
            mbr.enabled = false;
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

/*	IEnumerator sadcoco_2sec(){
		mbr.enabled = false;
		print ("시무룩해하는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		ti.currLineArr [0] = 31; //sorry
		ti.NPC_Say_yeah("별감");
		q13a2 = true;
	}*/

	IEnumerator ToDream(){
		mbr.enabled = false;
		print ("엎드리는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
		Stage5_Controller.q [21] = true;
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
        mbr.enabled = true;
        Stage5_Controller.q[23] = true;
    }

	IEnumerator Disapointed(){
		mbr.enabled = false;
		print ("좌절하는 코코");
		while (true) {
			yield return new WaitForSeconds (2f);
			break;
		}
        ti.Talk(46); // 화분도 깨고 바닥에 똥까지..
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
        q15a7 = true;
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
