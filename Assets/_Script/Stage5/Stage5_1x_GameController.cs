using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_1x_GameController : MonoBehaviour {

	private Text_Importer ti;
	private bool q_a = false;
	private bool q_b = false;
	private GameObject player;

	public BoxCollider2D a1;
	public BoxCollider2D a2;
	public GameObject portal;
	public Transform startpos;

	void Awake(){
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		player = GameObject.Find ("Player");
		portal = GameObject.Find ("Portal");
	}

	void Start () {
		ti.currLineArr [1] = 18;
		ti.NPC_Say_yeah ("이본");

		player.transform.position = startpos.position;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && !q_a) {
			ti.currLineArr [1] = 20;
			ti.NPC_Say_yeah ("이본");
			q_a = true;
		}
		if (q_b && other.CompareTag ("Player")) {
			print ("부딪힘");
			portal.transform.position = player.transform.position;
			Stage5_Controller._Stage5_Quest [22] = true;
		}
	}

	void Update () {
		if (q_a && !q_b && ti._text_boxes [1].activeSelf) {
			a1.enabled = false;
			a2.enabled = true;
			GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
			q_b = true;
		}
	}
}
