using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_5_GameController : Controller {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	private Text_Importer2 ti;
	private Item_Controller ic;

    public ParticleSystem rainFall;
    public ParticleSystem rainMist;
    
    private bool q1a1 = false;

    void Awake(){
        sceneNo = 55;
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

		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(55);

        if (!Stage5_Controller.q[39])
        {
            rainFall.transform.parent.gameObject.GetComponent<DigitalRuby.RainMaker.RainScript2D>().RainIntensity = 0f;
        }
    }
    
    void Update()
    {
        if (Stage5_Controller.q[25] && !Stage5_Controller.q[62])
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
            ti.Talk();
            q1a1 = true;
        }
        mbr.enabled = false;
    }
}
