using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farther_9_bg : MonoBehaviour {
	
	public float speed;
	public float var_speed;
	private GameObject _player;
	private Vector3 pastPosition;
	private Vector3 nowPosition;
	private Vector3 past_backposition;

	void Awake () {
		speed = 2f;
		_player = GameObject.Find ("Main Camera");
		this.transform.position = new Vector3 (_player.transform.position.x, transform.position.y, transform.position.z);
		pastPosition = _player.transform.position;
		nowPosition = _player.transform.position;
	}
	void LateUpdate () {

		nowPosition = _player.transform.position;

		if (pastPosition != nowPosition && var_speed == 0f) {
			transform.position = new Vector3 ((this.transform.position.x + (nowPosition.x - pastPosition.x) / speed), 0f, 9f);
			pastPosition = nowPosition;
		} else if (pastPosition != nowPosition && var_speed != 0f) {
			transform.position = new Vector3 ((this.transform.position.x + (nowPosition.x - pastPosition.x) / var_speed), 0f, 9f);
			pastPosition = nowPosition;
		}

	}
}
