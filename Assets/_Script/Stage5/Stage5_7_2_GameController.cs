using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_7_2_GameController : Controller {

    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    public SpriteRenderer _blackout;
    private Text_Importer2 ti;
    private Item_Controller ic;

    public Image rememberScene;
    public Transform portalTo5_4;
    public BoxCollider2D portalTo5_3;

    private GameObject trashHeap;
    private GameObject hardBox;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private float velocity = 0.0f;
    private float smoothTime = 1.5f; // For auto move

    private bool q1a1;
    private bool q2a1;
    private bool q2a4;
    private bool q2a5;
    private bool q2a6;
    private bool q2a7;
    private bool q2a8;
    private bool q2a9;
    private bool q2a10;
    private bool q2a11;
    private bool q2a12;
    private bool q3a1;

    void Awake()
    {
        sceneNo = 59;
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();

        trashHeap = GameObject.Find("TrashHeap");
        hardBox = GameObject.Find("HardBox");
    }

    void Start()
    {
        if (GetComponent<Load_data>()._where_are_you_from == 35)
        {
            player.transform.position = start_pos.position;
        }
        else if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = regen_pos.position;
        }
        if (!Stage5_Controller.q[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
        if (Stage5_Controller.q[42]) trashHeap.SetActive(false);

        ti.Import(59);

        trashHeap.layer = 2;
        hardBox.SetActive(false);
    }

    void Update()
    {
        if (Stage5_Controller.q[40] && !Stage5_Controller.q[41])
        {
            Q1_Go_Before_Trash();
        }
        else if (Stage5_Controller.q[41] && !Stage5_Controller.q[42])
        {
            Q2_After_Seven_Trash();
        }
        else if (Stage5_Controller.q[42] && !Stage5_Controller.q[43])
        {
            Q3_Auto_Move();
        }
    }

    void Q1_Go_Before_Trash()
    {
        if (!q1a1)
        {
           //   mbr.Moving_left(-8f);
            if (player.transform.position.x <= 6f) q1a1 = true;
        }
        else
        {
            trashHeap.layer = 0;
            StartCoroutine(Dig_Trash());
        }
    }

    void Q2_After_Seven_Trash()
    {
        if (!q2a1)
        {
            hardBox.SetActive(true);
            Physics2D.IgnoreCollision(hardBox.GetComponent<BoxCollider2D>(), trashHeap.GetComponent<PolygonCollider2D>());
            print("박스에 시그널 보내");
            if (ic._item_name_list[3] == "HardBox")
            {
                portalTo5_3.enabled = false;
                ti.Talk();//아 상자는 왜 또
                q2a1 = true;
            }
        }
        else if (!Stage5_Controller.q[75]) mbr.enabled = false;
        else if (Stage5_Controller.q[75] && !q2a4)
        {
            StartCoroutine(Fadeout_black());
            q2a4 = true;
        }
        else if (q2a4 && q2a5 && !q2a6)
        {
            StartCoroutine(Fadein_Image());
            q2a6 = true;
        }
        else if (q2a6 && q2a7 && !q2a8)
        {
            StartCoroutine(Fadeout_Image());
            q2a8 = true;
        }
        else if (q2a8 && q2a9 && !q2a10)
        {
            StartCoroutine(Fadein_black());
            q2a10 = true;
        }
        else if (q2a10 && q2a11 && !q2a12)
        {
            Stage5_Controller.q[42] = true; // 회상 씬에서 돌아오는 거까지 완료.
        }
    }

    void Q3_Auto_Move()
    {
        if (!q3a1)
        {
            ti.Talk(5); //코야 코코야
            q3a1 = true;
        }
        else if (Stage5_Controller.q[76])
        {
            //StartCoroutine(AutoMove());
            mbr.Moving_left(-8f);
            if (player.transform.position.x <= -17.29f)  Stage5_Controller.q[43] = true;
        }
    }

    IEnumerator Dig_Trash()
    {
        mbr.enabled = false;
        print("애니 계속.. 쓰레기 주워야 풀림");
        while (!Stage5_Controller.q[41]) // 쓰레기 다 던질 때까지 계속 반복.
        {
            yield return null;
        }
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
        q2a5 = true;
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
        q2a7 = true;
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
        trashHeap.SetActive(false); // 쓰레기더미 치워주고
        q2a9 = true;
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
        q2a11 = true;
    }
/*
    IEnumerator AutoMove()
    {
        mbr.enabled = false;
        print("자동으로 5-4로 가기");
        while (true)
        {
            float newPosition = Mathf.SmoothDamp(player.transform.position.x, portalTo5_4.position.x, ref velocity, smoothTime);
            player.transform.position = new Vector3(newPosition, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(2.7f);
            
            Stage5_Controller.q[43] = true;
            break;
        }
    }*/
}
