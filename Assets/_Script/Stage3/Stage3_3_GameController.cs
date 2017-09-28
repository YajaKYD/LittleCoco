using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_3_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private bool a1 = false;
	private bool a2 = false;
	private GameObject player;
    private GameObject npc;
	public GameObject _ivon_textbox;
	private Transform start_pos;
    private Item_Controller ic;
    private Text_Importer2 aa;

	public Transform _ivon;
    public GameObject portalto3_4;
    
	//private Transform regen_pos;
	void Awake(){
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage3_3");

        sceneNo = 33;
		player = GameObject.Find ("Player");
        npc = GameObject.FindWithTag("NPC");
		start_pos = GameObject.Find ("Start_Pos").transform;
		player.transform.position = start_pos.position;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        aa = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        //regen_pos = GameObject.Find ("Regen_Pos").transform;
    }

	void Start(){
        aa.Import(33);
		if (Stage3_Controller.q [8]) {
			portalto3_4.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

    void OnTriggerEnter2D()
    {
		if (Stage3_Controller.q[0] && !Stage3_Controller.q[1])
        {
            Q2_start_getaBall();
        }

		if (Stage3_Controller.q[1] && !Stage3_Controller.q[2])
        {
			if ((Stage3_Controller.q[3] && Stage3_Controller.q[4]) ||
				(Stage3_Controller.q[3] && Stage3_Controller.q[5]) ||
				(Stage3_Controller.q[4] && Stage3_Controller.q[5]))
            {
                Q4_start_getaBag();
            }
        }

		if (Stage3_Controller.q[2] && !Stage3_Controller.q[6])
        {
			if (Stage3_Controller.q[7] && !Stage3_Controller.q[8])
            {
                Q5_start_goOut();
            }
        }
    }

	void Q2_start_getaBall(){
        aa.Talk(); // 공원으로 놀러가자 코코
    }

	void Q4_start_getaBag(){
        Use_Item("ball1");
        Use_Item("ball2");
        Use_Item("ball3");//공주워온거지움
        aa.Talk(5); // 우리 코코 똑똑하지!
       // _ivon_textbox = GameObject.Find("이본_text");
		//Stage3_Controller.q[2] = true;
	}

    void Q5_start_goOut()
    {
        Use_Item("Dog_bag");
        aa.Talk(9); // 와 우리코코 잘했어
		//Stage3_Controller.q[8] = true;
        portalto3_4.GetComponent<BoxCollider2D>().enabled = true;
    }

    void Use_Item(string name)
    {
        for (int i = 0; i < 5; i++)
        {
            if (ic._item_name_list[i] == name)
            {
                ic._item_name_list[i] = "";
                ic._usable_item[i] = false;
                ic._the_number_of_items[i] = 0;
                ic._interaction_object[i] = "";
                ic._consumable[i] = false;
                ic._item_list[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                ic._item_list[i].transform.parent.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0);
				ic._explanations [i] = "";
				ic.cant_pick_during_using = true;
            }
        }
    }
}
