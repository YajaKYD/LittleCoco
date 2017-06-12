using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class Game_Controller_0_2 : MonoBehaviour {

	private GameObject player;
	private Transform start_pos;
	private GameObject _ic;
	//public Portal_Controller end_portal;
	public AudioSource bgm;
	public Tutorial_Controller tc;
	public SpriteRenderer _blackout;
	public GameObject _Canvas;


	void Awake(){
		player = GameObject.Find ("Player");
		start_pos = GameObject.Find ("Start_pos").transform;
		_ic = GameObject.FindWithTag ("Item_Canvas");
		player.transform.position = start_pos.position;
		tc = GameObject.FindWithTag ("Controller").GetComponent<Tutorial_Controller>();
		tc.tutorialMessageIndex++;
		bgm = tc.gameObject.GetComponentInChildren<AudioSource> ();
		//bgm = tc.GetComponentInChildren<AudioSource> ();
	}

	void Start(){
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			Selecting_stage._what_stage_now_cleared = 0; //stage1 open
			StartCoroutine ("FadeOut_sound");
			StartCoroutine ("FadeOut");

		}
	}

	void Update(){
		if (_Canvas.activeSelf && Input.GetMouseButton(0)) {
			SceneManager.LoadScene (3);
		}
	}

	void OnDisable(){
		_ic.SetActive (true);
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = true;
	}

	public void Active_Text(){
		_Canvas.GetComponentInChildren<Text> ().enabled = true;
	}

	IEnumerator FadeOut(){
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _blackout.color;
			c.a = f;
			_blackout.color = c;
			yield return null;
		}
		_ic.SetActive (false);
		_Canvas.SetActive (true);
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = false;
		Handheld.PlayFullScreenMovie ("Tutorial_ending.mp4",Color.black,FullScreenMovieControlMode.Hidden);
		Invoke ("Active_Text", 2f);
		//player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
	}

	IEnumerator FadeOut_sound(){
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			bgm.volume -= 0.0001f;
			if (bgm.volume <= 0) {
				StopCoroutine ("FadeOut");
				break;
			}
			yield return null;
		}
		Destroy (tc.gameObject);
	}
}
