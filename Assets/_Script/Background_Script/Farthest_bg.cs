using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farthest_bg : MonoBehaviour {

	public Camera _camera;
	public float xpos;
	public float ypos;

	void Awake(){
		_camera = Camera.main;
	}

	void LateUpdate () {

		this.transform.position = new Vector3(_camera.transform.position.x + xpos, _camera.transform.position.y + ypos, this.transform.position.z);

	}
}
