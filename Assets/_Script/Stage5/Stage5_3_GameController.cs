using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_3_GameController : MonoBehaviour {

    public Transform from_5_5;
    public Transform from_5_6;
    public Transform from_5_7;
    public Transform from_5_8;

    private Transform start_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
    private GameObject _coco_textbox;
	private GameObject _ivon_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

	public BoxCollider2D goto_5_5;
    public BoxCollider2D goto_5_7;
    public GameObject Goto_5_7;

	public GameObject warning;
    public GameObject trashTruck;
    public Camera main_Camera;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private float velocity = 0.0f;
    private float smoothTime = 0.7f; // For Camera move

    private bool q1a1 = false;
	private bool q1a2 = false;
	private bool q1a3 = false;
	private bool q1a4 = false;
	private bool q1a5 = false;
    private bool q1a6 = false;
    private bool q2a1 = false;
    private bool q2a2 = false;
    private bool q2a3 = false;
    private bool q2a4 = false;
    private bool q2a5 = false;
    private bool q2a6 = false;
    private bool q2a7 = false;
    private bool q2a8 = false;
    private bool q2a9 = false;
    private bool q2a10 = false;
    private bool q2a11 = false;
    private bool q3a1 = false;
    private bool q3a2 = false;
    private bool q3a3 = false;
    private bool q3a4 = false;
    private bool q4a1 = false;
    private bool q4a2 = false;
    private bool q4a3 = false;
    private bool q4a4 = false;

    void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;
	}

	void Start(){

		if (GetComponent<Load_data> ()._where_are_you_from == 40) {
			player.transform.position = from_5_8.position;
		}
        if (GetComponent<Load_data>()._where_are_you_from == 42)
        {
            player.transform.position = from_5_7.position;
        }

        ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];
        _coco_textbox = ti._text_boxes [2];

		if (Stage5_Controller._Stage5_Quest [24] && !Stage5_Controller._Stage5_Quest [25]) {
			Save_Script.Save_Now_Point ();
			//시작할 때 저장
		}
		if (Stage5_Controller._Stage5_Quest [25]) {
			goto_5_5.enabled = true;
		}
        if (Stage5_Controller._Stage5_Quest[31])
        {
            warning.SetActive(false); // 다시 돌아왔을 때 맨홀 뚜껑 없어짐.
        }
        if (!Stage5_Controller._Stage5_Quest[39])
        {
            trashTruck.SetActive(true);
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
        if (Stage5_Controller._Stage5_Quest[40])
        {
            goto_5_7.enabled = true;
        }
        if (Stage5_Controller._Stage5_Quest[42])
        {
            Goto_5_7.GetComponent<Portal_Controller>()._To_Scene = 39; // 쓰레기더미도 아무것도 없는 순수 5-7 Scene으로.
        }
    }

    void Update() {
        if (Stage5_Controller._Stage5_Quest[24] && !Stage5_Controller._Stage5_Quest[25]) {
            Q1_firstcon();
        }
        else if (Stage5_Controller._Stage5_Quest[31] && !Stage5_Controller._Stage5_Quest[32])
        {
            Q2_removed_manhole();
        }
        else if (Stage5_Controller._Stage5_Quest[32] && !Stage5_Controller._Stage5_Quest[33])
        {
            Q3_Pass_Portal_5_7();
        }
        else if (Stage5_Controller._Stage5_Quest[39] && !Stage5_Controller._Stage5_Quest[40])
        {
            Q4_Talk_Before_Entry(); // 5_7_2 입구 앞에서의 대화
        }
    }

	void Q1_firstcon(){
		if (!q1a1)
        {
            mbr.enabled = false;
            StartCoroutine (Warigari ());
			q1a1 = true;
		}
        if (q1a2 && !q1a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 32;
            ti.NPC_Say_yeah("코코");
            q1a3 = true;
        }
        if (q1a3 && !q1a4 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 78; // 뭐 이 상황에서..
            ti.NPC_Say_yeah("별감");
            q1a4 = true;
        }
        if (q1a4 && !q1a5 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 34;
            ti.NPC_Say_yeah("코코");
            q1a5 = true;
        }
        if (q1a5 && !q1a6 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 82;
            ti.NPC_Say_yeah("별감");
            q1a6 = true;
        }
		if (q1a6 && !_star_textbox.activeSelf) {
			goto_5_5.enabled = true;
			Stage5_Controller._Stage5_Quest [25] = true;
		}
	}

    void Q2_removed_manhole()
    {
        if (!q2a1)
        {
            ti.currLineArr[2] = 48;
            ti.NPC_Say_yeah("코코");
            q2a1 = true;
        }
        else if (q2a1 && !q2a2 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 115;
            ti.NPC_Say_yeah("별감");
            q2a2 = true;
        }
        else if (q2a2 && !q2a3 && !_star_textbox.activeSelf) // 중간 지점까지 카메라 훑어주기
        {
            StartCoroutine(Right_Camera_Move());
            q2a4 = true;
        }
        else if (q2a3 && q2a4 && !q2a5)
        {
            StartCoroutine(Left_Camera_Move());
            q2a6 = true;
        }
        else if (q2a5 && q2a6 && !q2a7)
        {
            main_Camera.GetComponent<CameraManager>().enabled = true;
            ti.currLineArr[0] = 117;
            ti.NPC_Say_yeah("별감");
            q2a7 = true;        
        }
        else if (q2a7 && !q2a8 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 48;
            ti.NPC_Say_yeah("코코");
            q2a8 = true;
        }
        else if (q2a8 && !q2a9 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 119;
            ti.NPC_Say_yeah("별감");
            q2a9 = true;
        }
        else if (q2a9 && !q2a10 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 50;
            ti.NPC_Say_yeah("코코");
            q2a10 = true;
        }
        else if (q2a10 && !q2a11 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 122;
            ti.NPC_Say_yeah("별감");
            q2a11 = true;
        }
        else if (q2a11 && !_star_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[32] = true; // 5_7_1로 가기 위해 5_8로 가야하기 전까지 완료.
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }
    }

    void Q3_Pass_Portal_5_7()
    {
        if (!q3a1 && player.transform.position.x >= from_5_7.position.x)
        {
            ti.currLineArr[2] = 50;
            ti.NPC_Say_yeah("코코");
            q3a1 = true;
        }
        else if (q3a1 && !q3a2 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 124; // 여기를..?
            ti.NPC_Say_yeah("별감");
            q3a2 = true;
        }
        else if (q3a2 && !q3a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 50;
            ti.NPC_Say_yeah("코코");
            q3a3 = true;
        }
        else if (q3a3 && !q3a4 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 126;
            ti.NPC_Say_yeah("별감");
            q3a4 = true;
        }
        else if (q3a4 && !_star_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[33] = true; // 5_7 포털 위치 지나갈 때 대화하는 거 끝냄. 이후 5_8로 가야됨.
        }
    }

    void Q4_Talk_Before_Entry()
    {
        if (!q4a1 && player.transform.position.x <= from_5_7.position.x)
        {
            ti.currLineArr[2] = 58;
            ti.NPC_Say_yeah("코코");
            q4a1 = true;
        }
        else if (q4a1 && !q4a2 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 152; // 오 뭐야 들어 갈 수 잇네
            ti.NPC_Say_yeah("별감");
            q4a2 = true;
        }
        else if (q4a2 && !q4a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 58;
            ti.NPC_Say_yeah("코코");
            q4a3 = true;
        }
        else if (q4a3 && !q4a4 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 156; // 아이 알겟어.
            ti.NPC_Say_yeah("별감");
            q4a4 = true;
        }
        else if (q4a4 && !_star_textbox.activeSelf)
        {
            goto_5_7.enabled = true;
            Stage5_Controller._Stage5_Quest[40] = true; // 5_7 입구 앞에서 대화가 끝나고 통로가 열림.
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }

    }

    IEnumerator Warigari(){
		print ("전전긍긍");
        mbr.enabled = false;
		while (true) {
            mbr.enabled = false;
            yield return new WaitForSeconds (2f);
			break;
		}
		ti.currLineArr [0] = 75; //도대체 어디로
		ti.NPC_Say_yeah("별감");
		q1a2 = true;
	}

    IEnumerator Right_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, from_5_6.transform.position.x + 3f, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("중간으로 비춰주기");
            yield return new WaitForSeconds(3f);
            q2a3 = true;
            break;
        }
    }

    IEnumerator Left_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, -9.639999f, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("다시 플레이어 비추기");
            yield return new WaitForSeconds(3f);
            q2a5 = true;
            break;
        }
    }
}
