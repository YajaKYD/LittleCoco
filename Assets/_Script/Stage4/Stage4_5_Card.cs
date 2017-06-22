using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_Card : MonoBehaviour {
	public Item_Controller ic;

	void Start () {
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			if (ic.Get_Item_Auto(19, GetComponent<SpriteRenderer>().sprite))
			{
				Stage4_Controller.q18 [0] = true;
				Destroy(this.gameObject);
			}
		}
	}
}
