using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_6_GameController : Stage3_5_GameController
{
	public GameObject portal;
    
    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + ", sceneIndex is " + Stage3_Controller.sceneIndex);
		if (Stage3_Controller.sceneIndex > SceneManager.GetActiveScene ().buildIndex || Stage3_Controller.sceneIndex==0) {
			Save_Script.Save_Now_Point ();
			player.transform.position = end_pos.position;
		}
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + ", sceneIndex is " + Stage3_Controller.sceneIndex);

        activatePortal();
		if (Stage3_Controller._Stage3_Quest[19]) {
			ti.currLineArr[1] += 2; //이본 다음대사 넘김
			ti.NPC_Say_yeah ("이본"); // Ivon calls Coco
			portal.GetComponent<BoxCollider2D>().enabled = false; // deactivate portal to 3_7
			background_far.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			background_far1.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			for (int i = 0; i < trees.Length; i++) {
				trees [i].GetComponent<PolygonCollider2D> ().enabled = false;
			}
		}


    }
    
    public void activatePortal()
    {
		if(Stage3_Controller._Stage3_Quest[16] && Stage3_Controller._Stage3_Quest[17] && Stage3_Controller._Stage3_Quest[18])
        {
            portal.GetComponent<BoxCollider2D>().enabled = true;
			Stage3_Controller._Stage3_Quest[15] = true;
            Debug.Log("likebutton" + likeButton.Length);
            for (int i = 0; i < likeButton.Length; i++)
                {
                    likeButton[i].GetComponent<PolygonCollider2D>().enabled = false; // like 버튼 비활성화
                }
        } else
        {
            portal.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
