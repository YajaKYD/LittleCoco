using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_1_GameController : Controller {

	private bool a1;
	private bool a2;
	private GameObject _ivon_textbox;
	private GameObject player;
	private Transform start_pos;
	private Transform regen_pos;
    private Text_Importer2 ti;

    public GameObject bag;

	void Awake(){
        sceneNo = 31;
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;



		if (Stage3_Controller.q[7])
        {
            Destroy(bag);
        }

	}

	void Start(){
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(31);
        //Stage3 Save point 1//
        if (!Stage3_Controller.q [0]) {
			Save_Script.Save_Now_Point ();
		}
		//Stage3 Save point 1//

		if (GetComponent<Load_data> ()._where_are_you_from == 16) {
			//Debug.Log ("from 16");
			player.transform.position = regen_pos.position;
		} else {
			player.transform.position = start_pos.position;
		}
		if (Stage3_Controller.q[2])
        {
            bag.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W)) {
			print ("go to end");
			for (int i = 0; i < Stage3_Controller.q.Length; i++) {
				Stage3_Controller.q [i] = true;
			}
			SceneManager.LoadScene (18);
			//SceneManager.LoadScene (21);
		}
		//


		if (!Stage3_Controller.q[0]) {
			Q1_StartTalk ();
		}


    }

	void Q1_StartTalk(){
		if (!a1) {
            ti.Talk();
			//_ivon_textbox = GameObject.Find ("이본_text");
			a1 = true;
		}
	}
}
