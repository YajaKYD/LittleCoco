using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_4_GameController : Controller {

	public Transform from_5_5;
	public Transform from_5_6;
	public Transform from_5_7;
	public Transform from_5_8;
    public Transform cluePlace;
    public Camera main_Camera;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private Text_Importer2 ti;
	private Item_Controller ic;

    public Image _ivon_textbox;
    public Text _ivon_text;

    public GameObject Ivon;
    public GameObject portal_5_1;
	public SpriteRenderer _blackout;
    private GameObject newsStand; // 뉴스스탠드 나중에 나타나게 하기 위함.
    private GameObject paper;
    private GameObject ddong;
    private GameObject IvonTextPos;

    public BoxCollider2D portal_5_7_1;
    public GameObject Goto_5_7;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private float velocity = 0.0f;
    private Vector3 velocity2;
    private float smoothTime = 0.7f; // For Camera move
    private float smoothTime2 = 0.5f; // For Camera move

    private bool q1a1 = false;	private bool q1a2 = false;	private bool q1a3 = false;	private bool q1a4 = false;
	private bool q1a6 = false;    private bool q1a8 = false;    private bool q1a9 = false;    private bool q1a10 = false;
    private bool q1a14 = false;    private bool q1a15 = false;    private bool q1a16 = false;    private bool q1a17 = false; private bool q1a18 = false;

    private bool q2a1 = false;

    private bool q3a1 = false;    private bool q3a2 = false;    private bool q3a3 = false;

    private bool q4a1 = false;

    private bool q5a1 = false;

    private bool q6a1 = false;    private bool q6a5 = false;
    private bool q6a6 = false;    private bool q6a7 = false;    private bool q6a8 = false;    private bool q6a9 = false;    private bool q6a10 = false;
    private bool q6a16 = false;    private bool q6a17 = false;

    void Awake(){
        sceneNo = 54;
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

        //player.transform.position = start_pos.position;
        newsStand = GameObject.Find("Newsstand");
        paper = GameObject.Find("paper");
        IvonTextPos = GameObject.Find("IvonTextPos");
        IvonTextPos.SetActive(false);
    }

	void Start(){
        _ivon_textbox = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Image>();
        _ivon_text = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetChild(0).GetComponent<Text>();
        _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
        _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (GetComponent<Load_data> ()._where_are_you_from == 37) {
			player.transform.position = from_5_5.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 38) {
			player.transform.position = from_5_6.position; // 자동으로 씬 이동했을 때 위치 수정 필요...
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 41 || GetComponent<Load_data>()._where_are_you_from == 39) {
			player.transform.position = from_5_7.position;
		}
		if (GetComponent<Load_data> ()._where_are_you_from == 40) {
			player.transform.position = from_5_8.position;
		}
        if (GetComponent<Load_data>()._where_are_you_from == 42)
        {
            player.transform.position = from_5_7.position;
            portal_5_7_1.GetComponent<Portal_Controller>()._To_Scene = 39;
        }
        if (Stage5_Controller.q[25] && !Stage5_Controller.q[26])
        {
            newsStand.SetActive(false); // 처음에는 가판대 없어야함.    
            player.transform.position = from_5_5.position;
            // save point //
            //Save_Script.Save_Now_Point();
            // save point //
        }
        if (Stage5_Controller.q[26] && !Stage5_Controller.q[27])
        { 
            GameObject k = (GameObject)Instantiate(Resources.Load("Prefabs/dogddong"));
            k.transform.position = cluePlace.transform.position;
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), k.GetComponent<Collider2D>(), true);
            ddong = GameObject.Find("dogddong(Clone)");
            newsStand.SetActive(false);
        }
        if (Stage5_Controller.q [29]) // 신문지 안 먹고 다른 씬으로 이동했다가 돌아온 경우 + 그 이후
        {
            //player.transform.position = from_5_5.position;
            newsStand.SetActive(true);
            paper.SetActive(false);
        }
        if (Stage5_Controller.q[35])
        {
            portal_5_7_1.enabled = true;
        }

        ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(54);
        if (Stage5_Controller.q [29] && !Stage5_Controller.q [31])
        {
            player.transform.position = new Vector2(cluePlace.position.x - 10f, player.transform.position.y);
        }

        if (!Stage5_Controller.q[39] || Stage5_Controller.q[48])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
        if (Stage5_Controller.q[42])
        {
            // save point //
            //Save_Script.Save_Now_Point();
            // save point //
            Goto_5_7.GetComponent<Portal_Controller>()._To_Scene = 39; // 쓰레기더미도 아무것도 없는 순수 5-7 Scene으로.
        }
        if (Stage5_Controller.q[48])
        {
            IvonTextPos.SetActive(true);
            // save point //
            //Save_Script.Save_Now_Point();
            // save point //
            player.transform.position = new Vector2(0.82f, -2.655f);
            Ivon.SetActive(true);
            newsStand.SetActive(false);
        }
       
    }

	void Update(){
        if (Stage5_Controller.q [25] && !Stage5_Controller.q[26]) { 
			Q1_gotoPaper ();
		}
		else if (Stage5_Controller.q [27] && !Stage5_Controller.q [28]) {
			Q2_Fadeout ();
		}
		else if (Stage5_Controller.q [28] && !Stage5_Controller.q [29]) {
            Q3_FadeIn ();
		}
        else if (Stage5_Controller.q [29] && !Stage5_Controller.q [31])
        {
            Q4_getPaper();
        }
        else if (Stage5_Controller.q[34] && !Stage5_Controller.q[35])
        {
            Q5_TalkBefore5_7_1();
        }
        else if (Stage5_Controller.q[35] && !Stage5_Controller.q[36])
        {
            portal_5_7_1.enabled = true;
        }
        else if (Stage5_Controller.q[48] && !Stage5_Controller.q[49])
        {
            Q6_TalkBefore5_1();
        }
        else if (Stage5_Controller.q[49] && !Stage5_Controller.q[50])
        { 
            portal_5_1.transform.position = player.transform.position;
        }
    }

	void Q1_gotoPaper(){
		if (!q1a1) {
            mbr.enabled = false;
            ti.Talk(); // 오 여긴 비교적 한적해
			q1a1 = true;
			//StartCoroutine (Coco_ddong ());
		}
        else if (Stage5_Controller.q[63] && !q1a2 && player.transform.position.x <= cluePlace.transform.position.x)
        {
            ti.Talk(4); // Coco: !
            q1a2 = true;
        }
        else if (Stage5_Controller.q[64] && !q1a3)
        {
            StartCoroutine(Coco_ddong_ready());
        }
        else if (q1a3 && !q1a4)
        {
            ti.Talk(6); // 그래 싸라 싸...
            q1a4 = true;
        }
        else if (Stage5_Controller.q[65] && !q1a6)
        {
            print("똥을 싸며 부르르 떨고 싼 후 원래 자세로 컴백");
            StartCoroutine(Coco_ddong_shot_after());
            q1a6 = true;
        }
        else if (Stage5_Controller.q[66] && !q1a8)
        {
            StartCoroutine(Hide_ddong());
            q1a8 = true;
        }
        else if (q1a9 && !q1a10)
        {
            ti.Talk(12); // 똥 덮을 수 있는....
            q1a10 = true;
        }
        else if (Stage5_Controller.q[67] && !q1a14) // 카메라 신문지 비춰주는 거 왼쪽으로
        {
            StartCoroutine(Left_Camera_Move());
            q1a15 = true;
        }
        else if (q1a14 && q1a15 && !q1a16) // 다시 플레이어 비춰주는 거 오른쪽으로
        {
            StartCoroutine(Right_Camera_Move());
            q1a17 = true;
        }
        else if (q1a16 && q1a17 && !q1a18)
        {
            main_Camera.GetComponent<CameraManager>().enabled = true;
            ti.Talk(17); // 음..신문지로 가려만 놓자.
            q1a18 = true;            
        }
	}

	void Q2_Fadeout(){
		if (!q2a1) {
            ti.Talk(19); //야야 조심해
			q2a1 = true;
            newsStand.transform.position = paper.transform.position + new Vector3(0f, 1.5f, 0); // 신문을 가판대로 대체
        }
		else if (Stage5_Controller.q[68]) {
			print ("뭉치덮침");
			StartCoroutine (Fadeout_black ());
			Stage5_Controller.q [28] = true;
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
            // save point //
            //Save_Script.Save_Now_Point();
            // save point //
            ti.Talk(21); // 신문 뭉치 뒤에 가판이 있었나
            q3a3 = true;
        }
    }

	void Q4_getPaper(){ // 신문지 안 먹고 똥 자리 갔을 때는 어쩌지?
        if (!q4a1 && player.transform.position.x >= cluePlace.position.x)
        {
            Save_Script.Save_Now_Point();
            ti.Talk(28); // 어 똥이 왜 없지
            q4a1 = true;
        }
	}

	void Q5_TalkBefore5_7_1 ()
    {
        if (!q5a1 && player.transform.position.x >= from_5_7.transform.position.x)
        {
            ti.Talk(33); // 뭔가 잇겟는걸
            q5a1 = true;
        }
	}

    void Q6_TalkBefore5_1()
    {
        if (!q6a1)
        {
            ti.Talk(36); // 후 화분은 결국
            q6a1 = true;
        }
        else if (Stage5_Controller.q[79] && !q6a5)
        {
            StartCoroutine(Ivon_Camera_Move());
            q6a6 = true;
        }
        else if (q6a5 && q6a6 && !q6a7)
        {
            ti.Talk(42); // 으악 똥이잖아.
            //_ivon_textbox.transform.parent.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //_ivon_textbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            q6a7 = true;
        }
        else if (Stage5_Controller.q[80] && !q6a8)
        {
            StartCoroutine(Coco_Camera_Move());
            q6a9 = true;
        }
        else if (q6a8 && q6a9 && !q6a10)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            ti.Talk(44); // 주인님이다..!
            q6a10 = true;
        }
        else if (Stage5_Controller.q[81] && !q6a16)
        {
            mbr.enabled = false;
            if (Ivon.transform.position.x >= 9f)
            {
                IvonTextPos.transform.position = new Vector2(IvonTextPos.transform.position.x - 0.1f, IvonTextPos.transform.position.y);
                Ivon.transform.position = new Vector2(Ivon.transform.position.x - 0.1f, Ivon.transform.position.y);
            }
            else
            {
                q6a16 = true;
            }   
        }
        else if (q6a16 && !q6a17)
        {
            ti.Talk(52); // 코코야 어디갓엇엉
            q6a17 = true;
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
            ti.Talk(10); // Music~
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
            mbr.enabled = false;
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

    IEnumerator Ivon_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, Ivon.transform.position.x - 5f, ref velocity, smoothTime2, 10f, Time.deltaTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("카메라가 이본에게로");
            yield return new WaitForSeconds(3f);
            q6a5 = true;
            break;
        }
    }

    IEnumerator Coco_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, player.transform.position.x, ref velocity, smoothTime2, 10f, Time.deltaTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("카메라가 코코에게로");
            yield return new WaitForSeconds(3f);
            q6a8 = true;
            break;
        }
    }
}
