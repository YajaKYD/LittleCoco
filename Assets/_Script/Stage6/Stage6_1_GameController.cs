using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage6_1_GameController : Controller {

    private GameObject player;
    private GameObject start_pos;
    private Text_Importer2 ti;
    public SpriteRenderer _blackout;

    public GameObject diary;
    private BookPro dragging;

    public GameObject tapeButton;
    public GameObject umbrellaButton;
    public GameObject ballButton;
    public GameObject glueButton;
    public GameObject sparkle;

    public GameObject on_off;
    public Image itemButton0; public Image item0;
    public Image itemButton1; public Image item1;
    public Image itemButton2; public Image itemButton3; public Image itemButton4;
    public GameObject joystick; public GameObject jumpButton;

    private bool q2a1, q2a2, q2a3;
    private bool GGALJAKI1 = false; private bool GGALJAKI2 = false; private bool GGALJAKI3 = false; private bool GGALJAKI4 = false;

    void Awake()
    {
        sceneNo = 61;
        Stage6_Controller.diaryscene = true;
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        start_pos = GameObject.Find("Start_Pos");
        dragging = diary.GetComponent<BookPro>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(61);

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
        on_off.SetActive(false); joystick.SetActive(false); jumpButton.SetActive(false);
        itemButton0.enabled = false; item0.enabled = false; itemButton1.enabled = false; item1.enabled = false;
        itemButton2.enabled = false; itemButton3.enabled = false; itemButton4.enabled = false;

        player.transform.position = start_pos.transform.position;

        if (!Stage6_Controller.q[15])
        {
            ti.Talk();
        }
        else if (!Stage6_Controller.q[25])
        {
            ti.Talk(4); // 음 여긴 없는걸
        }
        else if (!Stage6_Controller.q[35])
        {
            dragging.CurrentPaper = 5;
        }
        else if (!Stage6_Controller.q[42])
        {
            dragging.CurrentPaper = 3;
            ti.Talk(10); // 여기서 갑자기 왜 일기장을..?
        }
    }

    void Update () {
        if (!Stage6_Controller.q[26] && umbrellaButton.activeSelf && !q2a1)
        {
            if (!q2a1)
            {
                ti.Talk(6); // 우산이 빛난다!
                q2a1 = true;
            }
        }
        else if (!Stage6_Controller.q[35] && ballButton.activeSelf && !q2a2)
        {
            if (!q2a2)
            {
                ti.Talk(8); // 효과가 있었으면..
                q2a2 = true;
            }
        }
        else if (!Stage6_Controller.q[43] && glueButton.activeSelf && !q2a3)
        {
            if (!q2a3)
            {
                ti.Talk(13); // 오 여기 왜 빛이..
                q2a3 = true;
            }
        }

        if (dragging.currentPaper == 2 && !Stage6_Controller.q[16])
        {
            if (!GGALJAKI1)
            {
                tapeButton.SetActive(true);
                sparkle.transform.position = new Vector3(16.62f, -7.1f, 8.5f);
                StartCoroutine(LightOn());//sparkle.SetActive(true);
                GGALJAKI1 = true;
            }
        }
        else if (dragging.currentPaper == 5 && Stage6_Controller.q[25] && !Stage6_Controller.q[27] && !dragging.pageDragging)
        {
            if (!GGALJAKI2)
            {
                //print(dragging.currentPaper);
                StartCoroutine(UmbrellaOn());
                //umbrellaButton.SetActive(true);
                sparkle.transform.position = new Vector3(16.5f, -6.1f, 8.5f);
                StartCoroutine(LightOn());
                GGALJAKI2 = true;//sparkle.SetActive(true);
            }
        }
        else if (dragging.currentPaper == 3 && Stage6_Controller.q[34] && !Stage6_Controller.q[36] && !dragging.pageDragging)
        {
            if (!GGALJAKI3)
            {
                StartCoroutine(BallOn());
                sparkle.transform.position = new Vector3(13.54f, 0.4f, 8.5f);
                StartCoroutine(LightOn());
                GGALJAKI3 = true;
            }
        }
        else if (dragging.currentPaper == 4 && Stage6_Controller.q[42] && !Stage6_Controller.q[44] && !dragging.pageDragging)
        {
            if (!GGALJAKI4)
            {
                StartCoroutine(GlueOn());
                sparkle.transform.position = new Vector3(16.25f, -11.6f, 8.5f);
                StartCoroutine(LightOn());
                GGALJAKI4 = true;
            }
        }
        else
        {
            sparkle.SetActive(false);
            tapeButton.SetActive(false);
            umbrellaButton.SetActive(false);
            ballButton.SetActive(false);
            glueButton.SetActive(false);
            GGALJAKI1 = false; GGALJAKI2 = false; GGALJAKI3 = false; GGALJAKI4 = false;
        }
    }

    public void goto2_2()
    {
        SceneManager.LoadScene(47);
    }
    public void goto5_6()
    {
        SceneManager.LoadScene(48);
    }
    public void goto3_6()
    {
        SceneManager.LoadScene(49);
    }
    public void goto4_2()
    {
        SceneManager.LoadScene(50);
    }

    IEnumerator UmbrellaOn()
    {
        yield return new WaitForSeconds(0.3f);
        if (GGALJAKI2) umbrellaButton.SetActive(true);
    }

    IEnumerator BallOn()
    {
        yield return new WaitForSeconds(0.3f);
        if (GGALJAKI3) ballButton.SetActive(true);
    }

    IEnumerator GlueOn()
    {
        yield return new WaitForSeconds(0.3f);
        if (GGALJAKI4) glueButton.SetActive(true);
    }

    IEnumerator LightOn()
    {
        yield return new WaitForSeconds(0.2f);
        sparkle.SetActive(true);
    }
}
