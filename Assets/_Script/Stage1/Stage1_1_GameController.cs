using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Stage1_1_GameController : MonoBehaviour {

	private Transform start_pos;
	private Transform regen_pos;
	private GameObject player;
	public GameObject mirror;

	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_Pos").transform;
		regen_pos = GameObject.Find ("Regen_Pos").transform;
		player.transform.position = start_pos.position;

		if (!Stage1_Controller._Stage1_Quest[0]) {
			Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
			aa.Import ();
		}
	}

	void Start(){
		if (GetComponent<Load_data> ()._where_are_you_from == 5) {
			player.transform.position = regen_pos.position;
		}
		if (Stage1_Controller._Stage1_Quest[1]) {
			Destroy (mirror);
		}
	}

	void Update(){

		//개발용//
		if (Input.GetKey (KeyCode.Q) && Input.GetKey (KeyCode.W)) {
			print ("goto1-6");
			SceneManager.LoadScene (10);
		}
		//


		if (!Stage1_Controller._Stage1_Quest[0]) {
			Q1_say_what_happen ();
		}


		if (!mirror) {
			Stage1_Controller._Stage1_Quest[1] = true;
			Save_Script.Save_Quest_Info ();
		}
	}

	void Q1_say_what_happen(){
		Text_Importer aa = GameObject.FindWithTag ("Dialogue").GetComponent<Text_Importer> ();
		aa.NPC_Say_yeah ("코코");
		Stage1_Controller._Stage1_Quest[0] = true;
		Save_Script.Save_Quest_Info ();
	}



	/*

	public Text _System1;
	public Text _System2;
	public Text _System3;
	public Text _Dialogue_text;
	public Text_Importer _importer;
	public GameObject _Dialogue_Panel;
//	public Dialogue_Controller d_c;

	private Color ccc;
	private bool system_end = false;
	//private Main_Player_Controller m_p_c;

	void Awake () {
		
		ccc = new Color (1f, 1f, 1f, 1f);
		//m_p_c = GameObject.Find ("Player").GetComponent<Main_Player_Controller> ();
		//_Dialogue_Panel.GetComponent<Dialogue_Controller> ().current_line = this.GetComponent<Load_data> ()._current_LINE;
		if (this.GetComponent<Load_data> ()._current_LINE == 0) { //처음 씬 실행될 경우
			_System1.gameObject.SetActive(true);
		}
	}

	void Update () {
		if (_System1.IsActive ()) {
			StartCoroutine (System_message (_System1));
		}

		//if (system_end && _Dialogue_Panel.GetComponent<Dialogue_Controller> ().current_line == 0) {
		//	_Dialogue_text.text = _importer._textLines [1];
		//	_Dialogue_Panel.SetActive (true);
			//m_p_c.enabled = false;
		//	system_end = false;
		//}


	}
		
	IEnumerator System_message(Text aa){
		
		if (ccc.a > 0) {
			yield return new WaitForSeconds (3);
			ccc.a -= Time.deltaTime;
			aa.color = ccc;
		} else {
			aa.gameObject.SetActive (false);
			system_end = true;
			yield break;
		}
	}

*/
}
