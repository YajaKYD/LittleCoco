using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6_4_GameController : MonoBehaviour {
    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject Ivon;

    private Text_Importer2 ti;
    private Item_Controller ic;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;
    private DigitalRuby.RainMaker.RainScript2D rainIntensity;
    public SpriteRenderer _blackout;
    public BoxCollider2D portal6_5;

    private bool q4a1;

    void Start()
    {
        player = GameObject.Find("Player");
        Ivon = GameObject.Find("Ivon");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        rainIntensity = rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>();

        player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
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
        ti.Talk(ti.lineNo + 2);
    }
}
