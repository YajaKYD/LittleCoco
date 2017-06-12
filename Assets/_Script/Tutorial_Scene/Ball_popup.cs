using UnityEngine;
using System.Collections;

public class Ball_popup : MonoBehaviour {

	public float _speed;

	private Rigidbody2D rb;

	void Start() {

		this.name = "공";
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (-_speed, _speed);

	}

}
