﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_2_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    public BoxCollider2D transparent_wall;
	public SpriteRenderer broken_bridge;
	public GameObject sp;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Outline o_l;
	private Mirror_Socket_Controller msc;
	private Text_Importer2 ti;


	void Awake(){
        //googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage1_2");

        player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		msc = GameObject.Find ("Mirror_Socket").GetComponent<Mirror_Socket_Controller> ();
		o_l = GameObject.Find ("Mirror_Socket").GetComponent<Outline> ();
		o_l.used_or_not_for_retry = false;

		player.transform.position = start_pos.position;
		sceneNo = 12;
	}

	void Start(){

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (12);

		if (GetComponent<Load_data> ()._where_are_you_from == 6) {
			player.transform.position = regen_pos.position;
		}

		if (Stage1_Controller.q[2]) { //오 이거 생각보다 유용함
			//거울을 뽑으면 다른데 갔다와도 뽑혀있는상태임!
			transparent_wall.enabled = false;
			broken_bridge.size = new Vector2 (7.05f, 1.59f);
			//broken_bridge.SetActive (true);
			Destroy(GameObject.Find("Mirror_Socket"));
			sp.SetActive (false);
		}
	}

	void Update(){
		if (!Stage1_Controller.q[2]) {
			Q2_remove_partOfmirror ();
		}

	}

	void Q2_remove_partOfmirror(){
		if (msc.mirror_in_ornot) {//거울을 빼면,
			sp.SetActive(false);

			ti.Talk ();

//			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.currLineArr [0] += 2;//코코 다음대사로 넘김
//			aa.NPC_Say_yeah ("코코");

			transparent_wall.enabled = false;
			//broken_bridge.SetActive (true);
			StartCoroutine("Mirror_Effect");
			Destroy(GameObject.Find("Mirror_Socket"));
			Stage1_Controller.q[2] = true;
		}
	}

	IEnumerator Mirror_Effect(){
		float i = 0f;
		while(true) {
			i += 0.15f;
			//print (broken_bridge);
			broken_bridge.size = new Vector2 (i, 1.59f);
			yield return null;
			if (i > 7.05f) {
				broken_bridge.size = new Vector2 (7.05f, 1.59f);
				break;
			}
		}
		//broken_bridge.
	}
}

