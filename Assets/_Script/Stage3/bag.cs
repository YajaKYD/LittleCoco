using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag : MonoBehaviour {
	
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			Stage3_Controller.q[7] = true;
			//Save_Script.Save_Quest_Info ();
        }
    }
}
