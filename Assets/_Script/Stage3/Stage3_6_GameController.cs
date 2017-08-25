using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3_6_GameController : Stage3_5_GameController
{
	public GameObject portal;
    
	public SpriteRenderer _whiteOut;
	public GameObject _endingText;
	public static bool whiteOut_3_6 = false;

	public Sprite waterc1;
	public Sprite waterc2;
	public AnimateSprite asp;

    new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        ti.Import(36);
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + ", sceneIndex is " + Stage3_Controller.sceneIndex);
		if (Stage3_Controller.sceneIndex > SceneManager.GetActiveScene ().buildIndex || Stage3_Controller.sceneIndex==0) {
			Save_Script.Save_Now_Point ();
			player.transform.position = end_pos.position;
		}
		Stage3_Controller.sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		Debug.Log (name + "buildIndex is " + SceneManager.GetActiveScene ().buildIndex + ", sceneIndex is " + Stage3_Controller.sceneIndex);

        activatePortal();
		if (Stage3_Controller.q[19]) {

			//whiteout//
			//print("whiteout");


			GameObject.FindWithTag("Player").transform.localScale = new Vector3 (1f, 1f, 1f);
			GameObject _park = GameObject.FindWithTag("Controller").transform.GetChild (1).gameObject;
			_park.SetActive (true);
			asp.vSpriteList [0] = waterc1;
			asp.vSpriteList [1] = waterc2;

			portal.GetComponent<BoxCollider2D>().enabled = false; // deactivate portal to 3_7
			background_far.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			background_far1.GetComponent<SpriteRenderer> ().sprite = background_far_img;
			for (int i = 0; i < trees.Length; i++) {
				trees [i].GetComponent<BoxCollider2D> ().enabled = false;
			}

			if (!whiteOut_3_6) {
				StartCoroutine ("WhiteOut");
				whiteOut_3_6 = false;
			}
		}
    }
    
    public void activatePortal()
    {
		if(Stage3_Controller.q[16] && Stage3_Controller.q[17] && Stage3_Controller.q[18])
        {
            portal.GetComponent<BoxCollider2D>().enabled = true;
			Stage3_Controller.q[15] = true;
            Debug.Log("likebutton" + likeButton.Length);
            for (int i = 0; i < likeButton.Length; i++)
                {
                    likeButton[i].GetComponent<BoxCollider2D>().enabled = false; // like 버튼 비활성화

                }
        } else
        {
            portal.GetComponent<BoxCollider2D>().enabled = false;
        }
    }


	IEnumerator WhiteOut(){
		GameObject.FindWithTag("Item_Canvas").GetComponent<Canvas> ().enabled = false;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = false;
		player.GetComponent<Moving_by_RLbuttons> ().enabled = false;
		yield return new WaitForSeconds (1f);

		for (float f = 0f; f < 1; f += Time.deltaTime) {
			Color c = _whiteOut.color;
			c.a = f;
			_whiteOut.color = c;
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		//		for (int i = 0; i < _yellowThings.Length; i++) {
		//			_yellowThings [i].SetActive (true);
		//		}
		PopUpEndingText ();
		StartCoroutine ("WhiteIn");
	}

	IEnumerator WhiteIn(){

		for (float f = 1f; f > 0; f -= Time.deltaTime) {
			Color c = _whiteOut.color;
			c.a = f;
			_whiteOut.color = c;
			yield return null;
		}

		player.GetComponent<Moving_by_RLbuttons> ().enabled = true;

		GameObject.FindWithTag("Item_Canvas").GetComponent<Canvas> ().enabled = true;
		GameObject.FindWithTag ("Setting").GetComponent<Canvas> ().enabled = true;
        //글자띄우고.

        ti.Talk(); // Ivon calls Coco
	}

	void PopUpEndingText(){
		_endingText.SetActive (true);
		Invoke ("DestroyEndingText", 3f);
	}

	void DestroyEndingText(){
		Destroy (_endingText);
	}
}
