using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_8_GameController : Controller {

	private bool active;
	public AudioSource leftSound;
	public GameObject portal1, portal2;
	public GameObject startPos;

    void Awake()
    {
        sceneNo = 38;
    }

    void Start () {
		active = true;
		leftSound = GetComponent<AudioSource> ();
		GameObject.FindWithTag ("Player").transform.position = startPos.transform.position;
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		if (Stage3_Controller.q[21]) {
			Destroy (GameObject.Find ("basil"));
		}
	}

	void Update () {
		if (Stage3_Controller.q[21] && active) {
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
