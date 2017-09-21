using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class Text_Importer2 : MonoBehaviour {
	public TextAsset textFile;
	public GameObject[] textBoxes;
	public Text [] textInBoxes;

	public int lineNo;
	public int stageNo;

	public BoxCollider2D[] _npc_collider;
	public GameObject[] cocoDialogue;

	private string[] names = {"Coco", "Star", "Ivon", "Racoon", "null"};
	public GameObject player;
	public Moving_by_RLbuttons player_moving;

	//private char lineSeperator = '\r'; // for windows OS, use '\n'
	private char lineSeperator = '\n';
	private char fieldSeperator = ',';

	public string[] speaker;
	public string[] textLine;

	void Awake () {
		lineNo = 1;
		//player = GameObject.FindWithTag ("Player");

		textBoxes = new GameObject[names.Length];
		textInBoxes = new Text[names.Length];
		DontDestroyOnLoad (this.gameObject);
	}

	void Start(){
		//player_moving = player.GetComponent<Moving_by_RLbuttons> ();
	}

	public void Import (int sceneNo) { //Awake시점과 다른 상황에서 텍스트파일을 불러야 할 때
		textFile = (TextAsset)Resources.Load (sceneNo.ToString());
		GetLineNo (sceneNo);  

		//Load lines from CSV file
		string[] records = textFile.text.Split (lineSeperator);
		speaker = new string[records.Length];
		textLine = new string[records.Length];
		Debug.Log (records.Length);

		for (int i = 1; i < records.Length; i++) {
			string[] fields = records[i].Split (fieldSeperator);
			speaker [i] = fields [0];
			textLine [i] = fields [1];
			//Debug.Log (speaker [i] + " : " + textLine [i]);
		}

		//
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (true);
		}
		//
		cocoDialogue = new GameObject[GameObject.Find("Coco_Dialogue").transform.childCount];

		for (int i = 0; i < cocoDialogue.Length; i++) {
			cocoDialogue [i] = GameObject.Find ("Coco_Dialogue").transform.GetChild (i).gameObject;
		}
			
		Debug.Log (names.Length);

		for (int j = 0; j < names.Length - 1; j++) {
			Debug.Log (j + ", " + names [j]);
			textBoxes [j] = GameObject.Find (names [j] + "_text");
			textInBoxes [j] = GameObject.Find (names [j] + "_text").GetComponentInChildren<Text> ();
			textBoxes [j].SetActive (false);
		}
			
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (false);
		}
	}
		
	void GetLineNo(int sceneNo){
		stageNo = sceneNo / 10;
		sceneNo = sceneNo % 10;
		Debug.Log ("stage " + stageNo + ", scene " + sceneNo);

		switch (stageNo) {
		case 0:
			break;
		case 1:
			if (Stage1_Controller.lineNo [sceneNo] == 0) {
				lineNo = 1;
			} else {
				lineNo = Stage1_Controller.lineNo [sceneNo];
				Debug.Log ("load done");
			}
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			if (Stage4_Controller.lineNo [sceneNo] == 0) {
				lineNo = 1;
			} else {
				lineNo = Stage4_Controller.lineNo [sceneNo];
				Debug.Log ("load done");
			}
			break;
		case 5:
            if (Stage5_Controller.lineNo[sceneNo] == 0)
            {
                lineNo = 1;
            }
            else
            {
                lineNo = Stage5_Controller.lineNo[sceneNo];
                Debug.Log("load done");
            }
            break;
		case 6:
            if (Stage6_Controller.lineNo[sceneNo] == 0)
            {
                lineNo = 1;
            }
            else
            {
                lineNo = Stage6_Controller.lineNo[sceneNo];
                Debug.Log("load done");
            }
            break;
        default:
		    break;
		}
	}

	public bool Talk(){
		for (int i = 0; i < names.Length; i++) {
			//Debug.Log ("lineno : " + lineNo + ", names[" + i + "]" + names[i]);
			//Debug.Log (speaker [lineNo]);
			if (names [i] == speaker[lineNo]) {//같은 이름 NPC 찾고

				for (int x = 0; x < cocoDialogue.Length; x++) {
					cocoDialogue [x].SetActive (false);
				} //직전에 한 대사를 모두 끈다.

				if (speaker [lineNo] == "Coco") {
					for (int j = 0; j < cocoDialogue.Length; j++) {
						if ((int)textLine[lineNo][0] == (int)cocoDialogue[j].name[0]) {
							cocoDialogue [j].SetActive (true);
							if (player.transform.localScale.x > 0) {
								cocoDialogue [j].transform.localScale = new Vector3 (-1, 1, 1);
								cocoDialogue [j].GetComponentInChildren<Transform> ().localScale = new Vector3 (-1, 1, 1);
							} else if (player.transform.localScale.x < 0) {
								cocoDialogue [j].transform.localScale = new Vector3 (1, 1, 1);
								cocoDialogue [j].GetComponentInChildren<Transform> ().localScale = new Vector3 (1, 1, 1);
							}
							break;
						}
					}
				}

				if (speaker [lineNo] == "Star") {
					try{
						TurnOnOffItemList s = GameObject.FindWithTag ("Item_Canvas").GetComponentInChildren<TurnOnOffItemList> ();
						if (s.OnOffButton.localScale.x == 1) {
							s.TurnOnOffitemList ();
						}
					} catch {
					}
				}

				//other character say
				if (speaker[lineNo] == "null" ) { 
					Debug.Log ("case 1");
					player_moving.enabled = true; 

					switch (stageNo) {
					case 0:
						Tutorial_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 1:
						Stage1_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 2:
						//Stage2_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 3:
						//Stage3_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 4:
						Stage4_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 5:
						Stage5_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 6:
						Stage6_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					default:
						break;
					}

					lineNo--;
					return false;
				} else if (lineNo < speaker.Length - 1 && speaker [lineNo] == speaker [lineNo + 1]) {
					Debug.Log("case 2");
					textBoxes [i].SetActive (true);
					player_moving.enabled = false;
					textInBoxes [i].text = textLine [lineNo++];
					return true;
				} else {
					Debug.Log("case 3");
					textBoxes [i].SetActive (true);
					player_moving.enabled = false;
					textInBoxes [i].text = textLine [lineNo++];
					return true;
				}
			} else if (names [i] != speaker[lineNo]){
				textBoxes [i].SetActive (false);
			}  
		}
		return false;
	}

	public bool Talk(int _lineNo){

		lineNo = _lineNo;

		for (int i = 0; i < names.Length; i++) {
			if (names [i] == speaker[lineNo]) {//같은 이름 NPC 찾고

				for (int x = 0; x < cocoDialogue.Length; x++) {
					cocoDialogue [x].SetActive (false);
				} //직전에 한 대사를 모두 끈다.

				if (speaker [lineNo] == "Coco") {
					for (int j = 0; j < cocoDialogue.Length; j++) {
						if ((int)textLine[lineNo][0] == (int)cocoDialogue[j].name[0]) {
							cocoDialogue [j].SetActive (true);
							if (player.transform.localScale.x > 0) {
								cocoDialogue [j].transform.localScale = new Vector3 (-1, 1, 1);
								cocoDialogue [j].GetComponentInChildren<Transform> ().localScale = new Vector3 (-1, 1, 1);
							} else if (player.transform.localScale.x < 0) {
								cocoDialogue [j].transform.localScale = new Vector3 (1, 1, 1);
								cocoDialogue [j].GetComponentInChildren<Transform> ().localScale = new Vector3 (1, 1, 1);
							}
							break;
						}
					}
				}

				if (speaker [lineNo] == "Star") {
					try{
						TurnOnOffItemList s = GameObject.FindWithTag ("Item_Canvas").GetComponentInChildren<TurnOnOffItemList> ();
						if (s.OnOffButton.localScale.x == 1) {
							s.TurnOnOffitemList ();
						}
					} catch {
					}
				}

				//other character say
				if (speaker[lineNo] == "null" ) { 
					//Debug.Log ("case 1");
					player_moving.enabled = true; 

					switch (stageNo) {
					case 0:
						break;
					case 1:
						//Stage1_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 2:
						//Stage2_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 3:
						//Stage3_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 4:
						Stage4_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 5:
						Stage5_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					case 6:
						Stage6_Controller.q [int.Parse (textLine [lineNo])] = true;
						break;
					default:
						break;
					}

					lineNo--;
					return false;
				} else if (lineNo < speaker.Length - 1 && speaker [lineNo] == speaker [lineNo + 1]) {
					//Debug.Log("case 2");
					textBoxes [i].SetActive (true);
					player_moving.enabled = false;
					textInBoxes [i].text = textLine [lineNo++];
					return true;
				} else {
					//Debug.Log("case 3");
					textBoxes [i].SetActive (true);
					player_moving.enabled = false;
					textInBoxes [i].text = textLine [lineNo++];
					return true;
				}
			} else if (names [i] != speaker[lineNo]){
				textBoxes [i].SetActive (false);
			}  
		}
		return false;
	}
}
