using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextClose : MonoBehaviour {
    private bool check_click = false;
    void Update()
    {
        if (Input.GetMouseButton(0) && check_click) this.gameObject.SetActive(false);
        else if (!Input.GetMouseButton(0)) check_click = true;
    }
}
