using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_5_LikeButton : MonoBehaviour {

<<<<<<< HEAD
	private Sprite likeOn, likeOff;
	private GameObject likeListUI;
	private Stage3_5_likeList likeListScript;
	private Stage3_5_GameController stage3_5_GameController;
	private Stage3_6_GameController stage3_6_GameController;
=======
    public Sprite likedButton;
    public Sprite likeButton;
    public GameObject likeListUI;
    public Stage3_5_likeList likeListScript;
    public Stage3_6_GameController controller;
>>>>>>> 2d9e80396e0292ace09424982220109bc235a130
    private Stage3_5_tree_coco tree;

    void Start()
    {
        tree = GetComponentInParent<Stage3_5_tree_coco>();
        transform.parent.gameObject.SetActive(false);
<<<<<<< HEAD
		try{
			likeListUI = GameObject.Find("quest12_likelist(Clone)");
			likeListScript = likeListUI.GetComponent<Stage3_5_likeList>();
		} catch {
		}

		if (GameObject.Find ("Stage3_5_GameController") != null) {
			stage3_5_GameController = GameObject.Find ("Stage3_5_GameController").GetComponent<Stage3_5_GameController> ();
			likeOn = stage3_5_GameController.likeOn;
			likeOff = stage3_5_GameController.likeOff;
		} else if (GameObject.Find("Stage3_6_GameController") != null)
        {
			stage3_6_GameController = GameObject.Find ("Stage3_6_GameController").GetComponent<Stage3_6_GameController> ();
			likeOn = stage3_6_GameController.likeOn;
			likeOff = stage3_6_GameController.likeOff;
        }
			
		if (tree.ID >= 1 && tree.ID <= 3 && Stage3_Controller._Stage3_Quest [tree.ID + 15]) {
			GetComponent<SpriteRenderer>().sprite = likeOn;
		} else {
			GetComponent<SpriteRenderer>().sprite = likeOff;
		}
    }

	void OnEnable(){
		try{
			likeListUI = GameObject.Find("quest12_likelist(Clone)");
			likeListScript = likeListUI.GetComponent<Stage3_5_likeList>();
		} catch {
		}
	}
    	
    void OnMouseDown()
    {
		Debug.Log ("clicked");
        if (tree.like)
        {
			GetComponent<SpriteRenderer>().sprite = likeOff;
            tree.like = false;
			if (tree.ID >= 1 && tree.ID <= 3) {
				Stage3_Controller._Stage3_Quest [tree.ID + 15] = false;
				showHeart (tree.ID - 1);
			}
//            if (tree.ID == 1)
//            {
//				Stage3_Controller._Stage3_Quest[16] = false;
//                showHeart(0);
//            }
//            else if (tree.ID == 2)
//            {
//				Stage3_Controller._Stage3_Quest[17] = false;
//                showHeart(1);
//            }
//            else if (tree.ID == 3)
//            {
//				Stage3_Controller._Stage3_Quest[18] = false;
//                showHeart(2);
//            }
        }
        else {
			GetComponent<SpriteRenderer>().sprite = likeOn;
            tree.like = true;
			if (tree.ID >= 1 && tree.ID <= 3) {
				Stage3_Controller._Stage3_Quest [tree.ID + 15] = true;
				showHeart (tree.ID - 1);
			}
//            if (tree.ID == 1)
//            {
//				Stage3_Controller._Stage3_Quest[16] = true;
//                showHeart(0);
//            }
//            else if (tree.ID == 2)
//            {
//				Stage3_Controller._Stage3_Quest[17] = true;
//                showHeart(1);
//            }
//            else if (tree.ID == 3)
//            {
//				Stage3_Controller._Stage3_Quest[18] = true;
//                showHeart(2);
//            }
        }

		if (stage3_6_GameController != null)
        {
			stage3_6_GameController.activatePortal();
        } 
=======
        if (GameObject.Find("Stage3_6_GameController"))
        {
            controller = GameObject.Find("Stage3_6_GameController").GetComponent<Stage3_6_GameController>();
            //Debug.Log("Find controller");
        }
    }
    	
    void OnMouseDown()
    {
        if (tree.like)
        {
            GetComponent<SpriteRenderer>().sprite = likeButton;
            tree.like = false;
            if (tree.ID == 1)
            {
				Stage3_Controller._Stage3_Quest[16] = false;
                showHeart(0);
            }
            else if (tree.ID == 2)
            {
				Stage3_Controller._Stage3_Quest[17] = false;
                showHeart(1);
            }
            else if (tree.ID == 3)
            {
				Stage3_Controller._Stage3_Quest[18] = false;
                showHeart(2);
            }
        }
        else {
            GetComponent<SpriteRenderer>().sprite = likedButton;
            tree.like = true;
            if (tree.ID == 1)
            {
				Stage3_Controller._Stage3_Quest[16] = true;
                showHeart(0);
            }
            else if (tree.ID == 2)
            {
				Stage3_Controller._Stage3_Quest[17] = true;
                showHeart(1);
            }
            else if (tree.ID == 3)
            {
				Stage3_Controller._Stage3_Quest[18] = true;
                showHeart(2);
            }
        }

        if (controller != null)
        {
            controller.activatePortal();
        } else
        {
            //Debug.Log("not working");
        }
>>>>>>> 2d9e80396e0292ace09424982220109bc235a130
    }
    
    void showHeart(int i)
    {
<<<<<<< HEAD
=======
        likeListUI = GameObject.Find("quest12_likelist(Clone)");
        likeListScript = likeListUI.GetComponent<Stage3_5_likeList>();
>>>>>>> 2d9e80396e0292ace09424982220109bc235a130
        if (tree.like && likeListUI != null)
        {
            likeListScript.heart[i].GetComponent<Image>().enabled = true;
        } else if(!tree.like && likeListUI != null)
        {
            likeListScript.heart[i].GetComponent<Image>().enabled = false;
        }
    }
}
