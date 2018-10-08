using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoResize : MonoBehaviour {

    void Start()
    {
        SetWidth();
    }


    private void SetWidth(){

        RectTransform rt = this.gameObject.GetComponent<RectTransform>();
        float height = rt.rect.height;
        float width = (height / 3) * 4;
        rt.sizeDelta = new Vector2(width, 0);


    }
	
}
