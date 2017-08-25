using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_2_GameController : Controller {
    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    public GameObject Ivon;

    private Text_Importer2 ti;
    private Item_Controller ic;
    private DigitalRuby.RainMaker.RainScript2D rainIntensity;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;
    public SpriteRenderer _blackout;
    public BoxCollider2D Portal;
    public Image rememberScene;

    private bool getGum;
    private bool q1a1; private bool q1a2; private bool q1a3;

    void Awake()
    {
        sceneNo = 62;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        //regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        rainIntensity = rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>();
      //  Portal.enabled = false;
        player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(62);
        Save_Script.Save_Now_Point();
        ti.Talk();
    }

    void Update()
    {
        if (Stage6_Controller.q[0] && !Stage6_Controller.q[1])
        {
            mbr.enabled = false;
            if (rainIntensity.RainIntensity < 1f)
            {
                rainIntensity.RainIntensity += 0.005f;
            }
            else Stage6_Controller.q[1] = true;
        }
        else if (Stage6_Controller.q[1] && !Stage6_Controller.q[2])
        {
            if (!q1a1)
            {
                ti.Talk(11);
                q1a1 = true;
            }
        }
        else if (Stage6_Controller.q[2] && !Stage6_Controller.q[3])
        {// 나무 밑으로 뛰어가기
            Ivon.SetActive(false);
            /*mbr.Moving_Right(8f);
            _Ivon_Position.position = new Vector2(_Ivon_Position.position.x + 0.1f, _Ivon_Position.position.y);*/
        }
    }
}
