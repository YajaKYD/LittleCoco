using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2_1_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Item_Controller ic;
	private Stage2_Controller s2c;

	private bool a1a1 = true;
	private bool a2a2 = true;
	private bool a3a3 = true;

	public GameObject clockwork;
	public GameObject[] multiTap;
	public GameObject _coco_textbox;

	private Text_Importer2 ti;

	private bool drop_tape = false;

	//Test용//
//	public GameObject a1;
//	public GameObject b1;

	void Awake(){
        googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[1]);
        googleAnalytics.LogScreen("Stage2_1");

        player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		s2c = GameObject.Find ("Stage2_Controller").GetComponent<Stage2_Controller> ();

		sceneNo = 21;
	}

	void Start(){

		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
		ti.Import (21);

		if (!Stage2_Controller.q[0]) {
			//3rd save point//
			Save_Script.Save_Now_Point();
			print ("Saved");
			//3rd save point//
			s2c.ic = ic; //?이거뭐냐
			player.transform.localScale = new Vector3 (1.4f, 1.4f, player.transform.localScale.z);
		}

		if (GetComponent<Load_data> ()._where_are_you_from == 12) {
			player.transform.position = regen_pos.position;
		} else {
//			Text_Importer aa = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text_Importer> ();
//			aa.NPC_Say_yeah ("코코");
			ti.Talk ();

		}

		if (Stage2_Controller.q[0]) {
//			Destroy (a1);
//			Destroy (b1);	//이거 안꺼놓으면 nullreference라서 태엽하고 멀티탭 살아남
			Destroy (clockwork);
		}

		if (Stage2_Controller.q[4]) {
			Destroy (multiTap [0]);
			Destroy (multiTap [1]);
		}
	}

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W)) {
			print ("goto2end");
			SceneManager.LoadScene (14);
		}
		//
		if(!Stage2_Controller.q[0]){
			Q1_pick_clockwork_up ();
		}
			
		if (Stage2_Controller.q[3] && !Stage2_Controller.q[4]) {
			Q5_multiTap ();
		}

	}


	void Q1_pick_clockwork_up(){
		if (a1a1) {
			ti.Talk (1);
			a1a1 = false;
		}

		if (!a1a1 && Stage2_Controller.q[28]) {
			clockwork.GetComponent<Collider2D> ().enabled = true;

			ti.Talk(3);
			Stage2_Controller.q[0] = true;
		} //XoX 대사 넘기면 태엽을 줍는다.

	}

	void Q5_multiTap(){
		if (a3a3) {
			multiTap [0].GetComponent<BoxCollider2D> ().enabled = true;
			multiTap [1].GetComponent<BoxCollider2D> ().enabled = true;
			a3a3 = false;
		}

		if (!multiTap [0]) {
			ti.Talk (5);

			Stage2_Controller.q[4] = true;
		}
	}

}
