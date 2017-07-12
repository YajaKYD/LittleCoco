using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage5_6_GameController : MonoBehaviour {
    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject _star_textbox;
    private GameObject _coco_textbox;
    private GameObject _ivon_textbox;
    public SpriteRenderer _blackout;
    public Camera main_Camera;
    private Text_Importer ti;
    private Item_Controller ic;

    private GameObject Ivon;
    private GameObject IvonTextPos;
    private GameObject babyDog;
    private GameObject umbrella;
    private GameObject hardbox;
    public Image rememberScene;
    public GameObject portal_to_5_4;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private Vector3 velocity;
    private float smoothTime = 0.3f; // For Camera move

    private bool q1a1;
    private bool q1a2;
    private bool q1a3;
    private bool q1a4;
    private bool q1a5;
    private bool q2a1;
    private bool q2a2;
    private bool q2a3;
    private bool q2a4;
    private bool q2a5;
    private bool q2a6;
    private bool q2a7;
    private bool q2a8;
    private bool q3a1;
    private bool q3a2;
    private bool q3a3;
    private bool q3a4;
    private bool q3a5;
    private bool q4a1;
    private bool q4a2;
    private bool q4a3;
    private bool q4a4;
    private bool q4a5;
    private bool q4a6;
    private bool q4a7;
    private bool q4a8;

    void Awake()
    {
        player = GameObject.Find("Player");
        Ivon = GameObject.Find("Ivon");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();

        babyDog = GameObject.Find("BabyDog");
        umbrella = GameObject.Find("Umbrella");
        hardbox = GameObject.Find("HardBox");
        IvonTextPos = GameObject.Find("IvonTextPos");
    }

    void Start()
    {
        Ivon.SetActive(false);
        if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = start_pos.position;
        }

        _star_textbox = ti._text_boxes[0];
        _ivon_textbox = ti._text_boxes[1];
        _coco_textbox = ti._text_boxes[2];

        if (!Stage5_Controller._Stage5_Quest[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }

        if (Stage5_Controller._Stage5_Quest[43])
        {
            babyDog.SetActive(true);
        }
        else babyDog.SetActive(false);

        if (!Stage5_Controller._Stage5_Quest[45])
        {
            umbrella.SetActive(false);
            hardbox.SetActive(false);
        }
    }

    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[43] && !Stage5_Controller._Stage5_Quest[44])
        {
            Q1_Move_Baby();
        }
        else if (Stage5_Controller._Stage5_Quest[45] && !Stage5_Controller._Stage5_Quest[46])
        {
            Q2_After_Umbrella();
        }
        else if (Stage5_Controller._Stage5_Quest[46] && !Stage5_Controller._Stage5_Quest[47])
        {
            Q3_Ivon_Appear();
        }
        else if (Stage5_Controller._Stage5_Quest[47] && !Stage5_Controller._Stage5_Quest[48])
        {
            Q4_Remember();
        }
    }

    void Q1_Move_Baby()
    {
        if (!q1a1)
        {
            ti.currLineArr[0] = 162; // 코코야 저기 봐 강아지가.
            ti.NPC_Say_yeah("별감");
            q1a1 = true;
        }
        else if (q1a1 && !q1a2 && !_star_textbox.activeSelf)
        {
            mbr.Moving_Right(8f);
            if (babyDog.transform.position.x <= player.transform.position.x)
            {
                babyDog.transform.position = new Vector2(player.transform.position.x, this.transform.position.y);
            }
            if (player.transform.position.x >= 13f) q1a2 = true;
        }
        else if (q1a2 && !q1a3)
        {
            ti.currLineArr[0] = 164; // 어떡해 감기 걸리겟어..
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        else if (q1a3 && !q1a4 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 64;
            ti.NPC_Say_yeah("코코");
            q1a4 = true;
        }
        else if (q1a4 && !q1a5 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 166; // 우산과 상자..!
            ti.NPC_Say_yeah("별감");
            q1a5 = true;
        }
        else if (q1a5 && !_star_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[44] = true; // 상자와 우산 설치하기 전 대화까지 완료.
        }
    }

    void Q2_After_Umbrella()
    {
        if (player.transform.position.x >= 11f) 
        {
            mbr.Moving_left(-8f);
            babyDog.transform.position = player.transform.position;
        }
        else
        {
            if (!q2a1)
            {
                babyDog.transform.position = hardbox.transform.position;
                q2a1 = true;
            }
            else if (q2a1 && !q2a2)
            {
                StartCoroutine(Fadeout_black());
                q2a2 = true;
            }
            else if (q2a3 && !q2a4)
            {
                StartCoroutine(Fadein_Image());
                q2a4 = true;
            }
            else if (q2a5 && !q2a6)
            {
                StartCoroutine(Fadeout_Image());
                q2a6 = true;
            }
            else if (q2a7 && !q2a8)
            {
                StartCoroutine(Fadein_black());
                q2a8 = true;
            }
        }
    }

    void Q3_Ivon_Appear()
    {
        if (!q3a1)
        {
            Ivon.SetActive(true);
            StartCoroutine(Ivon_Camera());
        }
        else if (q3a1 && !q3a2)
        {
            ti.currLineArr[1] = 22; // 어? 우산이네..
            _ivon_textbox.transform.parent.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            _ivon_textbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ti.NPC_Say_yeah("이본");
            q3a2 = true;
        }
        else if (q3a2 && !q3a3 && !_ivon_textbox.activeSelf)
        {
            //main_Camera.GetComponent<CameraManager>().FocusObject = Ivon;
            main_Camera.transform.position = new Vector3(main_Camera.transform.position.x + 0.1f, main_Camera.transform.position.y, -10f);
            IvonTextPos.transform.position = new Vector2(IvonTextPos.transform.position.x + 0.1f, IvonTextPos.transform.position.y);
            Ivon.transform.position = new Vector2(Ivon.transform.position.x + 0.1f, Ivon.transform.position.y);
            if (Ivon.transform.position.x >= 2.5f) q3a3 = true;
        }
        else if (q3a3 && !q3a4)
        {
            print("우산을 집어들려고 하며...");
            ti.currLineArr[1] = 24; //...어?! 강아지??
            _ivon_textbox.transform.parent.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            _ivon_textbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ti.NPC_Say_yeah("이본");
            q3a4 = true;
        }
        else if (q3a4 && !_ivon_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[47] = true; // 코코와 주인의 첫 만남 씬 이전 대화까지 완료.
        }
    }

    void Q4_Remember()
    {
        if (!q4a1)
        {
            StartCoroutine(Fadeout_black());
            q4a1 = true;
        }
        else if (q4a2 && !q4a3)
        {
            StartCoroutine(Fadein_Image());
            q4a3 = true;
        }
        else if (q4a4 && !q4a5)
        {
            main_Camera.GetComponent<CameraManager>().enabled = true;
            StartCoroutine(Fadeout_Image());
            q4a5 = true;
        }
        else if (q4a6 && !q4a7)
        {
            StartCoroutine(Fadein_black());
            q4a7 = true;
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
        if (!Stage5_Controller._Stage5_Quest[47]) q2a3 = true;
        else q4a2 = true;
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
        if (!Stage5_Controller._Stage5_Quest[47]) q2a5 = true;
        else q4a4 = true;
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
        if (!Stage5_Controller._Stage5_Quest[47]) q2a7 = true;
        else q4a6 = true;
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
        if (!Stage5_Controller._Stage5_Quest[47]) Stage5_Controller._Stage5_Quest[46] = true;
        else
        {
            Stage5_Controller._Stage5_Quest[48] = true; // 자동으로 다음 씬 넘어가기 전까지
            portal_to_5_4.transform.position = player.transform.position;
        }
    }

    IEnumerator Ivon_Camera()
    {
        mbr.enabled = false;
        while (true)
        {
            main_Camera.GetComponent<CameraManager>().enabled = false;
            Vector3 newPosition = Vector3.SmoothDamp(main_Camera.transform.position, new Vector3(-7.0f, -1.001358e-05f, -10f), ref velocity, smoothTime, 10f, Time.deltaTime);
            main_Camera.transform.position = newPosition;//new Vector3(newPosition, main_Camera.transform.position.y, main_Camera.transform.position.z);
            print("카메라가 이본에게로");
            yield return new WaitForSeconds(3f);
            q3a1 = true;
            break;
        }
    }
}
