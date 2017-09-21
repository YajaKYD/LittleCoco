using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_4_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    public BoxCollider2D transparent_wall;

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	private Outline o_l;
	private Mirror_Socket_Controller msc;
	private bool tempbool = false;

	public SpriteRenderer fence;
	//public static bool stage1_4_mirror_or_not = false;

	void Awake(){
        googleAnalytics.StartSession();
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[1]);
        googleAnalytics.LogScreen("Stage1_4");

        player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		msc = GameObject.Find ("Mirror_Socket").GetComponent<Mirror_Socket_Controller> ();
		o_l = GameObject.Find ("Mirror_Socket").GetComponent<Outline> ();
		o_l.used_or_not_for_retry = false; //소켓 끔

		player.transform.position = start_pos.position;

		tempbool = msc.mirror_in_ornot;

		sceneNo = 15;
	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 9) {
			player.transform.position = regen_pos.position;
		}
		if (Stage1_Controller.q[5]) {//없는상태는 기본이므로 굳이 저장&적용 노노!
			//거울을 넣은 상태로 다른씬이동 후 왔을 때
			o_l.used_or_not_for_retry = true;
		}
	}

	void LateUpdate(){
		if (tempbool != msc.mirror_in_ornot) {
			if (msc.mirror_in_ornot) {//거울이 있을 때
				transparent_wall.enabled = false;
				StartCoroutine (Mirror_Effect_Off (fence));
				//fence.SetActive (false);
				Stage1_Controller.q [5] = true;
				//Destroy(GameObject.Find("Mirror_Socket"));
			} else {
				transparent_wall.enabled = true;
				StartCoroutine (Mirror_Effect_On (fence));
				//fence.SetActive (true);
				Stage1_Controller.q [5] = false;
			}
			tempbool = msc.mirror_in_ornot;
		}
	}

	IEnumerator Mirror_Effect_On(SpriteRenderer a){
		//if (a.size.x < 1f) {
		float i = 0f;
		while (true) {
			i += 0.08f;
			a.size = new Vector2 (i, 3.74f);
			if (i > 1.23) {
				a.size = new Vector2 (1.23f, 3.74f);
				break;
			}
			yield return null;
		}
		//}
	}

	IEnumerator Mirror_Effect_Off(SpriteRenderer a){
		//if (a.size.x > 1f) {
		float i = 1.23f;
		while (true) {
			i -= 0.08f;
			a.size = new Vector2 (i, 3.74f);
			if (i < 0) {
				a.size = new Vector2 (0f, 3.74f);
				break;
			}
			yield return null;
		}
		//}
	}
}
