using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Moving_by_RLbuttons : MonoBehaviour {

	private Rigidbody2D player_rb;
	private Vector2 before_position;
	private Vector2 item_position_to_get;
	private bool click_to_get;
//	private Vector2 before_jump_position;

	public bool in_air = false;
	private bool check_before_jump = false;

	void Awake () {
		player_rb = GetComponent<Rigidbody2D> ();
		before_position = (Vector2)this.transform.position;
		DontDestroyOnLoad (transform.gameObject);
	}

	void FixedUpdate () {
		//테스트용//
		if (Input.GetAxis ("Horizontal") > 0) {
			Moving_Right (8f);
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			Moving_left (-8f);
		}
		if (Input.GetButton("Jump")){
			Jumping ();
		}

		if (Input.GetMouseButton (1) && Input.GetKeyDown(KeyCode.G)) {
			print ("Delete All data");
			PlayerPrefs.DeleteAll ();
		}

		if (Input.GetMouseButton (1) && Input.GetKey (KeyCode.P) && Input.GetKeyDown (KeyCode.O)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
		//테스트용//
		/*
		//개발용//
		if (Input.GetAxis ("Horizontal") > 0) {
			Moving_Right (20f);
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			Moving_left (-20f);
		}
		if (Input.GetButton("Jump")){
			Jumping ();
		}
		//개발용//
		*/
		if (click_to_get) {
			Moving_to_get ();
		}

		/*
		if (transform.position.y > before_jump_position.y + 2f) {
			player_rb.velocity = Vector2.down;
		}
		*/
	}

	void LateUpdate(){
		//움직일때만 발이 움직임//
		if (before_position == (Vector2)this.transform.position) {
			this.GetComponent<AnimateSprite> ().enabled = false;
		}
		before_position = (Vector2)this.transform.position;
		////////////////////////
	}

	public void Moving_left(float speed){ //왼쪽 이동
		transform.localRotation = Quaternion.Euler(new Vector3(0f,180f,0f));
		//transform.localScale = new Vector3 (-1f, 1f, 1f); //왼쪽보는 이미지
		this.transform.position = (Vector2)this.transform.position + new Vector2(speed * Time.deltaTime,0f);
		click_to_get = false;

		//player_rb.AddForce (new Vector2(speed * 20f * Time.deltaTime,0));
		this.GetComponent<AnimateSprite> ().enabled = true;
	}
	public void Moving_Right(float speed){ //오른쪽 이동
		transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,0f));
		//transform.localScale = new Vector3 (1f, 1f, 1f);//오른쪽보는 이미지
		this.transform.position = (Vector2)this.transform.position + new Vector2(speed * Time.deltaTime,0f);
		click_to_get = false;
		this.GetComponent<AnimateSprite> ().enabled = true;
	}

	public void Jumping(){
		 

		//before_jump_position = (Vector2)transform.position;

		if (player_rb.velocity == Vector2.zero) {//한번만 점프가능
			//player_rb.AddForce (new Vector2 (0, 320f));
			player_rb.velocity = new Vector2 (0, 6f);
			click_to_get = false;
			//this.GetComponent<Animator> ().enabled = true;
		}


		//테라리아같은 점프
		/*
		if (!check_before_jump) {
			before_jump_position = (Vector2)transform.position;
			check_before_jump = true;
		}
		if (!in_air) {
			if (transform.position.y < before_jump_position.y + 2f) {
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transform.position.x, transform.position.y + 10f), 10f * Time.deltaTime);
			} else {
				in_air = true;
			}
		}
		this.GetComponent<AnimateSprite> ().enabled = true;
		*/
	}
	public void Stop_Animation(){
		//this.GetComponent<Animator> ().enabled = false;
	}
	public void Click_item_to_get(Vector2 item_position){
		item_position_to_get = new Vector2(item_position.x, this.transform.position.y);
		click_to_get = true;
	}
	void Moving_to_get(){
		if (transform.position.x > item_position_to_get.x) {//아이템이 왼쪽
			transform.position = Vector2.MoveTowards (transform.position, item_position_to_get, 8f * Time.deltaTime);
			transform.localRotation = Quaternion.Euler(new Vector3(0f,180f,0f));
			//transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else if (transform.position.x < item_position_to_get.x) {//아이템이 오른쪽
			transform.position = Vector2.MoveTowards (transform.position, item_position_to_get, 8f * Time.deltaTime);
			transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,0f));
			//transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			click_to_get = false;
		}
	}

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.CompareTag("Background")){
			in_air = false;
			check_before_jump = true;
		}
	}

}
