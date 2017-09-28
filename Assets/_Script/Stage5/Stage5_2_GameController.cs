using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_2_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private Transform start_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private Text_Importer2 ti;
	private Item_Controller ic;
    private Rigidbody2D rb2D;
    private BoxCollider2D dogBed;

    public Outline _Bed;
	public GameObject _ivon;
	public BoxCollider2D bed;
	public BoxCollider2D door;
	public BoxCollider2D snack;
	public SpriteRenderer _blackout;
	public GameObject _dogsnack;
	public Transform sleep_pos;
	public BoxCollider2D gooutportal;
    public BoxCollider2D portalto5_1;
	//temp
	public GameObject _stardoll;
    public GameObject _stardoll_afterused;
    private GameObject _dogsnack_not_item; // 간식 떨굴 때 나오는 용도.
                                           //
    private bool q4_a1,
        q5_a1, q5_a2, q5_a3,
        q6_a1,
        q7_a1,
        q9_a1, q9_a2;

    private bool itemeat;
    private bool afterFadein = false;
    
	private bool tr4 = false;

	void Awake(){
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage5_2");

        sceneNo = 52;
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
        _dogsnack_not_item = GameObject.Find("DogSnack_notItem");
        rb2D = _stardoll_afterused.GetComponent<Rigidbody2D>();
        dogBed = GameObject.FindWithTag("Bed").GetComponent<BoxCollider2D>();
     
		player.transform.position = start_pos.position;
        _stardoll.SetActive(false);
        itemeat = false;
    }

	void Start(){
        _dogsnack_not_item.SetActive(false); // 처음에는 없어야 하므로...
		//		if (GetComponent<Load_data> ()._where_are_you_from == 10) {
		//			player.transform.position = regen_pos.position;
		//		}
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(52);

        Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), bed);
        Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), dogBed);
        Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), player.GetComponent<PolygonCollider2D>());
        
            bed.enabled = true;

		if (Stage5_Controller.q [6]) {
			_ivon.SetActive (false);
		}
        if (Stage5_Controller.q[6] && !Stage5_Controller.q[7]) // 쿠션을 찬 상태
        {
            _stardoll_afterused.SetActive(false);
            for (int i = 0; i < ic._item_list.Length; i++)
            {
                if (ic._item_name_list[i] == "StarDoll") // 아이템을 먹은 상태
                {
                    itemeat = true;
                    break;
                }
            }
            if (!itemeat) _stardoll.SetActive(true);
        }

        if (Stage5_Controller.q[7]) {
			Destroy (_stardoll);
            _stardoll_afterused.SetActive(true);
        }
		if (Stage5_Controller.q [7] && !Stage5_Controller.q [9]) {
            portalto5_1.enabled = false;
            bed.enabled = true; 
            door.enabled = false;
		}

		if (Stage5_Controller.q[11] && !Stage5_Controller.q[12]) {//savepoint
			player.transform.position = sleep_pos.position;
			StartCoroutine (Fadein_black ());
			bed.enabled = false;
			door.enabled = true;
		}

		if (Stage5_Controller.q [12] && !Stage5_Controller.q [13]) {
			bed.enabled = false;
			door.enabled = true;
		}

		if (Stage5_Controller.q [24]) {
			gooutportal.enabled = true;
		}

	}

	void Update () {

		if (!Stage5_Controller.q[4]) {
			Q4_move_to_bed ();
		}
		else if (Stage5_Controller.q[5] && !Stage5_Controller.q[6]) {
			Q5_Until_ivonOut ();
		}
		else if (Stage5_Controller.q[6] && !Stage5_Controller.q[7]) {
			Q6_get_star ();
        }
        else if (Stage5_Controller.q[53] && !Stage5_Controller.q[11]) // Fadeout 시키기 위함
        {
            mbr.enabled = false;
            StartCoroutine(Fadeout_black());
        }
        else if (Stage5_Controller.q[11] && !Stage5_Controller.q[12] && afterFadein) // Fadein 시키기 위함
        {
            Q7_5_After_Fadein();
        }
        else if (Stage5_Controller.q[14] && !Stage5_Controller.q[15]) {
			Q9_snack_gotoBall ();
		}
        else if (Stage5_Controller.q[15] && Stage5_Controller.q[16]) { // 이 곳으로 다시 돌아오면 간식이 없어지도록..
            Destroy(_dogsnack_not_item);
        }

    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage5_Controller.q[4] && !Stage5_Controller.q[5]) {
            ti.Talk(3); // 자! 어서 누워야지 뭐해
		}
		else if (other.CompareTag ("Player") && Stage5_Controller.q[8] && !Stage5_Controller.q[9]) {
            print("시무룩해함");
            ti.Talk(20); // 남친을 정말 사랑하나봐
			bed.enabled = true;
			door.enabled = false;
            //Stage5_Controller.q10 = true;
			Stage5_Controller.q [9] = true;
		}
        else if (other.CompareTag ("Player") && Stage5_Controller.q[7] && !Stage5_Controller.q[8]) {
            //	Auto_ItemUse ();
			print ("방문 닫히는 소리");
            ti.Talk(14); // 주인님이 많이 피곤하신가봐.
			bed.enabled = false;
			door.enabled = true;
            //Stage5_Controller.q9 = true;
		}

        else if (other.CompareTag ("Player") && Stage5_Controller.q[10] && !Stage5_Controller.q[53]) {
			print ("잠든다");
            ti.Talk(26); // "..." + "zzz"
			mbr.enabled = false;
			bed.enabled = false;
			door.enabled = true;
			//StartCoroutine (Fadeout_black ());
			tr4 = true;
		}

        else if (other.CompareTag ("Player") && Stage5_Controller.q[13] && !Stage5_Controller.q[14]) {
			//ti.currLineArr [0] = 19;
			//ti.NPC_Say_yeah ("별감");
			//Stage5_Controller.q15 = true;
			Stage5_Controller.q [14] = true;
		}

        else if (other.CompareTag ("Player") && Stage5_Controller.q [12] && !Stage5_Controller.q [13]) { // 이본 확인하러 가는 거
            bed.enabled = false;
            door.enabled = false;
            snack.enabled = true;
            _dogsnack.SetActive(true);
            portalto5_1.enabled = false;
            ti.Talk(34); // 나 나갔아 올게!
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
            ti.Talk(); // 코코야 어서 자야지
			q4_a1 = true;
		}
        mbr.enabled = false;
	}

	void Q5_Until_ivonOut(){
		if (!q5_a1) {
			StartCoroutine (Kick_star ());
			q5_a1 = true;
		}
		else if (Stage5_Controller.q[52] && !q5_a2) {
			_ivon.SetActive (false);
            //Stage5_Controller.q7 = true;
            //_stardoll.transform.position = _stardoll_afterused.transform.position;
            q5_a2 = true;
        }
        else if (q5_a2 && !q5_a3)
        {
            _stardoll.SetActive(true);
            _stardoll_afterused.SetActive(false);
            bed.enabled = false; // 아이템 사용 시 이 콜라이더가 아이템 사용을 막음.
            ti.Talk(10); // 쿠션 다시 가져와..
            q5_a3 = true;
            portalto5_1.enabled = false;
        }
    }

    void Q6_get_star()
    {
        if (_Bed.used_or_not_for_retry && !q6_a1)
        {
            print("사용");
            // _stardoll_afterused.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            _stardoll_afterused.transform.position = new Vector2(-3.66f, -3.38f);
            _stardoll_afterused.SetActive(true);
            Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), player.GetComponent<PolygonCollider2D>());
            Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), bed);
            Physics2D.IgnoreCollision(_stardoll_afterused.GetComponent<BoxCollider2D>(), dogBed);

            bed.enabled = true;
            q6_a1 = true;
        }
        else if (q6_a1)
        {
            Stage5_Controller.q[7] = true; // 아무것도 없는 상태..
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }
    }

    void Q7_5_After_Fadein()
    {
        if (!q7_a1)
        {
            print("코코 눈 뜸");
            ti.Talk(29); // 문 여는 소리 같은데?
            q7_a1 = true;
        }
    }

	void Q9_snack_gotoBall(){
		if (!q9_a1 && ic._item_name_list[2] == "DogSnack") {
            ti.Talk(39); // 간식 먹어 코코야	
            q9_a1 = true;
		}
		else if (ic._now_used_item == "DogSnack" && !q9_a2) {
            _dogsnack_not_item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dogsnack_not_item.transform.position = new Vector3(_dogsnack_not_item.transform.position.x, _dogsnack_not_item.transform.position.y - 1f, 6.5f);
            _dogsnack_not_item.SetActive(true);

            ti.Talk(41); // 왜 안먹어 코코야
            print("힘이 없게 짖는 코코");
			q9_a2 = true;
            portalto5_1.enabled = true;
		}
	}

	IEnumerator Kick_star(){
		mbr.enabled = false;
		while (true) {
			print ("발로 참");
            //	_stardoll.SetActive (true);
            rb2D.velocity = new Vector2(5, 5);
			yield return new WaitForSeconds (2f);
            ti.Talk(5); // 누가 그렇게 차버리래!!
         //   itemScript.enabled = true;
			break;
		}
	}

	/*IEnumerator sadCoco(){
		mbr.enabled = false;
		print ("시무룩해함");
		while (true) {
			yield return new WaitForSeconds (2f);
			ti.currLineArr [0] = 15;
			ti.NPC_Say_yeah ("별감");
			break;
		}
	}*/

	IEnumerator Fadeout_black(){
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
        Stage5_Controller.q[11] = true;
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
		//Stage5_Controller.q [12] = true;
	}
}
