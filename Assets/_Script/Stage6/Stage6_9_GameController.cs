using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage6_9_GameController : Controller {
    private Transform start_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;

    private Text_Importer2 ti;
    private Item_Controller ic;

    public SpriteRenderer _blackout;
    public BoxCollider2D portalto3_6;

    public GameObject on_off;
    public Image itemButton0; public Image item0;
    public Image itemButton1; public Image item1;
    public Image itemButton2; public Image itemButton3; public Image itemButton4;
    public GameObject joystick; public GameObject jumpButton;

    void Awake()
    {
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
        on_off.SetActive(true); joystick.SetActive(true); jumpButton.SetActive(true);
        itemButton0.enabled = true; item0.enabled = true; itemButton1.enabled = true; item1.enabled = true;
        itemButton2.enabled = true; itemButton3.enabled = true; itemButton4.enabled = true;

        sceneNo = 69;
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

        player.transform.position = start_pos.position;
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(69);
        ti.Talk();
    }
	
	void Update () {
        if (ic._item_name_list[2] == "Pot" && !Stage6_Controller.q[45])
        {
            portalto3_6.transform.position = player.transform.position;
            Stage6_Controller.q[45] = true;
        }
    }
}
