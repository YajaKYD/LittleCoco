using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropTheBal : MonoBehaviour {

	private Item_Controller ic;
	public Sprite starImg;
    private Text_Importer2 ti;

    private bool q1a1 = false;

	void Awake(){
		ic = GameObject.FindWithTag ("Item_Canvas").GetComponent<Item_Controller> ();

        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        ti.Import(500);
    }

	/*void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player") && Stage5_Controller.q [18] && !Stage5_Controller.q[19]) {
			//GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ().enabled = false;
			GameObject ball = (GameObject)Instantiate (Resources.Load ("Prefabs/Ball"));
			Destroy (ball.GetComponent<Ball_popup> ());
			Destroy (ball.GetComponent<Player_get_Item> ());
			Destroy (ball.GetComponent<Destroy_tutorialMessage> ());
			//Destroy (ball.GetComponent<Rigidbody2D> ());
			//Destroy (ball.GetComponent<PolygonCollider2D> ());
			Physics2D.IgnoreCollision (GameObject.Find("Player").GetComponent<Collider2D> (), ball.GetComponent<Collider2D> (), true);
			ball.transform.position = this.transform.position;
			Auto_ItemUse ();
			Stage5_Controller.q [19] = true;
		}
	}*/

    void Update()
    {
        if (Stage5_Controller.q[18] && !Stage5_Controller.q[19])
        {
            GetComponent<Outline>().used_or_not_for_retry = false;
            ic._interaction_object[1] = "Player"; // 공을 임시로 player로 interaction object 바꾸기
            if (ic._now_used_item == "Ball")
            {
                //GameObject.Find ("Player").GetComponent<Moving_by_RLbuttons> ().enabled = false;
                GameObject ball = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
                Destroy(ball.GetComponent<Ball_popup>());
                Destroy(ball.GetComponent<Player_get_Item>());
                Destroy(ball.GetComponent<Destroy_tutorialMessage>());
                //Destroy (ball.GetComponent<Rigidbody2D> ());
                //Destroy (ball.GetComponent<PolygonCollider2D> ());
                Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), ball.GetComponent<Collider2D>(), true);
                ball.transform.position = this.transform.position;
                Auto_ItemUse();
                Stage5_Controller.q[19] = true;
            }
        }
        else if (Stage5_Controller.q[19] && !Stage5_Controller.q[20])
        {
            Star_GoBack();
        }
    }

    void Auto_ItemUse(){
		print ("DSF");
		for (int i = 0; i < ic._item_list.Length; i++) {
			if (ic._item_name_list [i] == "Ball") {
				ic._item_name_list [i] = "";
				ic._usable_item [i] = false;
				ic._interaction_object [i] = "";
				ic._the_number_of_items [i] = 0;
				ic._item_list [i].GetComponent<Image> ().color = new Color (1, 1, 1, 0);
				ic._item_list [i].transform.parent.GetComponentInChildren<Text> ().color = new Color (1, 1, 1, 0);
				ic._explanations [i] = "";
				break;
			}
		}
	}

    void Star_GoBack()
    {
        if (!q1a1)
        {
            ti.Talk();
            q1a1 = true;
        }
        else if (Stage5_Controller.q[54])
        {
            ic.Get_Item_Auto(5, starImg); // 탭으로 아이템 획득
            ic._interaction_object[1] = "";
            Stage5_Controller.q[20] = true;
        }
    }

/*	void OnMouseDown(){
		if (Stage5_Controller.q [19] && !Stage5_Controller.q[20]) {
			ic.Get_Item_Auto (5, starImg); // 탭으로 아이템 획득
            ic._interaction_object[1] = "";
			Stage5_Controller.q [20] = true;
		}
	}*/
}
