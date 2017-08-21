using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_3_GameController : Controller {
    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private Text_Importer2 ti;

    public SpriteRenderer _blackout;
    public BoxCollider2D Portal;
    public Image rememberScene;

    void Awake()
    {
        sceneNo = 63;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();

        player.transform.position = start_pos.position;
        ti.Import(63);
        ti.Talk(); // 어서와 코코
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("앉고나서 씬으로 전환");
        if (other.CompareTag("Player") && !Stage6_Controller.q[4])
        {
            StartCoroutine(Fadein_Image());
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Fadeout_black()
    {
        mbr.enabled = false;
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
        mbr.enabled = false;
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
        mbr.enabled = false;
        for (float f = 1f; f > 0; f -= Time.deltaTime)
        {
            Color c = new Color(1, 1, 1, 1);
            c.a = f;
            rememberScene.color = c;
            yield return null;
        }
        rememberScene.color = new Color(1, 1, 1, 0);
        Stage6_Controller.q[4] = true;
        Portal.enabled = true;
        Portal.transform.position = player.transform.position;
    }
}
