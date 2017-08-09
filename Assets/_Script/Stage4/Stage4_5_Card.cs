using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4_5_Card : MonoBehaviour {

	private Item_Controller ic;
	private Text_Importer2 ti;

	void Start () {
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();
		ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			if (ic.Get_Item_Auto(19, GetComponent<SpriteRenderer>().sprite))
			{
				Stage4_Controller.q18 [0] = true;
				ti.Talk (ti.lineNo + 2);
				Destroy(this.gameObject);
			}
		}
	}
}
