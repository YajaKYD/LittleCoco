using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_5_GameController : Controller {

    protected GameObject player;
    protected Transform start_pos;
	protected Transform end_pos;
    protected Item_Controller ic;
    protected Text_Importer2 ti;
	public GameObject portal3_4, portal3_6;

    //public Sprite npcTemp;
    public bool usable;
    public bool consumable;

    public GameObject[] trees;
    protected GameObject[] likeButton;
	public Sprite likeOn, likeOff;
	public GameObject background_far;
	public GameObject background_far1;
	public Sprite background_far_img;

	public GameObject logo;

    protected void Awake()
    {
        sceneNo = 35;
        player = GameObject.Find("Player");
        start_pos = GameObject.Find("Start_Pos").transform;
		end_pos = GameObject.Find ("End_Pos").transform;
        player.transform.position = start_pos.position;
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
        ti = GameObject.FindWithTag("Dialogue").GetComponent<Text_Importer2>();
        trees = GameObject.FindGameObjectsWithTag("Tree");
        likeButton = GameObject.FindGameObjectsWithTag("likeButton");
    }

    void Start() {
		//Save_Script.Save_Now_Point();
		Debug.Log ("buildIndex is " + SceneManager.GetActiveScene ().buildIndex + "sceneIndex is " + Stage3_Controller.sceneIndex);
		if (Stage3_Controller.sceneIndex > SceneManager.GetActiveScene ().buildIndex) {
			player.transform.position = end_pos.position;
		}
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		Debug.Log ("buildIndex is " + SceneManager.GetActiveScene ().buildIndex + "sceneIndex is " + Stage3_Controller.sceneIndex);

		if (!Stage3_Controller.q[14])
        {
			//ic.Get_Item_Auto (5, npcTemp); //temp code
            //ti.Import(15);
        }

		if (Stage3_Controller.q[15])
        {
            for (int i = 0; i < likeButton.Length; i++)
            {
                likeButton[i].GetComponent<Stage3_5_LikeButton>().enabled = false; // like 버튼 비활성화
            }
        }

		if (Stage3_Controller.q[19]) {

			logo.SetActive (false);

			portal3_4.GetComponent<BoxCollider2D> ().enabled = true;
			portal3_6.GetComponent<BoxCollider2D> ().enabled = false;
			background_far.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			background_far1.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			for (int i = 0; i < trees.Length; i++) {
				try{
					trees [i].GetComponent<BoxCollider2D> ().enabled = false;
				} catch {
				}
			}
		}
    }
	
	
}
