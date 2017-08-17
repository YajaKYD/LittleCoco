using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_5_GameController : Controller {
    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject Ivon;

    private Text_Importer2 ti;
    private Item_Controller ic;
    
    public SpriteRenderer _blackout;
    public Image _ivon_textbox;
    public Text _ivon_text;
    public BoxCollider2D sceneTrigger;
    public BoxCollider2D talkTrigger;
    public Image rememberScene;
    public BoxCollider2D portalTo6_4;

    void Awake()
    {
        sceneNo = 65;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        Ivon = GameObject.Find("Ivon");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

        _ivon_textbox.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
        _ivon_text.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        sceneTrigger.enabled = false;
        player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(65);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") /*&& Stage6_Controller.q[8]*/ && !Stage6_Controller.q[9])
        {
            ti.Talk();
            talkTrigger.enabled = false;
            sceneTrigger.enabled = true;
        }
        else if (other.CompareTag("Player") && Stage6_Controller.q[10] && !Stage6_Controller.q[11])
        {
            StartCoroutine(Fadeout_black());
        }
    }

    void Update()
    {
        if (Stage6_Controller.q[9] && !Stage6_Controller.q[10])
        {
            print("문 닫히는 소리");
            Ivon.SetActive(false);
            Stage6_Controller.q[10] = true;
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
        portalTo6_4.transform.position = player.transform.position;
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
    }
}
