using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Stage4_4_GameController : MonoBehaviour {

	public GameObject player;
	public bool movePlayer;
	public Vector2 playerPos;
	public Vector2 pos;
	public float speed;
	public float rotationSpeed;
	public SpriteRenderer blackout;

	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
		
	void FixedUpdate(){
		if (movePlayer) {
			Debug.Log ("Fixed Update" + playerPos + ", " + pos + " difference is " + (playerPos-pos));
			player.GetComponent<Rigidbody2D> ().AddForce ((pos-playerPos) * speed * Time.deltaTime);
			player.transform.Rotate (Vector3.forward * rotationSpeed);

			if (Vector3.Distance (player.transform.position, pos) <= 1) {
				movePlayer = false;
				player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				player.GetComponent<Rigidbody2D> ().angularVelocity = 0;
				GameObject.Find ("NewBackground(Clone)").SetActive (false);
				StartCoroutine ("FinishPosterPuzzle");
			}
		}
	}

	IEnumerator FinishPosterPuzzle(){
		Debug.Log ("finish 4_4");
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = blackout.color;
			c.a = f;
			blackout.color = c;
			yield return null;
		}
		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		PlayerPrefs.SetInt ("SceneFromWhere", SceneManager.GetActiveScene ().buildIndex);
	}
}
