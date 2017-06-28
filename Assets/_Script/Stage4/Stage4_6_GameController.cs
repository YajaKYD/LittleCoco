using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4_6_GameController : MonoBehaviour {

	public Transform fraudPanel;
	private bool q22_0;
	public int[] number = new int[2];
	public int scorePlayer, scoreNeogulman;
	public GameObject neogulman, neogulmanPrefab;
	public bool clickable;
	public int roundNo = 0;

	public GameObject[] slot;
	private Vector3[] velocity;
	public float smoothTime = 0.3f;
	public static bool getTempTransform;
	private Vector3[] tempPos;
	public bool mixDone;
	public int mixTimes;

	void Start () {
		Stage4_Controller.q [20] = true;
		returnRandom ();

		tempPos = new Vector3[2];
		velocity = new Vector3[2];
		for (int i = 0; i < velocity.Length; i++) {
			velocity [i] = Vector3.zero;
		}
		getTempTransform = false;
	}

	void Update () {
		if (Stage4_Controller.q [20] && !Stage4_Controller.q [21]) {
			Q21_popupGame ();
		} else if (Stage4_Controller.q [21] && !Stage4_Controller.q [22]) {
			Q22_startGame ();
		}
	}

	void Q21_popupGame(){
		//conversation
		//Pop up Game UI
		ShowMenu(fraudPanel);
		Stage4_Controller.q [21] = true;
	}

	void Q22_startGame(){
		if (q22_0) {
			if (mixDone) {
				returnRandom ();
			} else if (!mixDone) {
				Mix (number [0], number [1]);
				if (mixTimes >= 20) { // Mix Done Completely
					clickable = true;
					Stage4_Controller.q [22] = true;
				}
			}
		}
	}

	public void tempComplete(){
		HideNeogulman ();
		q22_0 = true;
	}

	public void Restart(){
		Destroy (neogulman);
		for (int i = 0; i < slot.Length; i++) {
			slot [i].GetComponent<Stage4_6_Fraud> ().neogulmanIn = false;
			slot [i].GetComponent<Image> ().enabled = true;
		}
	}

	public void HideNeogulman(){
		int answer = Random.Range (0, 3);
		neogulman = Instantiate (neogulmanPrefab, slot[answer].transform.position, Quaternion.identity) as GameObject;
		neogulman.transform.SetParent (slot [answer].transform);
		slot [answer].GetComponent<Stage4_6_Fraud> ().neogulmanIn = true;
	}

	int[] returnRandom(){
		number [0] = Random.Range (0, 3);
		number [1] = Random.Range (0, 3);
		while (number [0] == number [1]) {
			number [1] = Random.Range (0, 3);
		}
		Debug.Log (number [0] + ", " + number [1]);
		mixDone = false;
		return number;
	}

	void ShowMenu(Transform menu)
	{
		menu.GetComponent<Animator>().SetBool("show", true);
	}
		
	public void Mix(int a, int b){
		if (!getTempTransform) {
			tempPos [0] = slot [a].transform.position;
			tempPos [1] = slot [b].transform.position;
			getTempTransform = true;
		}
		//Debug.Log ("tempPos[0] = " + tempPos [0] + " = " + slot[b].transform.position);
		//Debug.Log (Vector3.Distance (slot [a].transform.position, tempPos [1]));
		//Debug.Log ("tempPos[1] = " + tempPos [1] + " = " + slot[a].transform.position);

		slot [a].transform.position = Vector3.SmoothDamp (slot[a].transform.position, tempPos[1], ref velocity[0], smoothTime);
		slot [b].transform.position = Vector3.SmoothDamp (slot[b].transform.position, tempPos[0], ref velocity[1], smoothTime);

		if (Vector3.Distance (slot [a].transform.position, tempPos [1]) <= 0.1f && Vector3.Distance(slot[b].transform.position, tempPos[0]) <= 0.1f) {
			mixTimes++;
			mixDone = true;	
			getTempTransform = false;
			Debug.Log (mixDone + ", " + mixTimes);
		}
	}

}
