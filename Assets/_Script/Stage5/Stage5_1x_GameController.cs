using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_1x_GameController : Controller {
    public GoogleAnalyticsV4 googleAnalytics;
    private GameObject[] Analyticslist;

    private Text_Importer2 ti;
	private bool q_b = false;
	private GameObject player;

	public BoxCollider2D a1;
	public BoxCollider2D a2;
	public GameObject portal;
	public Transform startpos;
    private Moving_by_RLbuttons mbr;

    void Awake(){
        Analyticslist = GameObject.FindGameObjectsWithTag("Analysis");
        if (Analyticslist.Length > 1) Destroy(Analyticslist[0]);
        googleAnalytics.LogScreen("Stage5_1#");

        sceneNo = 50;
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
        ti.Import(50);
		player = GameObject.Find ("Player");
		portal = GameObject.Find ("Portal");
	}

	void Start () {
        mbr = player.GetComponent<Moving_by_RLbuttons>();
        Save_Script.Save_Now_Point();
        ti.Talk(); // 코코~
        mbr.enabled = false;

		player.transform.position = startpos.position;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && !Stage5_Controller.q[57]) {
            ti.Talk(3); // 자! 물어와!
		}
		if (q_b && other.CompareTag ("Player")) {
			print ("부딪힘");
			portal.transform.position = player.transform.position;
			Stage5_Controller.q [22] = true;
		}
	}

	void Update () {
        if (Stage5_Controller.q[57] && !q_b)
        {
            a1.enabled = false;
            a2.enabled = true;
            GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
            q_b = true;
        }
        else if (!Stage5_Controller.q[56]) mbr.enabled = false;
	}
}
