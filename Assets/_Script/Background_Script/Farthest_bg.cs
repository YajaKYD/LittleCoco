using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farthest_bg : MonoBehaviour {

	public Camera _camera;
	public float aaa;

	void Awake(){
		_camera = Camera.main;
	}

	void LateUpdate () {

		this.transform.position = new Vector3(_camera.transform.position.x + aaa, 0, this.transform.position.z);

	}
}
