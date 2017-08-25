using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_4_GameController : Controller {
    private GameObject player;
    private Transform start_pos;
    private Item_Controller ic;
    private Text_Importer2 aa;
    public Image _ivon_textbox;
    public Text _ivon_text;
    public GameObject IvonTextPos;
	private Vector3 IvonTextPosTemp;

    //public bool quest1_start1;
    public bool quest1_start2;
    //public bool quest1_processing;
    //public bool quest1_complete;
    public bool quest2_start1;
    public bool quest2_start2;

	private bool a1a1 = false;
	private bool a1a2 = false;
	private bool a2a1 = false;
	private bool a1a3 = false;
	private bool a1a4 = false;
    private bool a1a5 = false;
    private bool a1a6 = false;
    private bool a7 = false;
	private bool a8 = false;
	private bool a8a1, a8a2, a8a3, a8a4;
    private bool q9 = false;

	public GameObject quest1_gaugePrefab, quest1_gauge, quest1_gaugeBackPrefab, quest1_gaugeBack;
    private Vector3 tempPos;
    public GameObject quest2_ball1;
    public GameObject quest2_ball2;
    public Transform _Ivon_Position;
    public GameObject portal3_5;
	public GameObject portalEnd;
	public GameObject background_far;
	public GameObject background_far1;
	public Sprite background_far_img;

    void Awake()
    {
        sceneNo = 34;
        player = GameObject.Find("Player");
        start_pos = GameObject.Find("Start_Pos").transform;
        player.transform.position = start_pos.position;
		_Ivon_Position = GameObject.FindWithTag("NPC").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

		player.transform.localScale = new Vector3 (1f, 1f, player.transform.localScale.z);
    }

	void Start(){
		aa = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        aa.Import(34);
        _ivon_textbox = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Image>();
        _ivon_text = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetChild(0).GetComponent<Text>();

        if (Stage3_Controller.q[8] && !Stage3_Controller.q[9]) {
			//Stage3 Save point 2//
			Save_Script.Save_Now_Point();
			//Stage3 Save point 2//
			//집에서 나옴. 달리기미션 not clear
			//quest1_start1 = true;
			GameObject _room = GameObject.FindWithTag("Controller").transform.GetChild (0).gameObject;
			GameObject _park = GameObject.FindWithTag("Controller").transform.GetChild (1).gameObject;
			_room.SetActive (false);
			_park.SetActive(true);
		}
		if (Stage3_Controller.q [9] && !Stage3_Controller.q [10]) {
			quest1_gauge = Instantiate(quest1_gaugePrefab, new Vector3(-500,300,0), Quaternion.identity) as GameObject;
			quest1_gauge.transform.SetParent(GameObject.FindWithTag("Item_Canvas").transform, false);
			quest1_gaugeBack = Instantiate(quest1_gaugeBackPrefab, new Vector3(-500,300,0), Quaternion.identity) as GameObject;
			quest1_gaugeBack.transform.SetParent(GameObject.FindWithTag("Item_Canvas").transform, false);
		}
		IvonTextPos = GameObject.Find("IvonTextPos");
		IvonTextPosTemp = IvonTextPos.transform.position;
		//Debug.Log (IvonTextPosTemp);

		if (Stage3_Controller.q [19]) {
			background_far.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			background_far1.GetComponent<SpriteRenderer> ().sprite = background_far_img;
		}
	}
    
    void OnTriggerEnter2D()
    {
		if (Stage3_Controller.q[8] && !Stage3_Controller.q[9])
        {
            aa.Talk(); // 집이 많이 답답했지 코코야
            //ivon_textbox = GameObject.Find("이본_text");
            quest1_start2 = true;
        }

		if (Stage3_Controller.q[9] && !Stage3_Controller.q[10])
        {
            if (quest2_start1 && Stage3_Controller.q[24])
            {
				ModifySpeechBubble ();
                aa.Talk(8); // 공놀이 시작
				//Stage3_Controller.q[10] = true;
                quest2_start1 = false;
            }
        }

		if (Stage3_Controller.q[11] && !Stage3_Controller.q[12])
        {
            a1a3 = checkBall(a1a3);
            
            if (a1a3 && !a1a4)
            {
				ModifySpeechBubble();
                aa.Talk(10); //잘했어 한번 더 물ㅓ와라
				//ivon_textbox = GameObject.Find("이본_text");
                a1a4 = true;
            }
        }

		if (Stage3_Controller.q[12] && !Stage3_Controller.q[13])
        {
            a1a6 = checkBall(a1a6);

            if (!a1a5 && a1a6)
            {
				ModifySpeechBubble();
                aa.Talk(12); // 공놀이 끝
				//ivon_textbox = GameObject.Find("이본_text");
                quest1_gauge.SetActive(true);
				quest1_gaugeBack.SetActive (true);
                a1a5 = true;
            }
        }

		if (Stage3_Controller.q[13] && !Stage3_Controller.q[14])
        {
            if (!a7)
            {
				ModifySpeechBubble();
                aa.Talk(16);//코코 착하지 마음껏 놀았니? (가서 다른 강아지들이랑 놀고오렴)
				//ivon_textbox = GameObject.Find("이본_text");
                portal3_5.GetComponent<BoxCollider2D>().enabled = true;
                a7 = true;
            }
        }

		if (Stage3_Controller.q[19]) {
			a8 = true;
		}
    }

	void ModifySpeechBubble(){
		if ((player.transform.position.x - transform.position.x) >= 0) { //플레이어가 오른쪽
			IvonTextPos.transform.position = new Vector3 (7.4f, 2.71f, 0);
            _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //ivon_textbox.transform.Rotate (new Vector3 (0, 180, 0));
            //ivon_textbox.transform.parent.gameObject.transform.Rotate (new Vector3 (0, 180, 0));
        } else {
			IvonTextPos.transform.position = IvonTextPosTemp;
            _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //ivon_textbox.transform.Rotate (new Vector3 (0, 180, 0));
            //ivon_textbox.transform.parent.gameObject.transform.Rotate (new Vector3 (0, 180, 0));
        }
	}

	void Update () {

		if (Stage3_Controller.q[8] && !Stage3_Controller.q[9])
        {
			Q6_Running_Start (); //게이지 생김.
        }

		if (Stage3_Controller.q[9] && !Stage3_Controller.q[10])
        {
			Q7_Running (); 	//게이지 줄이고, 코코 부름.
        }

		if (Stage3_Controller.q[10] && !Stage3_Controller.q[11]) {
			Q8_BallPlayStart ();
		}

		if (Stage3_Controller.q[11] && !Stage3_Controller.q[12]) {
			Q9_GetABall_Onemore ();
		}

		if (Stage3_Controller.q[12] && !Stage3_Controller.q[13])
        {
            Q10_Running_Finish();
        }

		if (Stage3_Controller.q[19] && a8) {
			//aa.Import (15); // temp code
			Q13_Talk ();
		}
    }

	void OnDisable(){
		if (Stage3_Controller.q [19] && !_Ivon_Position.gameObject.GetComponent<SpriteRenderer> ().enabled) {
			Destroy (GameObject.FindWithTag ("Controller"));
			Destroy (aa.gameObject);
			Debug.Log ("stage3 dialouge destroyed");
			Selecting_stage._what_stage_now_cleared = 3;//4스테이지 오픈시킴
			PlayerPrefs.SetInt("Stage_Now_Cleared",Selecting_stage._what_stage_now_cleared);
			print ("Stage3 End");
		}
	}


	void Q6_Running_Start(){
		if (quest1_start2 && Stage3_Controller.q[23])
		{
			quest1_gauge = Instantiate(quest1_gaugePrefab, new Vector3(-418,400,0), Quaternion.identity) as GameObject;
			quest1_gauge.transform.SetParent(GameObject.FindWithTag("Item_Canvas").transform, false);
			quest1_gaugeBack = Instantiate(quest1_gaugeBackPrefab, new Vector3(-500,400,0), Quaternion.identity) as GameObject;
			quest1_gaugeBack.transform.SetParent(GameObject.FindWithTag("Item_Canvas").transform, false);
			Stage3_Controller.q[9] = true;
			Debug.Log ("ddd");
			quest1_start2 = false;
			tempPos = player.transform.position;
		}
	}

	void Q7_Running(){
        if (tempPos != player.transform.position)
        {
            quest1_gauge.transform.localScale = new Vector3(quest1_gauge.transform.localScale.x - Vector3.Distance(tempPos, player.transform.position) / 180f, 1, 1);
            tempPos = player.transform.position; // 맵의 가로길이가 30이라고 가정한 수치
        }

        if (quest1_gauge.transform.localScale.x <= 2f / 3f && !a2a1) // 게이지가 2/3가 되면
		{
			quest2_start1 = true;

			if ((player.transform.position.x - transform.position.x) >= 0) { //플레이어가 오른쪽
				IvonTextPos.transform.position = new Vector3 (7.4f, 2.71f, 0);
                _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
                _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            } else if ((player.transform.position.x - transform.position.x) >= -10) { // 왼쪽
			} else {
				IvonTextPos.transform.position = new Vector3 (player.transform.position.x + 7.3f, 2.71f, 0);
			}
			aa.Talk(5); // 코코 부름
			//ivon_textbox = GameObject.Find("이본_text");
			a2a1 = true;
		}
	}

	void Q8_BallPlayStart(){
		
		if (!a1a1) { //click해서 대사침.
			a1a2 = true;
			a1a1 = true;
		}

		if (a1a2 /*&& !ivon_textbox.activeSelf*/) {//윗 대사 종료
			//공던짐.
			_Ivon_Position = GameObject.FindWithTag("NPC").transform;
			if (Stage3_Controller.q[3]) {//1,2 or 1,3
				GameObject aa = (GameObject)Instantiate(Resources.Load("Prefabs/Hokyun/ball1"));
				aa.transform.position = _Ivon_Position.position;
				aa.GetComponent<Ball_popup> ().enabled = true;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), aa.GetComponent<Collider2D>(), false);
            } else {//2,3
				GameObject aa = (GameObject)Instantiate(Resources.Load("Prefabs/Hokyun/ball2"));
				aa.transform.position = _Ivon_Position.position;
				aa.GetComponent<Ball_popup> ().enabled = true;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), aa.GetComponent<Collider2D>(), false);
            }
			a1a2 = false;
			Stage3_Controller.q[11] = true;
            quest1_gauge.SetActive(false); // running pause
			quest1_gaugeBack.SetActive(false);
        }
        
    }

    void Q9_GetABall_Onemore(){
        
		if (a1a4 && Stage3_Controller.q[25]) {
			if (Stage3_Controller.q[3] && Stage3_Controller.q[4]) {
				GameObject aa = (GameObject)Instantiate(Resources.Load("Prefabs/Hokyun/ball2"));
				aa.transform.position = _Ivon_Position.position;
				aa.GetComponent<Ball_popup> ().enabled = true;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), aa.GetComponent<Collider2D>(), false);
            } else {
				GameObject aa = (GameObject)Instantiate(Resources.Load("Prefabs/Hokyun/ball3"));
				aa.transform.position = _Ivon_Position.position;
				aa.GetComponent<Ball_popup> ().enabled = true;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), aa.GetComponent<Collider2D>(), false);
            }
            a1a4 = false;
			Stage3_Controller.q[12] = true;
            //quest1_gauge.SetActive(false); // running pause
        }
        
    }

    void Q10_Running_Finish()
    {
		if (quest1_gauge != null && tempPos != player.transform.position)
        {
			quest1_gauge.transform.localScale = new Vector3(quest1_gauge.transform.localScale.x - Vector3.Distance(tempPos, player.transform.position) / 180f, 1, 1);
            tempPos = player.transform.position; // 맵의 가로길이가 30이라고 가정한 수치
        }

		if (quest1_gauge != null && quest1_gauge.transform.localScale.x <= 0)
        {
            Destroy(quest1_gauge); // 게이지 다달면 끝
			Destroy(quest1_gaugeBack);
			Stage3_Controller.q[13] = true;
        }
    }

	void Q13_Talk(){
		if (!a8a1) {
            aa.Talk(19); // 잘했어 코코~
			//ivon_textbox = GameObject.Find ("이본_text");
			a8a1 = true;
		}
		else if (Stage3_Controller.q[20]) {
			Debug.Log ("a8a5");
			portal3_5.GetComponent<BoxCollider2D> ().enabled = false;
			portalEnd.GetComponent<BoxCollider2D> ().enabled = true;
			_Ivon_Position.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			_Ivon_Position.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			a8 = false;
		}
			
	}

    bool checkBall(bool a)
    {
        if (!a)
        {
            for (int i = 0; i < ic._item_list.Length; i++)
            {
                if (ic._item_name_list[i] == "ball1") 
                {
                    a = true;
                    Use_Item("ball1");
                    return a;
                } else if (ic._item_name_list[i] == "ball2")
                {
                    a = true;
                    Use_Item("ball2");
                    return a;
                } else if(ic._item_name_list[i] == "ball3")
                {
                    a = true;
                    Use_Item("ball3");
                    return a;
                }
            }
        }
        return a;
    }

    void Use_Item(string name)
    {
        for (int i = 0; i < 5; i++)
        {
            if (ic._item_name_list[i] == name)
            {
                ic._item_name_list[i] = "";
                ic._usable_item[i] = false;
                ic._the_number_of_items[i] = 0;
                ic._interaction_object[i] = "";
                ic._consumable[i] = false;
                ic._item_list[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                ic._item_list[i].transform.parent.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0);
				ic._explanations [i] = "";
				ic.cant_pick_during_using = true;
            }
        }
    }

}
