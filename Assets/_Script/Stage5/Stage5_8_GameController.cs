using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_8_GameController : MonoBehaviour {

    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject _star_textbox;
    private GameObject _ivon_textbox;
    private GameObject _coco_textbox;
    private Text_Importer ti;
    private Item_Controller ic;

    private bool q1a1 = false;
    private bool q1a2 = false;
    private bool q1a3 = false;
    private bool q1a4 = false;

    void Awake()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();

        player.transform.position = start_pos.position;
    }

    void Start()
    {
        if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = regen_pos.position;
        }

        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
        _star_textbox = ti._text_boxes[0];
        _ivon_textbox = ti._text_boxes[1];
        _coco_textbox = ti._text_boxes[2];
    }

    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[33] && !Stage5_Controller._Stage5_Quest[34])
        {
            Q1_conversation();
        }
    }

    void Q1_conversation()
    {
        if (!q1a1)
        {
            ti.currLineArr[0] = 129; // 어 이 골목 없어지지...
            ti.NPC_Say_yeah("별감");
            q1a1 = true;
        }
        else if (q1a1 && !q1a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 52;
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        else if (q1a2 && !q1a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 131; //아냐.. 아닌가?
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        else if (q1a3 && !_star_textbox.activeSelf)
        {
            Stage5_Controller._Stage5_Quest[34] = true;
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
        }
    }
}
