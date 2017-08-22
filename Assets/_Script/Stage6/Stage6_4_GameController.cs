using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_4_GameController : Controller {
    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject Ivon;

    private Text_Importer2 ti;
    private Item_Controller ic;
    public Image _ivon_textbox;
    public Text _ivon_text;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;
    private DigitalRuby.RainMaker.RainScript2D rainIntensity;
    public SpriteRenderer _blackout;
    public BoxCollider2D portal6_5;
    public BoxCollider2D portaltodiary;
    public GameObject mission;
    public SpriteRenderer wholePanel;
    public Image rememberScene;

    private bool q4a1, q4a2, q4a3, q4a4, q4a5, q4a6, movieend;

    void Awake()
    {
        sceneNo = 64;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        Ivon = GameObject.Find("Ivon");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        rainIntensity = rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>();

        _ivon_textbox = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetComponent<Image>();
        _ivon_text = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(2).GetChild(0).GetComponent<Text>();

        player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        //sparkle.transform.position = GameObject.FindWithTag("Item_Canvas").transform.GetChild(1).GetChild(0).position;
        ti.Import (64);
        //ti.Talk();

        if (!Stage6_Controller.q[10])
        {
            ti.Talk();
        }
        else if (Stage6_Controller.q[10] && !Stage6_Controller.q[11]) {
            rainIntensity.RainIntensity = 0f;
            Ivon.SetActive(false);
            ti.Talk(10);
        }
        else if (Stage6_Controller.q[16] && !Stage6_Controller.q[17])
        {
            rainIntensity.RainIntensity = 0f;
            Ivon.SetActive(false);
            ti.Talk(29);
        }
        else if (Stage6_Controller.q[21] && !Stage6_Controller.q[22])
        {
            ti.Talk(39);
        }
        else if (Stage6_Controller.q[28] && !Stage6_Controller.q[29])
        {
            _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); // 도대체 왜 돌려지지...
            ti.Talk(48); // 코코야 왜 짖닝
            portal6_5.enabled = false;
            ic._interaction_object[2] = "NPC";
           // player.GetComponent<Outline>().used_or_not_for_retry = false;
        }
    }

    void Update()
    {
        if (Stage6_Controller.q[5] && !Stage6_Controller.q[6])
        {
            Stage6_Controller.q[6] = true;
            StartCoroutine(Fadeout_black());
        }
        else if (Stage6_Controller.q[7] && !Stage6_Controller.q[8])
        {
            Stage6_Controller.q[8] = true;
            Ivon.SetActive(false);
        }
        else if (Stage6_Controller.q[11] && !Stage6_Controller.q[12])
        {
            if (!q4a1) ti.Talk(26);
            portal6_5.enabled = false;
            q4a1 = true;
        }
        else if (Stage6_Controller.q[12] && !Stage6_Controller.q[13])
        {
            Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
            for (int k = 0; k < ids.Length; k++)
            {
                ids[k]._diary_usable = true;
            } //change diary image -usable-
            System_Message();
            //save point//
            Save_Script.Save_Now_Point();
        }
        else if (Stage6_Controller.q[13] && !Stage6_Controller.q[14])
        {
            ic._usable_item[0] = true;
            ic._interaction_object[0] = "Player";
            player.GetComponent<Outline>().used_or_not_for_retry = false;
            if (ic._now_used_item == "Diary")
            {
                portaltodiary.enabled = true;
                portaltodiary.transform.position = player.transform.position;
                Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
                for (int x = 0; x < ids.Length; x++)
                {
                    ids[x]._diary_usable = false;
                } //change diary image -unusable-
                Stage6_Controller.q[14] = true;
            }
        }
        else if (Stage6_Controller.q[17] && !Stage6_Controller.q[18])
        {
            portal6_5.enabled = true;
        }
        else if (Stage6_Controller.q[22] && !Stage6_Controller.q[23])
        {
            Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
            player.GetComponent<Outline>().used_or_not_for_retry = false;
            for (int k = 0; k < ids.Length; k++)
            {
                ids[k]._diary_usable = true;
            } //change diary image -usable-
            if (!q4a3)
            {
                ti.Talk(46); // 일기장이 다시 빛난다...
                portal6_5.enabled = false;
                q4a3 = true;
            }
        }
        else if (Stage6_Controller.q[23] && !Stage6_Controller.q[24])
        {
            if (ic._now_used_item == "Diary")
            {
                portaltodiary.enabled = true;
                portaltodiary.transform.position = player.transform.position;
                Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
                for (int x = 0; x < ids.Length; x++)
                {
                    ids[x]._diary_usable = false;
                } //change diary image -unusable-
                Stage6_Controller.q[24] = true;
            }
        }
        else if (Stage6_Controller.q[29] && ic._now_used_item == "Umbrella" && !Stage6_Controller.q[30])
        {
            if (!q4a4) ti.Talk(51);
            q4a4 = true;
        }
        else if (Stage6_Controller.q[30] && !Stage6_Controller.q[31])
        {
            StartCoroutine(Fadeout_black_remember());
            Stage6_Controller.q[31] = true;
        }
        else if (movieend && !Stage6_Controller.q[32])
        {
            if (!q4a5)
            {
                ti.Talk(60); // 설마 또...
                q4a5 = true;
            }
        }
        else if (Stage6_Controller.q[32] && !Stage6_Controller.q[33])
        {
            Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
            player.GetComponent<Outline>().used_or_not_for_retry = false;
            for (int k = 0; k < ids.Length; k++)
            {
                ids[k]._diary_usable = true;
            } //change diary image -usable-
            if (!q4a6)
            {
                ti.Talk(69); // 또 빛이 나네..
                q4a6 = true;
            }
        }
        else if (Stage6_Controller.q[33] && !Stage6_Controller.q[34])
        {
            if (ic._now_used_item == "Diary")
            {
                portaltodiary.enabled = true;
                portaltodiary.transform.position = player.transform.position;
                Item_Drag[] ids = ic.GetComponentsInChildren<Item_Drag>();
                for (int x = 0; x < ids.Length; x++)
                {
                    ids[x]._diary_usable = false;
                } //change diary image -unusable-
                Stage6_Controller.q[34] = true;
            }
        }
    }

    void System_Message()
    {
        if (!q4a2)
        {
            mission.SetActive(true);
            wholePanel.enabled = true;
            q4a2 = true;
        }
        else if (q4a2 && !mission.activeSelf)
        {
            wholePanel.enabled = false;
            Stage6_Controller.q[13] = true;
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
        mbr.enabled = false;
        rainIntensity.RainIntensity = 0f; // 비 그치게
        _blackout.color = new Color(0, 0, 0, 1);
        StartCoroutine(Fadein_black());
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
        mbr.enabled = true;
        ti.Talk(ti.lineNo + 2);
    }

    IEnumerator Fadeout_black_remember()
    {
        for (float f = 0f; f < 1; f += Time.deltaTime)
        {
            Color c = _blackout.color;
            c.a = f;
            _blackout.color = c;
            yield return null;
        }
        _blackout.color = new Color(0, 0, 0, 1);
        StartCoroutine(Fadein_Image());
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
        StartCoroutine(Fadeout_Image());
    }

    IEnumerator Fadeout_Image()
    {
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = new Color(1, 1, 1, 1);
            c.a = f;
            rememberScene.color = c;
            yield return null;
        }
        rememberScene.color = new Color(1, 1, 1, 0);
        StartCoroutine(Fadein_black2());
    }

    IEnumerator Fadein_black2()
    {
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color bb = new Color(0, 0, 0, 1);
            bb.a = f;
            _blackout.color = bb;
            yield return null;
        }
        _blackout.color = new Color(0, 0, 0, 0);
        mbr.enabled = true;
        movieend = true;
    }
}
