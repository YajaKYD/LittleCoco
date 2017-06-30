using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_4_GameController : MonoBehaviour {

	public Transform from_5_5;
	public Transform from_5_6;
	public Transform from_5_7;
	public Transform from_5_8;
    public Transform cluePlace;
    public Camera main_Camera;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
    private GameObject _coco_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	public SpriteRenderer _blackout;
    private GameObject newsStand; // 뉴스스탠드 나중에 나타나게 하기 위함.
    private GameObject paper;
    private GameObject ddong;

    public BoxCollider2D portal_5_7_1;

    private float velocity = 0.0f;
    private float smoothTime = 0.7f; // For Camera move

	private bool q1a1 = false;
	private bool q1a2 = false;
	private bool q1a3 = false;
	private bool q1a4 = false;
	private bool q1a5 = false;
	private bool q1a6 = false;
    private bool q1a7 = false;
    private bool q1a8 = false;
    private bool q1a9 = false;
    private bool q1a10 = false;
    private bool q1a11 = false;
    private bool q1a12 = false;
    private bool q1a13 = false;
    private bool q1a14 = false;
    private bool q1a15 = false;
    private bool q1a16 = false;
    private bool q1a17 = false;
    private bool q1a18 = false;
    private bool q2a1 = false;
	private bool q2a2 = false;
	private bool q2a3 = false;
    private bool q3a1 = false;
    private bool q3a2 = false;
    private bool q3a3 = false;
    private bool q3a4 = false;
    private bool q3a5 = false;
    private bool q3a6 = false;
    private bool q3a7 = false;
    private bool q3a8 = false;
    private bool q3a9 = false;
    private bool q4a1 = false;
    private bool q4a2 = false;
    private bool q4a3 = false;
    private bool q5a1 = false;
    private bool q5a2 = false;

    void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

        //player.transform.position = start_pos.position;
        newsStand = GameObject.Find("Newsstand");
        paper = GameObject.Find("paper");
	}

	void Start(){

        if (GetComponent<Load_data> ()._where_are_you_from == 37) {
			player.transform.position = from_5_5.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 38) {
			player.transform.position = from_5_6.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 41) {
			player.transform.position = from_5_7.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 40) {
			player.transform.position = from_5_8.position;
		}
        if (Stage5_Controller._Stage5_Quest[25] && !Stage5_Controller._Stage5_Quest[26])
        {
            newsStand.SetActive(false); // 처음에는 가판대 없어야함.    
            player.transform.position = from_5_5.position;
            // save point //
            Save_Script.Save_Now_Point();
            // save point //
        }
        if (Stage5_Controller._Stage5_Quest [29] && !Stage5_Controller._Stage5_Quest [30]) // 신문지 안 먹고 다른 씬으로 이동했다가 돌아온 경우
        {
            player.transform.position = from_5_5.position;
            newsStand.SetActive(true);
            paper.SetActive(false);
        }

        ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];
        _coco_textbox = ti._text_boxes [2];

	}

	void Update(){
        if (Stage5_Controller._Stage5_Quest [25] && !Stage5_Controller._Stage5_Quest[26]) { 
			Q1_gotoPaper ();
		}
		if (Stage5_Controller._Stage5_Quest [27] && !Stage5_Controller._Stage5_Quest [28]) {
			Q2_Fadeout ();
		}
		if (Stage5_Controller._Stage5_Quest [28] && !Stage5_Controller._Stage5_Quest [29]) {
            Q3_FadeIn ();
		}
        if (Stage5_Controller._Stage5_Quest [30] && !Stage5_Controller._Stage5_Quest [31])
        {
            Q4_getPaper();
        }
        if (Stage5_Controller._Stage5_Quest[34] && !Stage5_Controller._Stage5_Quest[35])
        {
            Q5_TalkBefore5_7_1();
        }

    }

	void Q1_gotoPaper(){
		if (!q1a1) {
            mbr.enabled = false;
			ti.currLineArr [0] = 89;//한적하네
			ti.NPC_Say_yeah("별감");
			q1a1 = true;
			//StartCoroutine (Coco_ddong ());
		}
        else if (q1a1 && !q1a2 && !_star_textbox.activeSelf && player.transform.position.x <= cluePlace.transform.position.x)
        {
            ti.currLineArr[2] = 36;
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        else if (q1a2 && !q1a3 && !_coco_textbox.activeSelf)
        {
            StartCoroutine(Coco_ddong_ready());
        }
        else if (q1a3 && !q1a4)
        {
            ti.currLineArr[0] = 92; // 하.. 그래 싸라 싸
            ti.NPC_Say_yeah("별감");
            q1a4 = true;
        }
        else if (q1a4 && !q1a5 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 36; // 힘주며 싸는 "!"
            ti.NPC_Say_yeah("코코");
            q1a5 = true;
        }
        else if (q1a5 && !q1a6 && !_coco_textbox.activeSelf)
        {
            print("똥을 싸며 부르르 떨고 싼 후 원래 자세로 컴백");
            StartCoroutine(Coco_ddong_shot_after());
            q1a6 = true;
        }
        else if (q1a6 && q1a7 && !q1a8 && !_coco_textbox.activeSelf)
        {
            StartCoroutine(Hide_ddong());
            q1a8 = true;
        }
        else if (q1a8 && q1a9 && !q1a10)
        {
            ti.currLineArr[0] = 95; // 똥 덮을 수 있는...
            ti.NPC_Say_yeah("별감");
            q1a10 = true;
        }
        else if (q1a10 && !q1a11 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 40;
            ti.NPC_Say_yeah("코코");
            q1a11 = true;
        }
        else if (q1a11 && !q1a12 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 97; // 저 신문지?
            ti.NPC_Say_yeah("별감");
            q1a12 = true;
        }
        else if (q1a12 && !q1a13 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 40;
            ti.NPC_Say_yeah("코코");
            q1a13 = true;
        }
        else if (q1a13 && !q1a14 && !_coco_textbox.activeSelf) // 카메라 신문지 비춰주는 거 왼쪽으로
        {
            StartCoroutine(Left_Camera_Move());
            q1a15 = true;
        }
        else if (q1a14 && q1a15 && !q1a16) // 다시 플레이어 비춰주는 거 오른쪽으로
        {
            StartCoroutine(Right_Camera_Move());
            q1a17 = true;
        }
        else if (q1a16 && q1a17)
        {
            main_Camera.GetComponent<CameraManager>().enabled = true;
            ti.currLineArr[0] = 99;
            ti.NPC_Say_yeah("별감");
            Stage5_Controller._Stage5_Quest[26] = true;
        }
		/*if (q1a3 && q1a6 && !q1a5 && !_star_textbox.activeSelf) {
			ti.currLineArr [0] = 63;//덮을거
			ti.NPC_Say_yeah("별감");
			q1a5 = true;
			Stage5_Controller._Stage5_Quest [26] = true;
		}*/

	}

	void Q2_Fadeout(){
		if (!q2a1) {
			ti.currLineArr [0] = 101;//야야 조심해
			ti.NPC_Say_yeah("별감");
			q2a1 = true;
            newsStand.transform.position = paper.transform.position + new Vector3(0f, 1.5f, 0); // 신문을 가판대로 대체
        }
		if (q2a1 && !_star_textbox.activeSelf) {
			print ("뭉치덮침");
			StartCoroutine (Fadeout_black ());
			Stage5_Controller._Stage5_Quest [28] = true;
		} 
    }

	void Q3_FadeIn(){
        if (!q3a1)
        {
            StartCoroutine(Fadein_black());
            q3a1 = true;
        }
        else if (q3a1 && q3a2 && !q3a3)
        {
            ti.currLineArr[0] = 103; // 응 뭐지?
            ti.NPC_Say_yeah("별감");
            q3a3 = true;
        }
        else if (q3a3 && !q3a4 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 42;
            ti.NPC_Say_yeah("코코");
            q3a4 = true;
        }
        else if (q3a4 && !q3a5 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 105; // 신문 뭉치 뒤에 가판이?
            ti.NPC_Say_yeah("별감");
            q3a5 = true;
        }
        else if (q3a5 && !q3a6 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 44;
            ti.NPC_Say_yeah("코코");
            q3a6 = true;
        }
        else if (q3a6 && !q3a7 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 108; // 그래 ! 똥이 우선!
            ti.NPC_Say_yeah("별감");
            q3a7 = true;
            Stage5_Controller._Stage5_Quest[29] = true; // 신문지 탭하기 전까지의 퀘스트
            // save point //
            Save_Script.Save_Now_Point();
            // save point //
        }
    }

	void Q4_getPaper(){ // 신문지 안 먹고 똥 자리 갔을 때는 어쩌지?
        if (!q4a1 && player.transform.position.x >= cluePlace.position.x)
        {
            ti.currLineArr[0] = 110; // 어.. 똥이 왜 없지?
            ti.NPC_Say_yeah("별감");
            q4a1 = true;
        }
        else if (q4a1 && !q4a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 46;
            ti.NPC_Say_yeah("코코");
            q4a2 = true;
        }
        else if (q4a2 && !q4a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 112; // 어찌됐든 다행이야
            ti.NPC_Say_yeah("별감");
            q4a3 = true;
        }
        else if (q4a3 && !_star_textbox.activeSelf)
        {
            portal_5_7_1.enabled = (false);
            Stage5_Controller._Stage5_Quest[31] = true; // 신문지 얻고 똥 없는 거 발견한거까지 완료.
        }
	}

	void Q5_TalkBefore5_7_1 ()
    {
        if (!q5a1 && player.transform.position.x >= from_5_7.transform.position.x)
        {
            ti.currLineArr[2] = 54; // 오! 여기 뭔가 좀 있겠;;
            ti.NPC_Say_yeah("코코");
            q5a1 = true;
        }
        else if (q5a1 && !q5a2 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 133;
            ti.NPC_Say_yeah("별감");
            q5a2 = true;
        }
        else if (q5a2 && !_star_textbox.activeSelf)
        {
            portal_5_7_1.enabled = (true);
            Stage5_Controller._Stage5_Quest[35] = true; // 5_7_1로 들어가기 전 대사 완료.
        }
	}

	IEnumerator Coco_ddong_ready(){
		mbr.enabled = false;
		print ("CoCo Ani ready");
		while (true) {
			yield return new WaitForSeconds (2f);//ready
            q1a3 = true;
			break;
		}
	}
    
    IEnumerator Coco_ddong_shot_after()
    {
        mbr.enabled = false;
        print("Coco Ani Shot And After");
        while (true)
        {
            yield return new WaitForSeconds(3f);//shot
            GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/dogddong"));
            k.transform.position = player.transform.position;
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), k.GetComponent<Collider2D>(), true);
            yield return new WaitForSeconds(3f);//after
            ddong = GameObject.Find("dogddong(Clone)");
            print("쾌변해서 기분 좋음");
            ti.currLineArr[2] = 38; // Music~
            ti.NPC_Say_yeah("코코");
            q1a7 = true;
            break;
        }
    }

    IEnumerator Hide_ddong()
    {
        mbr.enabled = false;
        print("Hide ddong");
        while (true)
        {
            yield return new WaitForSeconds(2f);//ready
            q1a9 = true;
            break;
        }
    }

    IEnumerator Left_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, paper.transform.position.x, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("왼쪽");
            yield return new WaitForSeconds(3f);
            q1a14 = true;
            break;
        }
    }

    IEnumerator Right_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, player.transform.position.x, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("오른쪽");
            yield return new WaitForSeconds(3f);
            q1a16 = true;
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
    }

    IEnumerator Fadein_black(){
		while (true) {
			yield return new WaitForSeconds (2f);
            newsStand.SetActive(true); // 가판대가 위치하도록
            paper.SetActive(false); // 신문지를 코코 밑으로
            ddong.SetActive(false); // 과거로 회귀했기 때문에 똥이 없어짐.
            print ("배경 바뀜, 가판대");
			break;
		}
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color bb = new Color(0, 0, 0, 1);
            bb.a = f;
            _blackout.color = bb;
            yield return null;
        }
		_blackout.color = new Color (0, 0, 0, 0);
        q3a2 = true;
    }

}
