using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutePackagebutton : MonoBehaviour {

	public UI_ImageAnimation[] uiAni;
	public int aninum = 0;

	public void RightButton(){
		if (aninum < uiAni.Length - 1) {
			uiAni [aninum].enabled = false;
			uiAni [++aninum].enabled = true;
		} else {
			uiAni [aninum].enabled = false;
			aninum = 0;
			uiAni [aninum].enabled = true;
		}
	}

	public void LeftButton(){
		if (aninum > 0) {
			uiAni [aninum].enabled = false;
			uiAni [--aninum].enabled = true;
		} else {
			uiAni [aninum].enabled = false;
			aninum = uiAni.Length - 1;
			uiAni [aninum].enabled = true;
		}
	}
}
