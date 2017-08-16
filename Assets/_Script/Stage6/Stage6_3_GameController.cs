using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_3_GameController : Controller {
    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    public Transform _Ivon_Position;

    private Text_Importer2 ti;
    private Item_Controller ic;
    private DigitalRuby.RainMaker.RainScript2D rainIntensity;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;
    public SpriteRenderer _blackout;
    public BoxCollider2D Portal;

    private bool getGum;
    private bool q1a1; private bool q1a2; private bool q1a3;

    void Awake()
    {
        sceneNo = 63;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
      //  start_pos = GameObject.Find("Start_Pos").transform;
        //regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        rainIntensity = rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>();
        Portal.enabled = false;
        //player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(63);
        ti.Talk();
    }

    void Update()
    {
        if (Stage6_Controller.q[0] && !Stage6_Controller.q[1])
        {
            GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Ball(6)"));
            prefab.transform.position = _Ivon_Position.position + Vector3.up * 2;
            Stage6_Controller.q[1] = true;
        }
        else if (ic._item_name_list[2] == "공" && Vector2.Distance(player.transform.position, _Ivon_Position.position) < 5f && !q1a1)
        {
            ic._item_name_list[2] = "";
            ic._usable_item[2] = false;
            ic._the_number_of_items[2] = 0;
            q1a1 = true;
            ti.Talk(ti.lineNo + 2); // 개껌이옵니다.
        }
        else if (Stage6_Controller.q[2] && !getGum)
        {
            GameObject gum = (GameObject)Instantiate(Resources.Load("Prefabs/dogfood"));
            gum.name = "개껌";
            gum.transform.position = player.transform.position + Vector3.up * 1;
            getGum = true;
        }
        else if (ic._now_used_item =="개껌" && !Stage6_Controller.q[3])
        {
            mbr.enabled = false;
            if (rainIntensity.RainIntensity < 1f)
            {
                rainIntensity.RainIntensity += 0.005f;
            }
            else if (!q1a2)
            {
                q1a2 = true;
                ti.Talk(ti.lineNo + 2);
                Portal.enabled = (true);
            }
        }
        else if (Stage6_Controller.q[3] && !Stage6_Controller.q[4])
        {
            Stage6_Controller.q[4] = true;
            Portal.transform.position = player.transform.position;
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
    }
}
