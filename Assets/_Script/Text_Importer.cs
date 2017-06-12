using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class Text_Importer : MonoBehaviour {

	public TextAsset _textfile;
	public string[] _textLines;
	public int[] currLineArr;
	public int pNum;
	public int pInd;
	public GameObject[] _text_boxes;
	public Text [] _text_in_boxes;
	public BoxCollider2D[] _npc_collider;
	public GameObject[] _coco_dialogue;

	private List<string>[] dialogArr;
	private string[] nameArr;
	private Moving_by_RLbuttons player_moving;

	void Awake () {
		if ((TextAsset)Resources.Load (SceneManager.GetActiveScene ().buildIndex.ToString ()) != null) {
			//텍스트파일을 Assets/Resources 안에 처넣어야만 한다. 그래야 빌드할 때 같이함
			_textfile = (TextAsset)Resources.Load (SceneManager.GetActiveScene ().buildIndex.ToString ());
			_textLines = (_textfile.text.Split ('\n'));

			player_moving = GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ();

			int line = 0;
			pNum = int.Parse (_textLines [line++]);
			dialogArr = new List<string>[pNum];
			currLineArr = new int[pNum];
			nameArr = new string[pNum];
			_text_in_boxes = new Text[pNum];
			_text_boxes = new GameObject[pNum];
			_npc_collider = new BoxCollider2D[pNum];
			pInd = 0;

			for (int i = 0; i < pNum; i++) {
		
				dialogArr [i] = new List<string> ();
				currLineArr [i] = 0;
				nameArr [i] = _textLines [line++];
				_text_in_boxes [i] = GameObject.Find (nameArr [i] + "_text").GetComponent<Text> ();

				_text_boxes [i] = GameObject.Find (nameArr [i] + "_text");
				_npc_collider [i] = GameObject.Find (nameArr [i]).GetComponent<BoxCollider2D> ();
				//
				_text_boxes[i].transform.parent.gameObject.SetActive(false);
				//
				_text_boxes [i].SetActive (false);
				int leng = int.Parse (_textLines [line++]);

				for (int j = 0; j < leng; j++) {
					dialogArr [i].Add (_textLines [line++]);
				}
			}
		}
	}

	public void Import () { //Awake시점과 다른 상황에서 텍스트파일을 불러야 할 때
		//텍스트파일을 Assets/Resources 안에 처넣어야만 한다. 그래야 빌드할 때 같이함
		_textfile = (TextAsset)Resources.Load (SceneManager.GetActiveScene ().buildIndex.ToString ());
		_textLines = (_textfile.text.Split ('\n'));

		player_moving = GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ();

		for (int x = 0; x < gameObject.transform.childCount - 1; x++) {
			gameObject.transform.GetChild (x).gameObject.SetActive (true);
		}

		int line = 0;
		pNum = int.Parse (_textLines [line++]);
		dialogArr = new List<string>[pNum];
		currLineArr = new int[pNum];
		nameArr = new string[pNum];
		_text_in_boxes = new Text[pNum];
		_text_boxes = new GameObject[pNum];
		_npc_collider = new BoxCollider2D[pNum];
		pInd = 0;

		for (int i = 0; i < pNum; i++) {

			dialogArr [i] = new List<string> ();
			currLineArr [i] = 0;
			nameArr [i] = _textLines [line++];
			_text_in_boxes [i] = GameObject.Find (nameArr [i] + "_text").GetComponent<Text> ();


			_text_boxes [i] = GameObject.Find (nameArr [i] + "_text");
			if (GameObject.Find (nameArr [i])) {
				_npc_collider [i] = GameObject.Find (nameArr [i]).GetComponent<BoxCollider2D> ();
			}
			//
			_text_boxes[i].transform.parent.gameObject.SetActive(false);
			//
			_text_boxes [i].SetActive (false);
			int leng = int.Parse (_textLines [line++]);

			for (int j = 0; j < leng; j++) {
				dialogArr [i].Add (_textLines [line++]);
			}
		}

		for (int x = 0; x < gameObject.transform.childCount - 1; x++) {
			gameObject.transform.GetChild (x).gameObject.SetActive (false);
		}
	}

	public void Import (int aaa) { //Awake시점과 다른 상황에서 텍스트파일을 불러야 할 때
		//텍스트파일을 Assets/Resources 안에 처넣어야만 한다. 그래야 빌드할 때 같이함
		_textfile = (TextAsset)Resources.Load (aaa.ToString());
		_textLines = (_textfile.text.Split ('\n'));

		player_moving = GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ();

		for (int x = 0; x < gameObject.transform.childCount - 1; x++) {
			gameObject.transform.GetChild (x).gameObject.SetActive (true);
		}

		int line = 0;
		pNum = int.Parse (_textLines [line++]);
		dialogArr = new List<string>[pNum];
		currLineArr = new int[pNum];
		nameArr = new string[pNum];
		_text_in_boxes = new Text[pNum];
		_text_boxes = new GameObject[pNum];
		_npc_collider = new BoxCollider2D[pNum];
		pInd = 0;

		for (int i = 0; i < pNum; i++) {

			dialogArr [i] = new List<string> ();
			currLineArr [i] = 0;
			nameArr [i] = _textLines [line++];
			//Debug.Log (nameArr [i]);
			_text_in_boxes [i] = GameObject.Find (nameArr [i] + "_text").GetComponent<Text> ();


			_text_boxes [i] = GameObject.Find (nameArr [i] + "_text");
			if (GameObject.Find (nameArr [i])) {
				_npc_collider [i] = GameObject.Find (nameArr [i]).GetComponent<BoxCollider2D> ();
			}
			//
			_text_boxes[i].transform.parent.gameObject.SetActive(false);
			//
			_text_boxes [i].SetActive (false);
			int leng = int.Parse (_textLines [line++]);

			for (int j = 0; j < leng; j++) {
				dialogArr [i].Add (_textLines [line++]);
			}
		}

		for (int x = 0; x < gameObject.transform.childCount - 1; x++) {
			gameObject.transform.GetChild (x).gameObject.SetActive (false);
		}
	}

	public bool NPC_Say_yeah(string npcname){
		for (int i = 0; i < pNum; i++) {//받은 NPC이름하고 가지고 있는 NPC리스트 비교
			if (nameArr [i] == npcname) {//같은 이름 NPC 찾고
				if (npcname == "코코") {
					//
					_text_boxes[i].transform.parent.gameObject.SetActive(true);
					//
					_text_boxes [i].SetActive (true);
					player_moving.enabled = false;

					for (int x = 0; x < _coco_dialogue.Length; x++) {
						_coco_dialogue [x].SetActive (false);
					} //직전에 한 대사를 모두 끈다.

					if (dialogArr [i] [currLineArr [i]] != "<><>") { //현재 대사를 한다.
						for (int j = 0; j < _coco_dialogue.Length; j++) {
							if(dialogArr [i] [currLineArr [i]] == _coco_dialogue[j].name){
								//_coco_dialogue [j].transform.rotation = player_moving.gameObject.transform.rotation;
								_coco_dialogue[j].SetActive(true);
								currLineArr [i]++;
								break;
							}
						}
						//_text_in_boxes [i].text = dialogArr [i] [currLineArr [i]++];
						return true;
					} else {//대화가 끝난 상태면 직전 마지막 말을 반복함
						player_moving.enabled = true;
						currLineArr[i]--;
						return false;
					}
				}

				if (npcname == "별감") {
					TurnOnOffItemList s = GameObject.FindWithTag ("Item_Canvas").GetComponentInChildren<TurnOnOffItemList> ();
					if (s.OnOffButton.localScale.x == 1) {
						s.TurnOnOffitemList ();
					}
				}

				if(_npc_collider[i]){
					_npc_collider[i].enabled = false; //NPC Collider 끄기
				}
				//
				_text_boxes[i].transform.parent.transform.gameObject.SetActive(true);
				//
				_text_boxes [i].SetActive (true);
				player_moving.enabled = false;
				if (dialogArr [i] [currLineArr [i]] != "<><>") {
					_text_in_boxes [i].text = dialogArr [i] [currLineArr [i]++];
					return true;
				} else {//대화가 끝난 상태면 직전 마지막 말을 반복함
					player_moving.enabled = true;
					currLineArr[i]--;
					return false;
				}
			} else {
				//return false;
			}
		}
		return false;
	}
}