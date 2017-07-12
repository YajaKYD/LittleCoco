using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_7_1_GameController : MonoBehaviour {

    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject _star_textbox;
    private GameObject _coco_textbox;
    private GameObject _ivon_textbox;
    public SpriteRenderer _blackout;
    private Text_Importer ti;
    private Item_Controller ic;

    public GameObject starDoll;
    public Image rememberScene;

    private GameObject trashHeap;
    private GameObject umbrella;
    public GameObject mission;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private bool q1a1 = false;
    private bool q1a2 = false;
    private bool q1a3 = false;
    private bool q1a4 = false;
    private bool q1a5 = false;
    private bool q1a6 = false;
    private bool q2a1 = false;
    private bool q2a2 = false;
    private bool q3a1 = false;
    private bool q4a1 = false;
    private bool q4a2 = false;
    private bool q4a3 = false;
    private bool q4a4 = false;
    private bool q4a5 = false;
    private bool q4a6 = false;
    private bool q4a7 = false;
    private bool q4a8 = false;
    private bool q4a9 = false;
    private bool q4a10 = false;
    private bool q4a11 = false;
    private bool q4a12 = false;
    private bool q4a13 = false;
    private bool q4a14 = false;
    private bool q4a15 = false;
    private bool q4a16 = false;
    private bool q4a17 = false;

    private float velocity = 0.0f;
    private float smoothTime = 1.5f; // For auto move

    void Awake()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();

        player.transform.position = start_pos.position;

        trashHeap = GameObject.Find("TrashHeap");
        umbrella = GameObject.Find("Umbrella");
    }

    void Start()
    {
        if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = start_pos.position;
        }

        _star_textbox = ti._text_boxes[0];
        _ivon_textbox = ti._text_boxes[1];
        _coco_textbox = ti._text_boxes[2];

        Physics2D.IgnoreCollision(starDoll.GetComponent<BoxCollider2D>(), trashHeap.GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(starDoll.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        
        if (!Stage5_Controller._Stage5_Quest[37])
        {
            trashHeap.layer = 2;
            starDoll.GetComponent<DragRigidBody2D>().enabled = false;
        }

        if (Stage5_Controller._Stage5_Quest[38])
        {
            starDoll.SetActive(false);
            trashHeap.layer = 2;
        }
        umbrella.SetActive(false);

        if (!Stage5_Controller._Stage5_Quest[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
    }

    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[35] && !Stage5_Controller._Stage5_Quest[36])
        {
            Q1_Find_Trash_Ani();
        }
        else if (Stage5_Controller._Stage5_Quest[36] && !Stage5_Controller._Stage5_Quest[37])
        {
            Q2_Start_Ani();
            mbr.enabled = false;
        }
        else if (Stage5_Controller._Stage5_Quest[37] && !Stage5_Controller._Stage5_Quest[38])
        {
            Q3_Before_Seven_Trash();
            mbr.enabled = false;
        }
        else if (Stage5_Controller._Stage5_Quest[38] && !Stage5_Controller._Stage5_Quest[39])
        {
            Q4_After_Seven_Trash();
            mbr.enabled = false;
        }
        else if (Stage5_Controller._Stage5_Quest[39])
        {
            mbr.enabled = true;
        }
    }

    void Q1_Find_Trash_Ani()
    {
        if (!q1a1)
        {
            ti.currLineArr[0] = 135; // 엇 저 인형은?
            ti.NPC_Say_yeah("별감");
            q1a1 = true;
        }
        else if (q1a1 && !q1a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 54;
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        else if (q1a2 && !q1a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 137; // 그래 한번 찾아보자
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        else if (q1a3 && !q1a4 && !_star_textbox.activeSelf)
        {
            mbr.Moving_Right(8f);
            if (player.transform.position.x >= -6.5f) q1a4 = true;
            //StartCoroutine(MoveToTrash());
            //q1a5 = true;
        }
        else if (q1a4)
        {
            Stage5_Controller._Stage5_Quest[36] = true;
        }
    }

    void Q2_Start_Ani()
    {
        if (!q2a1)
        {
            mission.SetActive(true);
            q2a1 = true;
        }
        else if (q2a1 && !mission.activeSelf)
        {
            starDoll.GetComponent<DragRigidBody2D>().enabled = true;
            //if (!starDoll.activeSelf) q2a2 = true;
        }
    }
    
    void Q3_Before_Seven_Trash()
    {
        if (!q3a1)
        {
            ti.currLineArr[0] = 139;
            ti.NPC_Say_yeah("별감");
            trashHeap.layer = 0;
            q3a1 = true;
        }
    }

    void Q4_After_Seven_Trash()
    {
        if (!q4a1)
        {
            // save point //
            //Save_Script.Save_Now_Point();
            // save point //
            trashHeap.layer = 2;
            umbrella.SetActive(true);
            umbrella.GetComponent<BoxCollider2D>().enabled = false;
            Physics2D.IgnoreCollision(umbrella.GetComponent<BoxCollider2D>(), trashHeap.GetComponent<PolygonCollider2D>());
            ti.currLineArr[0] = 141; // 화분으로 쓸만한건없네..
            ti.NPC_Say_yeah("별감");
            q4a1 = true;
        }
        else if (q4a1 && !q4a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 56;
            ti.NPC_Say_yeah("코코");
            q4a2 = true;
        }
        else if (q4a2 && !q4a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 143; // 뭐해 임마?
            ti.NPC_Say_yeah("별감");
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
            q4a3 = true;
        }
        else if (q4a3 && !q4a4 && !_star_textbox.activeSelf)
        {
            StartCoroutine(RainDrop());
            q4a5 = true;
        }
        else if (q4a4 && q4a5 && !q4a6)
        {
            ti.currLineArr[0] = 146; // 어 비온다
            ti.NPC_Say_yeah("별감");
            q4a6 = true;
        }
        else if (q4a6 && !q4a7 && !_star_textbox.activeSelf)
        {
            umbrella.GetComponent<BoxCollider2D>().enabled = true;
            print("우산에 시그널 보내");
            for (int i = 0; i < ic._item_list.Length; i++)
            {
                if (ic._item_name_list[i] == "Umbrella")
                {
                    ti.currLineArr[0] = 148; // 임마 쓰지도 못하는
                    ti.NPC_Say_yeah("별감");
                    q4a7 = true;
                    break;
                }
            }
        }
        else if (q4a7 && !q4a8 && !_star_textbox.activeSelf)
        {
            StartCoroutine(Fadeout_black());
            q4a8 = true;
        }
        else if (q4a8 && q4a9 && !q4a10)
        {
            StartCoroutine(Fadein_Image());
            q4a10 = true;
        }
        else if (q4a10 && q4a11 && !q4a12)
        {
            StartCoroutine(Fadeout_Image());
            q4a12 = true;
        }
        else if (q4a12 && q4a13 && !q4a14)
        {
            StartCoroutine(Fadein_black());
            q4a14 = true;
        }
        else if (q4a14 && q4a15 && !q4a16)
        {
            ti.currLineArr[2] = 56;
            ti.NPC_Say_yeah("코코");
            q4a16 = true;
        }
        else if (q4a16 && !q4a17 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 150; // 무시하냐..
            ti.NPC_Say_yeah("별감");
            q4a17 = true;
        }
        else if (q4a17 && !_star_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[39] = true; // 맵을 나오기 전까지 완료.
            
        }
    }
    
    IEnumerator MoveToTrash()
    {
        mbr.enabled = false;
        print("자동으로 쓰레기 더미 앞까지 가기");
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(player.transform.position.x, trashHeap.transform.position.x, ref velocity, smoothTime);
            player.transform.position = new Vector3(newPosition, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(1f);
            q1a4 = true;
            break;
        }
    }

 /*   IEnumerator Mission()
    {
        print("텍스트");
        while (true)
        {
            mission.enabled = true;
            yield return new WaitForSeconds(2f);
            mission.enabled = false;
            q2a2 = true;
            break;
        }
    }*/

    IEnumerator RainDrop()
    {
        print("비가 와요 비가 와");
        rainFall.Play();
        rainMist.Play();
        while (true)
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity += 0.01f;
            yield return new WaitForSeconds(2f);
            q4a4 = true;
            break;
        }
    }

    IEnumerator Fadein_Image()
    {
        for (float f = 0f; f < 1; f += Time.deltaTime)
        {
            Color c = rememberScene.color;
            c.a = f;
            rememberScene.color = c;
            yield return null;
        }
        rememberScene.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(4f);
        q4a11 = true;
    }

    IEnumerator Fadeout_Image()
    {
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = new Color(1, 1, 1, 1);
            c.a = f;
            rememberScene.color = c;
            print(rememberScene.color.a);
            yield return null;
        }
        rememberScene.color = new Color(1, 1, 1, 0);
        q4a13 = true;
    }

    IEnumerator Fadeout_black()
    {
        for (float f = 0f; f < 1; f += Time.deltaTime)
        {
            Color c = _blackout.color;
            c.a = f;
            _blackout.color = c;
            yield return null;
        }
        _blackout.color = new Color(0, 0, 0, 1);
        q4a9 = true;
    }

    IEnumerator Fadein_black()
    {
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color bb = new Color(0, 0, 0, 1);
            bb.a = f;
            _blackout.color = bb;
            yield return null;
        }
        _blackout.color = new Color(0, 0, 0, 0);
        q4a15 = true;
    }

    void Execute_Ani()
    {
        print("쓰레기 더미 헤집는 중");
    }
}
