using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image bgImg;
	public Image joystickImg;
	public Vector3 inputVetcor;
	public Moving_by_RLbuttons mbr;
	private GameObject player;

	private bool exit_drag = false;

	private void Awake(){
		player = GameObject.Find ("Player");
		//mbr = player.GetComponent<Moving_by_RLbuttons> ();
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVetcor = new Vector3 (pos.x, 0, pos.y);
			inputVetcor = (inputVetcor.magnitude > 0.7f) ? inputVetcor.normalized : inputVetcor;
			//print (inputVetcor.magnitude);
			//move joystick Img
			joystickImg.rectTransform.anchoredPosition = new Vector3(inputVetcor.x * (bgImg.rectTransform.sizeDelta.x/2), inputVetcor.z * (bgImg.rectTransform.sizeDelta.y/2));
			mbr.now_Draged = true;
		}
		if (exit_drag) {
			OnPointerUp (ped);
			exit_drag = false;
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		inputVetcor = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
		mbr.now_Draged = false;
	}

	public void FixedUpdate(){

		if (player.activeSelf && mbr.isActiveAndEnabled) {//player active일때만
			if (inputVetcor.x > 0) {//오른쪽
				mbr.Moving_Right (inputVetcor.x * 8f);
			} else if (inputVetcor.x < 0 && mbr.isActiveAndEnabled) {//왼쪽
				mbr.Moving_left (inputVetcor.x * 8f);
			}
		} else {
			exit_drag = true;
		}
	}
}
