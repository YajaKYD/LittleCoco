using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_7_GameController : MonoBehaviour {
    private Transform start_pos;
    private Transform regen_pos;
    private GameObject player;
    private Moving_by_RLbuttons mbr;
    private GameObject _star_textbox;
    private GameObject _coco_textbox;
    private GameObject _ivon_textbox;
    public SpriteRenderer _blackout;
    private Text_Importer ti;
    private Item_Controller ic;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    void Awake()
    {
        player = GameObject.Find("Player");
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        start_pos = GameObject.Find("Start_Pos").transform;
        regen_pos = GameObject.Find("Regen_Pos").transform;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer>();
    }

    void Start()
    {
        if (GetComponent<Load_data>()._where_are_you_from == 36)
        {
            player.transform.position = regen_pos.position;
        }
        else if (GetComponent<Load_data>()._where_are_you_from == 35)
        {
            player.transform.position = start_pos.position;
        }
    }
}
