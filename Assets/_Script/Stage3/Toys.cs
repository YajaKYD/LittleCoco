using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys : MonoBehaviour {

	public int _this_num;
	public GameObject _player;
	public bool _pickable = false;
	private Item_Controller i_c;

	private int num_of_balls = 0;
	void Awake(){
		_player = GameObject.FindWithTag ("Player");
		i_c = GameObject.Find ("Item_Canvas").GetComponent<Item_Controller>();
		//멍멍이와의 충돌만 무시함.
		Physics2D.IgnoreCollision (_player.GetComponent<Collider2D> (), GetComponent<Collider2D> (), true);
	}
		

	void OnMouseDown(){
		for (int i = 0; i < i_c._item_list.Length; i++) {
			if (i_c._item_name_list [i] == "ball1" || i_c._item_name_list [i] == "ball2" || i_c._item_name_list [i] == "ball3") {
				num_of_balls++;
			}
		}

		if (_pickable && num_of_balls < 2) {
			if (i_c.Get_Item_Auto (_this_num, GetComponent<SpriteRenderer> ().sprite)) {
				//i_c.Get_Item_Auto ("장난감" + _this_num, GetComponent<SpriteRenderer> ().sprite, true, "Air", true);

				if (_this_num == 11) {
					Stage3_Controller.q[3] = true;
					//Save_Script.Save_Quest_Info ();
				} else if (_this_num == 12) {
					Stage3_Controller.q[4] = true;
					//Save_Script.Save_Quest_Info ();
				} else if (_this_num == 13){
					Stage3_Controller.q[5] = true;
					//Save_Script.Save_Quest_Info ();
				}
				Destroy (this.gameObject);
			}
		}

		num_of_balls = 0;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (i_c.Get_Item_Auto(11, GetComponent<SpriteRenderer>().sprite))
            {
                if (_this_num == 11)
                {
					Stage3_Controller.q[3] = true;
					//Save_Script.Save_Quest_Info ();
                }
                else if (_this_num == 12)
                {
					Stage3_Controller.q[4] = true;
					//Save_Script.Save_Quest_Info ();
                }
                else {
					Stage3_Controller.q[5] = true;
					//Save_Script.Save_Quest_Info ();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
