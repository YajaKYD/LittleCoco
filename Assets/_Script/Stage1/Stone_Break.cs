using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Break : MonoBehaviour {

	public GameObject mirror;

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("HandMirror")) {
			coll.gameObject.GetComponent<Rigidbody2D> ().Sleep ();
			if (mirror != null && mirror.activeSelf) {
				mirror.SetActive (false);
			}
			StartCoroutine (Break_Stone ());
		}
	}

	IEnumerator Break_Stone(){
		Vector3[] aa = new Vector3[4];
		aa [0] = new Vector3 (0f, 0f, 5f);
		aa [1] = new Vector3 (0f, 0f, -5f);
		aa [2] = new Vector3 (0f, 0f, 5f);
		aa [3] = new Vector3 (0f, 0f, -5f);

		for (int i = 0; i < aa.Length; i++) {
			gameObject.transform.Rotate (aa [i]);
			yield return null;
			gameObject.transform.Rotate (Vector3.zero);
			yield return new WaitForSeconds (0.5f);
		}

		gameObject.SetActive (false);

	}
}
