using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage1_1x_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Moving_by_RLbuttons mbr;
	//public Text Player_text;
	private bool a1 = false;
	public Transform arrow;

	private Text_Importer2 ti;

	void Awake(){
        //googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage1_1x");

		player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		//Player_text = GameObject.Find ("코코_text").GetComponent<Text> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;

		sceneNo = 14;

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (14);
	}

	void Start(){
		if (PlayerPrefs.GetInt ("SceneFromWhere") == 6) {
			//2nd save point//
			Save_Script.Save_Now_Point ();
			print ("Saved");
			//print (PlayerPrefs.GetInt ("Restart_SceneNum"));
			//print (PlayerPrefs.GetInt ("Now_SceneNum"));
			//2nd save point//
		}

		if (GetComponent<Load_data> ()._where_are_you_from == 8) {
			player.transform.position = regen_pos.position;
		} else {
//			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.currLineArr [0] += 2;//코코 다음대사로 넘김
		}
	}

//	void Update(){
//		if (a1 && mbr.state != CocoState.Fear) {
//			StartCoroutine ("Backback");
//			a1 = false;
//		}
//	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player) {
			//말하고 뒤로 자동으로 움직임?
			//mbr.enabled = false;
			//StartCoroutine ("Backback");
			arrow.rotation = Quaternion.Euler(Vector3.zero);
			mbr.SetState(CocoState.Fear);
			//mbr.enabled = false;
//			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.NPC_Say_yeah ("코코");
			//a1 = true;

			ti.Talk ();
		}
	}

//	IEnumerator Backback(){
//		for (int i = 0; i < 80; i++) {
//			mbr.Moving_left (-8f);
//			yield return null;
//		}
//		mbr.enabled = true;
//	}
}
