using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4_4_poster : MonoBehaviour{

	private Stage4_4_GameController controller;
	public GameObject otherman;
	private GameObject player;
	private Vector2 pos;
	private SpriteRenderer fade;

	void Start () {
		controller = GameObject.Find ("Stage4_4_GameController").GetComponent<Stage4_4_GameController>();
		player = GameObject.FindWithTag ("Player");
		controller.poster = this;
		fade = GetComponent<SpriteRenderer> ();
	}
		
	void OnMouseDown(){
		Debug.Log ("mouse down");
		if (!Stage4_Controller.q [16]) {
			StartCoroutine ("ChangeImage");
		}
	}

	IEnumerator ChangeImage(){
		//player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		//controller.playerPos = player.transform.position;

		for (float a = 0f; a < 1f; a+=Time.deltaTime) {
			Color c = fade.color;

			c.a = 1 - a;
			Debug.Log (c);
			fade.color = c;
			yield return null;
		}

		otherman.SetActive (true);
		//this.gameObject.SetActive (false);

		controller.q16_0 = true;
		//need animation effect
		//pos = this.transform.position;
		//controller.pos = pos;
		//yield return new WaitForSeconds(1);

		//player.GetComponent<Rigidbody2D> ().gravityScale = 0; 
		//controller.movePlayer = true;
		//Stage4_Controller.q [16] = true;
		yield return null;
	}

	public void DisappearPoster(){
		//animation
		transform.parent.gameObject.SetActive (false);
	}
}
