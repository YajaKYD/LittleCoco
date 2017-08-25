using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_7_1_GameController : Controller {

    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    public SpriteRenderer _blackout;
    private Text_Importer2 ti;
    private Item_Controller ic;

    public GameObject starDoll;
    public Image rememberScene;

    private GameObject trashHeap;
    private GameObject umbrella;
    public GameObject mission;
    public SpriteRenderer wholePanel;
    public GameObject sparkle;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private bool q1a1 = false;
    private bool q1a2 = false;
    private bool q2a1 = false;
    private bool q3a1 = false;
    private bool q4a1 = false;
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

    private float velocity = 0.0f;
    private float smoothTime = 1.5f; // For auto move

    void Awake()
    {
        sceneNo = 57;
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();

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
        ti.Import(57);

        Physics2D.IgnoreCollision(starDoll.GetComponent<BoxCollider2D>(), trashHeap.GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(starDoll.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        
        if (!Stage5_Controller.q[37])
        {
            trashHeap.layer = 2;
            starDoll.GetComponent<DragRigidBody2D>().enabled = false;
        }

        if (Stage5_Controller.q[38])
        {
            starDoll.SetActive(false);
            trashHeap.layer = 2;
        }
        umbrella.SetActive(false);

        if (!Stage5_Controller.q[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
    }

    void Update()
    {
        if (Stage5_Controller.q[35] && !Stage5_Controller.q[36])
        {
            Q1_Find_Trash_Ani();
        }
        else if (Stage5_Controller.q[36] && !Stage5_Controller.q[37])
        {
            Q2_Start_Ani();
            mbr.enabled = false;
        }
        else if (Stage5_Controller.q[37] && !Stage5_Controller.q[38])
        {
            Q3_Before_Seven_Trash();
            mbr.enabled = false;
        }
        else if (Stage5_Controller.q[38] && !Stage5_Controller.q[39])
        {
            Q4_After_Seven_Trash();
            mbr.enabled = false;
        }
        else if (Stage5_Controller.q[39])
        {
            mbr.enabled = true;
        }
    }

    void Q1_Find_Trash_Ani()
    {
        if (!q1a1)
        {
            ti.Talk(); // 엇 저 인형은..
            q1a1 = true;
        }
        else if (!Stage5_Controller.q[70]) mbr.enabled = false;
        else if (Stage5_Controller.q[70] && !q1a2)
        {
            mbr.Moving_Right(8f);
            if (player.transform.position.x >= -6.5f) q1a2 = true;
            //StartCoroutine(MoveToTrash());
            //q1a5 = true;
        }
        else if (q1a2)
        {
            Stage5_Controller.q[36] = true;
        }
    }

    void Q2_Start_Ani()
    {
        if (!q2a1)
        {
            mission.SetActive(true);
            wholePanel.enabled = true;
            q2a1 = true;
        }
        else if (q2a1 && !mission.activeSelf)
        {
            wholePanel.enabled = false;
            starDoll.GetComponent<DragRigidBody2D>().enabled = true;
            //if (!starDoll.activeSelf) q2a2 = true;
        }
    }
    
    void Q3_Before_Seven_Trash()
    {
        if (!q3a1)
        {
            ti.Talk(5); //저런 예쁜 인형을 누가
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
            // rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
            ti.Talk(7); // 화분으로 쓸 만한게..
            q4a1 = true;
        }
        else if (Stage5_Controller.q[72] && !q4a4)
        {
            StartCoroutine(RainDrop());
            q4a5 = true;
        }
        else if (q4a4 && q4a5 && !q4a6)
        {
            ti.Talk(12); // 어 비온다
            q4a6 = true;
        }
        else if (Stage5_Controller.q[73] && !q4a7)
        {
            umbrella.GetComponent<BoxCollider2D>().enabled = true;
            print("우산에 시그널 보내");
            sparkle.SetActive(true);
            if (ic._item_name_list[2] == "Umbrella")
            {
                ti.Talk(14); // 임마 쓰지도 못하는
                q4a7 = true;
                sparkle.SetActive(false);
            }
        }
        else if (Stage5_Controller.q[74] && !q4a8)
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
            ti.Talk(16); // 무시하냐..
            q4a16 = true;
        }
    }
    
 /*   IEnumerator MoveToTrash()
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

    IEnumerator Mission()
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
