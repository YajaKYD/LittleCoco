using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_3_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    public Transform from_5_5;
    public Transform from_5_6;
    public Transform from_5_7;
    public Transform from_5_8;

    private Transform start_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private Text_Importer2 ti;
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
    private bool q2a1 = false;
    private bool q2a3 = false;
    private bool q2a4 = false;
    private bool q2a5 = false;
    private bool q2a6 = false;
    private bool q2a7 = false;
    private bool q3a1 = false;
    private bool q4a1 = false;

    void Awake(){
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage5_3");

        sceneNo = 53;
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
        if (GetComponent<Load_data>()._where_are_you_from == 42 || GetComponent<Load_data>()._where_are_you_from == 39)
        {
            player.transform.position = from_5_7.position;
        }
        if (GetComponent<Load_data>()._where_are_you_from == 37)
        {
            player.transform.position = from_5_5.position;
        }
        ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(53);

		/*if (Stage5_Controller.q [24] && !Stage5_Controller.q [25]) {
			Save_Script.Save_Now_Point ();
			//시작할 때 저장
		}*/
		if (Stage5_Controller.q [25]) {
			goto_5_5.enabled = true;
		}
        if (Stage5_Controller.q[31])
        {
            warning.SetActive(false); // 다시 돌아왔을 때 맨홀 뚜껑 없어짐.
        }
        if (!Stage5_Controller.q[39])
        {
            trashTruck.SetActive(true);
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
        if (Stage5_Controller.q[40])
        {
            goto_5_7.enabled = true;
        }
        if (Stage5_Controller.q[42])
        {
            Goto_5_7.GetComponent<Portal_Controller>()._To_Scene = 39; // 쓰레기더미도 아무것도 없는 순수 5-7 Scene으로.
        }
    }

    void Update() {
        if (Stage5_Controller.q[24] && !Stage5_Controller.q[25]) {
            Q1_firstcon();
        }
        else if (Stage5_Controller.q[31] && !Stage5_Controller.q[32])
        {
            Q2_removed_manhole();
        }
        else if (Stage5_Controller.q[32] && !Stage5_Controller.q[33])
        {
            Q3_Pass_Portal_5_7();
        }
        else if (Stage5_Controller.q[39] && !Stage5_Controller.q[40])
        {
            Q4_Talk_Before_Entry(); // 5_7_2 입구 앞에서의 대화
        }
        else if (Stage5_Controller.q[40] && !Stage5_Controller.q[41])
        {
            goto_5_7.enabled = true;
        }
    }

	void Q1_firstcon(){
		if (!q1a1)
        {
            StartCoroutine (Warigari ());
			q1a1 = true;
		}
        mbr.enabled = false;
	}

    void Q2_removed_manhole()
    {
        if (!q2a1)
        {
            ti.Talk(11); //음?
            q2a1 = true;
        }
        else if (!Stage5_Controller.q[69]) mbr.enabled = false;
        else if (Stage5_Controller.q[69] && !q2a3) // 중간 지점까지 카메라 훑어주기
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
            ti.Talk(14); // 원래 이랬나..
            q2a7 = true;
        }
    }

    void Q3_Pass_Portal_5_7()
    {
        if (!q3a1 && player.transform.position.x >= from_5_7.position.x)
        {
            ti.Talk(21); //여기를... 쓰레기 수거통에 막혀서 못들어가
            q3a1 = true;
        }
    }

    void Q4_Talk_Before_Entry()
    {
        if (!q4a1 && player.transform.position.x <= from_5_7.position.x)
        {
            ti.Talk(27); // 오 뭐야 들어갈 수 있잖아
            q4a1 = true;
        }
    }

    IEnumerator Warigari(){
		print ("전전긍긍");
		while (true) {
            yield return new WaitForSeconds (2f);
			break;
		}
        ti.Talk(); // 도대체 어디로..
        goto_5_5.enabled = true;
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
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, -8.1f, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("다시 플레이어 비추기");
            yield return new WaitForSeconds(3f);
            q2a5 = true;
            break;
        }
    }
}
