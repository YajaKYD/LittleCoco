using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_8_GameController : Controller {
    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;

    private Text_Importer2 ti;
    private Item_Controller ic;
    private float velocity = 0.0f;
    private float smoothTime = 0.7f; // For Camera move

    public SpriteRenderer _blackout;
    public Camera main_Camera;
    public GameObject Racoon;
    public BoxCollider2D portaltodiary;
    public BoxCollider2D portalto6_4;
    public Sprite cardImage;

    public GameObject on_off;
    public Image itemButton0; public Image item0;
    public Image itemButton1; public Image item1;
    public Image itemButton2; public Image itemButton3; public Image itemButton4;
    public GameObject joystick; public GameObject jumpButton;

    private bool q8a1,
        q8a2, q8a3, q8a4, q8a5,
        q8a6,
        q8a7, q8a8;

    void Awake()
    {
        on_off = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(0).gameObject;
        itemButton0 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        item0 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        itemButton1 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
        item1 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>();
        itemButton2 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
        itemButton3 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>();
        itemButton4 = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(0).GetChild(1).GetChild(4).GetComponent<Image>();
        joystick = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(3).gameObject;
        jumpButton = GameObject.FindGameObjectWithTag("Item_Canvas").transform.GetChild(1).gameObject;
        on_off.SetActive(true); joystick.SetActive(true); jumpButton.SetActive(true);
        itemButton0.enabled = true; item0.enabled = true; itemButton1.enabled = true; item1.enabled = true;
        itemButton2.enabled = true; itemButton3.enabled = true; itemButton4.enabled = true;

        sceneNo = 68;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ic._now_used_item = "";

        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(68);
        
        if (!Stage6_Controller.q[36])
        {
            player.transform.position = start_pos.position;
            ti.Talk();
        }
        else if (!Stage6_Controller.q[46])
        {
            q8a1 = true;
            player.transform.position = new Vector2(3.36f, -3.725002f);
            ti.Talk(39); // 부적은 줄수없어 썩 돌아가라
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Stage6_Controller.q[39] && !Stage6_Controller.q[40])
        {
            ti.Talk(11); // 호오 너희는?
        }
    }

    void Update()
    {
        if (!q8a1 && player.transform.position.x >= -8f)
        {
            ti.Talk(7); // !
            q8a1 = true;
        }
        else if (Stage6_Controller.q[37] && !Stage6_Controller.q[38])
        {
            CameraMoving();
        }
        else if (Stage6_Controller.q[38] && !Stage6_Controller.q[39])
        {
            if (!q8a6)
            {
                ti.Talk(9); // 너굴맨이다!
                q8a6 = true;
            }
        }
        else if (Stage6_Controller.q[40] && !Stage6_Controller.q[41])
        {
            player.GetComponent<Outline>().used_or_not_for_retry = false;
            if (ic._now_used_item == "Diary")
            {
                portaltodiary.enabled = true;
                portaltodiary.transform.position = player.transform.position;
                Stage6_Controller.q[41] = true;
            }
        }
        else if (Stage6_Controller.q[46] && !Stage6_Controller.q[47] && ic._now_used_item == "Pot")
        {
            if (!q8a7)
            {
                ti.Talk(46); // 어 화분 내가 분명히 깨드렷는데
                q8a7 = true;
            }
        }
        else if (Stage6_Controller.q[47] && !Stage6_Controller.q[48])
        {
            if (ic.Get_Item_Auto(19, cardImage))
            {
                Stage6_Controller.q[48] = true;
                ti.Talk(65); // 아싸 이걸로 집가면되는거지?
            }
        }
        else if (Stage6_Controller.q[49])
        {
            portalto6_4.transform.position = player.transform.position;
        }
    }

    void CameraMoving()
    {
        if (!q8a2)
        {
            StartCoroutine(Right_Camera_Move());
            q8a3 = true;
        }
        else if (q8a2 && q8a3 && !q8a4)
        {
            StartCoroutine(Left_Camera_Move());
            q8a5 = true;
        }
        else if (q8a4 && q8a5)
        {
            main_Camera.GetComponent<CameraManager>().enabled = true;
            Stage6_Controller.q[38] = true;
        }
    }

    IEnumerator Right_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, Racoon.transform.position.x, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            //print("왼쪽");
            yield return new WaitForSeconds(3f);
            q8a2 = true;
            break;
        }
    }

    IEnumerator Left_Camera_Move()
    {
        mbr.enabled = false;
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(main_Camera.transform.position.x, player.transform.position.x, ref velocity, smoothTime);
            main_Camera.transform.position = new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            //print("왼쪽");
            yield return new WaitForSeconds(3f);
            q8a4 = true;
            break;
        }
    }
}
