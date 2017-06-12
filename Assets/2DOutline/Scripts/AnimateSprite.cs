using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSprite : MonoBehaviour {

	public List<Sprite> vSpriteList;

	private float vElapseTime = 0f;
	public float vCptTime = 0.15f;
	private SpriteRenderer vRenderer;
	private int vCptSprite = 0;

	// Use this for initialization
	void Start () {
		vRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (vRenderer != null) {
			vElapseTime += Time.deltaTime;
			if (vElapseTime >= vCptTime) {

				if (vCptSprite == vSpriteList.Count)
					vCptSprite = 0;
			
				vRenderer.sprite = vSpriteList [vCptSprite];
				if (GameObject.FindWithTag ("Player").GetComponent<Moving_by_RLbuttons> ().state == CocoState.GetItem) {
					print ("CHeck");
				}
				vElapseTime = 0f;
				vCptSprite++;
			}
		}
	}
}
