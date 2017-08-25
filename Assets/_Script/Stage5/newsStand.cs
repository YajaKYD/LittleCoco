using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newsStand : MonoBehaviour {

    private Item_Controller ic;
    public Sprite paperImg;

    void Awake()
    {
        ic = GameObject.FindWithTag("Item_Canvas").GetComponent<Item_Controller>();
    }

    void OnMouseDown()
    {
        if (Stage5_Controller.q[29] && !Stage5_Controller.q[30])
        {
            ic.Get_Item_Auto(20, paperImg);
            Stage5_Controller.q[30] = true;
        }    
    }
}
