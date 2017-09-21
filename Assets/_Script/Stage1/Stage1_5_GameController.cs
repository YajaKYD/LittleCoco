using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_5_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    public BoxCollider2D transparent_walls;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	public Outline[] o_l;
	public Mirror_Socket_Controller[] msc;
	public GameObject[] xx;
	public GameObject twinkle;
	public BoxCollider2D twall;

	public Ghost_remover gr1;
	public Ghost_remover gr2;

	private Moving_by_RLbuttons mbr;
	//public static bool stage1_5_mirror_or_not_1 = false;
	//public static bool stage1_5_mirror_or_not_2 = false;
	//public static bool stage1_5_mirror_or_not_3 = false;

	private Text_Importer2 ti;

	void Awake(){
        googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[1]);
        googleAnalytics.LogScreen("Stage1_5");

        player = GameObject.Find ("Player");
		mbr = player.GetComponent<Moving_by_RLbuttons> ();
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		transparent_walls = GetComponent<BoxCollider2D> ();
//		msc = GameObject.Find ("Mirror_Socket").GetComponent<Mirror_Socket_Controller> ();
//		o_l = GameObject.Find ("Mirror_Socket").GetComponent<Outline> ();
//		o_l.used_or_not_for_retry = true;

		for(int i = 0 ; i < 1 ; i ++){
			o_l [i].used_or_not_for_retry = false;
		}

		player.transform.position = start_pos.position;

		sceneNo = 16;

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (15);
	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 10) {
			player.transform.position = regen_pos.position;
		}

		if (Stage1_Controller.q[6]) {
			xx[1].SetActive(false);
			transparent_walls.enabled = false;
			msc [0].mirror_in_ornot = false;
			Destroy (msc [0].gameObject);
			twall.enabled = false;
		}
//		if (Stage1_Controller._Stage1_Quest[7]) {
//			xx[1].SetActive(false);
//			transparent_walls[1].enabled = false;
//			msc [1].mirror_in_ornot = false;
//			Destroy (msc [1].gameObject);
//		}
//		if (Stage1_Controller._Stage1_Quest[8]){
//			xx[2].SetActive(false);
//			transparent_walls[2].enabled = false;
//			msc [2].mirror_in_ornot = false;
//			Destroy (msc [2].gameObject);
//		}

	}


	void Update(){
		//static으로 고정필요

		if (msc[0].mirror_in_ornot) {//거울이 있을 때
			//xx[0].SetActive(false);
			StartCoroutine(Mirror_Effect(xx[0].GetComponent<SpriteRenderer>()));
			transparent_walls.enabled = false;
			msc [0].mirror_in_ornot = false;
			Destroy (msc [0].gameObject);
			player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
			Stage1_Controller.q[6] = true;
		}
//		if (msc[1].mirror_in_ornot) {//거울이 있을 때
//			//xx[1].SetActive(false);
//			Mirror_Effect_Off(xx[1].GetComponent<SpriteRenderer>());
//			transparent_walls[1].enabled = false;
//			msc [1].mirror_in_ornot = false;
//			Destroy (msc [1].gameObject);
//			Stage1_Controller._Stage1_Quest[7] = true;
//		}
//		if (msc[2].mirror_in_ornot) {//거울이 있을 때
//			//xx[2].SetActive(false);
//			Mirror_Effect_Off(xx[2].GetComponent<SpriteRenderer>());
//			transparent_walls[2].enabled = false;
//			msc [2].mirror_in_ornot = false;
//			Destroy (msc [2].gameObject);
//			Stage1_Controller._Stage1_Quest[8] = true;
//		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == player) {
			//말하고 뒤로 자동으로 움직임?
			mbr.enabled = false;
			mbr.SetState(CocoState.Fear);
//			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.NPC_Say_yeah ("코코");
			ti.Talk();
			// StartCoroutine ("Backback");
		}
	}

	void GhostRemove(){
		gr1.remover = true;
		gr2.remover = true;
		//print ("GhostRemove_Ani");
		//xx [0].SetActive (false);
		//xx [1].SetActive (false);
		//player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		twinkle.SetActive (false);
		twall.enabled = false;
	}

	IEnumerator Mirror_Effect(SpriteRenderer a){
		float i = 0f;
		while(true) {
			i += 0.08f;
			//print (broken_bridge);
			a.size = new Vector2 (i, 2.78f);

			if (i > 1.92f) {
				a.size = new Vector2 (1.92f, 2.78f);
				Invoke ("GhostRemove", 1f);
				break;
			}
			yield return null;
		}
		//broken_bridge.
	}

//	IEnumerator Backback(){
//		for (int i = 0; i < 30; i++) {
//			mbr.Moving_Right (8f);
//			yield return null;
//		}
//		Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
//		aa.NPC_Say_yeah ("코코");
//		mbr.enabled = true;     
//	}
}
