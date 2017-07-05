using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextClose : MonoBehaviour {
    void Update()
    {
        if (Input.GetMouseButton(0)) this.gameObject.SetActive(false);
    }
}
