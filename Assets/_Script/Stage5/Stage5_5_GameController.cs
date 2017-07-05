using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_5_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private GameObject _star_textbox;
	private GameObject _ivon_textbox;
    private GameObject _coco_textbox;
	private Text_Importer ti;
	private Item_Controller ic;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;

    private bool talk = false;

    private bool q1a1 = false;
    private bool q1a2 = false;
    private bool q1a3 = false;

    void Awake(){
		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

		player.transform.position = start_pos.position;
	}

	void Start(){
        if (GetComponent<Load_data> ()._where_are_you_from == 36) {
			player.transform.position = regen_pos.position;
		}

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		_star_textbox = ti._text_boxes [0];
		_ivon_textbox = ti._text_boxes [1];
        _coco_textbox = ti._text_boxes [2];

        if (!Stage5_Controller._Stage5_Quest[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
    }
    
    void Update()
    {
        if (Stage5_Controller._Stage5_Quest[25] && !Stage5_Controller._Stage5_Quest[26] && !talk)
        {
            Q1_conversation();
        }
    }

    void Q1_conversation()
    {
        if (!q1a1)
        {
            //save point//
            Save_Script.Save_Now_Point();
            //save point//
            ti.currLineArr[0] = 85;
            ti.NPC_Say_yeah("별감");
            q1a1 = true;
        }
        if (q1a1 && !q1a2 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 32;
            ti.NPC_Say_yeah("코코");
            q1a2 = true;
        }
        if (q1a2 && !q1a3 && !_coco_textbox.activeSelf)
        {
            ti.currLineArr[0] = 87;
            ti.NPC_Say_yeah("별감");
            q1a3 = true;
        }
        if (q1a3 && !_star_textbox.activeSelf)
        {
            ti.currLineArr[2] = 34;
            ti.NPC_Say_yeah("코코");
            talk = true;
        }
    }
}
