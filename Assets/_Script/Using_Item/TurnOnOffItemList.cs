using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnOffItemList : MonoBehaviour {

	private RectTransform aa;
	private bool a1 = false;

	public float OnTime;
	public RectTransform OnOffButton;

	void Awake(){
		aa = GetComponent<RectTransform> ();
	}

	void Update(){
		if (Time.realtimeSinceStartup - OnTime > 3f && a1) {
			TurnOnOffitemList ();
			a1 = false;
		}
	}

	public void TurnOnOffitemList(){
		if (OnOffButton.localScale.x == 1) { // 아이템창이 꺼져있는 상태
			StopAllCoroutines();
			StartCoroutine (MovingList (-95));
			OnOffButton.localScale = new Vector3 (-1f, 1f, 1f);
			OnTime = Time.realtimeSinceStartup;
			OnOffButton.GetComponent<Button> ().interactable = false;
			a1 = true;
			//OnOffButton.rotation = Quaternion.Euler (0f, 180f, 0f); 회전 시 작동안하게됨.

		} else { // 아이템창이 켜져있는 상태
			StopAllCoroutines();
			StartCoroutine (MovingList (95));
			OnOffButton.localScale = new Vector3 (1f, 1f, 1f);
			a1 = false;
			OnOffButton.GetComponent<Button> ().interactable = false;
			//OnOffButton.rotation = Quaternion.Euler (0f, 0f, 0f);
		}

	}

	IEnumerator MovingList(int x){
		if (x > 0) {
			while (true) {
				aa.position += new Vector3 (Time.deltaTime * 300f , 0f, 0f);
				if (aa.anchoredPosition.x >= x) {
					StopCoroutine ("MovingList");
					OnOffButton.GetComponent<Button> ().interactable = true;
					break;
				}
				yield return null;
			}
		} else {
			while (true) {
				aa.position -= new Vector3 (Time.deltaTime * 300f, 0f, 0f);
				if (aa.anchoredPosition.x <= x) {
					StopCoroutine ("MovingList");
					OnOffButton.GetComponent<Button> ().interactable = true;
					break;
				}
				yield return null;
			}
		}
	}
}
