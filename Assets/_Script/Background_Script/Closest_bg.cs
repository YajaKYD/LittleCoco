using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closest_bg : MonoBehaviour {

	public float speed;

	private GameObject _player;
	private Vector3 pastPosition;
	private Vector3 nowPosition;

	void Awake () {
		speed = -1f;
		_player = GameObject.Find ("Main Camera");
		//this.transform.position = new Vector3 (_player.transform.position.x, transform.position.y, transform.position.z);
		pastPosition = _player.transform.position;
		nowPosition = _player.transform.position;
	}

	void LateUpdate () {

		nowPosition = _player.transform.position;

		if (pastPosition != nowPosition) {
			transform.position = new Vector3 ( (this.transform.position.x + (nowPosition.x - pastPosition.x) / speed), 0f, -5f);
			pastPosition = nowPosition;
		}
	}
}
