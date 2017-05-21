using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_8_GameController : MonoBehaviour {

	private bool active;
	public AudioSource leftSound;
	public GameObject portal1, portal2;
	public GameObject startPos;

	void Start () {
		active = true;
		leftSound = GetComponent<AudioSource> ();
		GameObject.FindWithTag ("Player").transform.position = startPos.transform.position;

		if (Stage3_Controller._Stage3_Quest[21]) {
			Destroy (GameObject.Find ("basil"));
		}
	}

	void Update () {
		if (Stage3_Controller._Stage3_Quest[21] && active) {
			StartCoroutine("WaitAndSound");
			active = false;
		}
	}

	IEnumerator WaitAndSound(){
		yield return new WaitForSeconds(2);
		leftSound.Play ();
		Debug.Log ("left sound");
		yield return new WaitForSeconds(2);
		portal1.GetComponent<BoxCollider2D> ().enabled = true;
		portal2.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
