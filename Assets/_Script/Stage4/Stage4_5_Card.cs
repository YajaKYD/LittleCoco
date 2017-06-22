using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_Card : MonoBehaviour {


	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			if (i_c.Get_Item_Auto(11, GetComponent<SpriteRenderer>().sprite))
			{
				
				Destroy(this.gameObject);
			}
		}
	}
}
