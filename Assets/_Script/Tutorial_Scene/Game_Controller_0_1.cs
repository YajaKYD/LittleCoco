using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Game_Controller_0_1 : Controller {

    public GoogleAnalyticsV4 googleAnalytics;

	public Transform _Ivon_Position;
	public Transform startpos;

	//public Text_Importer _t_i;
	public Text_Importer2 ti;
	private Item_Controller i_c;
	private GameObject player;
	private GameObject Ivon;
	private GameObject portal;
	private bool throwing_ball = false;
	private bool first_quest_start = false;
	private bool first_quest_end = false;
	private bool second_quest_end = false;

    //호균
    public Tutorial_Controller tc;
    public GameObject tutorialMessage;
    public GameObject[] tutorialMessagePrefab;
    private GameObject itemCanvas;
    public GameObject _ivon_textbox;
    private bool ivon_quest_end;
    public int tutorialMessageIndex;
    private bool getGum;
	private bool remo = false;



    void Awake(){
        googleAnalytics.StartSession();
        googleAnalytics.LogScreen(SceneManager.GetActiveScene().name);

        player = GameObject.Find ("Player");
		Ivon = GameObject.Find ("이본");
		portal = GameObject.Find ("Portal");
		//_t_i = GameObject.Find ("Dialogue_Canvas_").GetComponent<Text_Importer> ();
		ti = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer2> ();
		i_c = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		bool musictog = PlayerPrefsX.GetBool ("Music_ONOFF");
		float musicsl = PlayerPrefs.GetFloat ("Music_Volume");
		PlayerPrefs.DeleteAll ();
		PlayerPrefsX.SetBool ("Music_ONOFF", musictog);
		PlayerPrefs.SetFloat ("Music_Volume", musicsl);
        tc = GameObject.FindWithTag("Controller").GetComponent<Tutorial_Controller>();
		sceneNo = 1;
		ti.Import(1);
		player.transform.position = startpos.position;
    }

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W)) {
			print ("goto1-6");
			SceneManager.LoadScene (4);
		}
		//


		if (portal.GetComponent<Portal_Controller> ().enter_ && !throwing_ball) {//throwing ball조건을 해제하면 엄청난 일이 발생한다.
			//_t_i.NPC_Say_yeah ("이본");
			ti.Talk();
			first_quest_start = true;
			portal.SetActive (false);
			throwing_ball = true;
		}

		if (first_quest_start && Tutorial_Controller.q[0]) {//공을 던진다
			
			GameObject prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
			prefab.transform.position = _Ivon_Position.position + Vector3.up * 2;
            first_quest_start = false;
            //
            tc.instantiateMessage(0); // 이동 방법 알려주는 메세지
         }

		if (i_c._item_name_list [0] == "공" && Vector2.Distance(player.transform.position, Ivon.transform.position) < 5f && !first_quest_end) {//공 줍고
			//_t_i.currLineArr[0] += 2;
			//_t_i.NPC_Say_yeah ("이본");
			ti.Talk(ti.lineNo+2);
			i_c._item_name_list [0] = "";
			i_c._usable_item [0] = false;
			i_c._the_number_of_items[0] = 0;
			i_c._interaction_object [0] = "";
			i_c._consumable [0] = false;
			i_c._explanations [0] = "";

			first_quest_end = true;
        }

		if(Tutorial_Controller.q[1] && !getGum && first_quest_end && Input.GetMouseButtonDown(0))
        {
            GameObject gum = (GameObject)Instantiate(Resources.Load("Prefabs/dogfood"));
            gum.name = "개껌";
            gum.transform.position = player.transform.position + Vector3.up * 1;
            getGum = true;
            tc.instantiateMessage(1); // 개껌을 획득했다!
            StartCoroutine("changeMessage");
        }

        if (i_c._now_used_item == "개껌" && !second_quest_end) {
			//_t_i.currLineArr[0] += 2;
			//_t_i.NPC_Say_yeah ("이본");
			ti.Talk(ti.lineNo+2);
			second_quest_end = true;
		}

		if(second_quest_end && Tutorial_Controller.q[2] && !ivon_quest_end)
        {
            tc.instantiateMessage(4); // 왼쪽으로 이동하세요
            //Debug.Log("이본 대사 끝")
            ivon_quest_end = true;
			portal.SetActive (true);
            GameObject.FindWithTag("NPC").SetActive(false);
        }

		if (portal.GetComponent<Portal_Controller> ().exit_ && !remo) {
			Destroy(tc.tutorialMessage);
			remo = true;
		}
	}

    IEnumerator changeMessage()
    {
        yield return new WaitForSeconds(3);
        Destroy(tc.tutorialMessage);
        tc.tutorialMessageIndex++;
        tc.instantiateMessage(2);
    }
//
//    void OnDisable()
//    {
//        Destroy(tc.tutorialMessage);
//    }
}
