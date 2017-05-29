using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_9_GameController : MonoBehaviour {

	private bool active;
	public AudioSource rightSound;
	public GameObject portal1, portal2;
	public GameObject startPos;

	void Start () {
		active = true;
		rightSound = GetComponent<AudioSource> ();
		GameObject.FindWithTag ("Player").transform.position = startPos.transform.position;
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
	}

	void Update () {
		if (active) {
			StartCoroutine("WaitAndSound");
			active = false;
		}	
	}

	IEnumerator WaitAndSound(){
		yield return new WaitForSeconds(2);
		rightSound.Play ();
		//Debug.Log ("right sound");
		yield return new WaitForSeconds(2);
		portal1.GetComponent<BoxCollider2D> ().enabled = true;
		portal2.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
