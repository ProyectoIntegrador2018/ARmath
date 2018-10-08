using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuDynamicGrid : MonoBehaviour {


    public int numberOfButtons;

    void Update()
    {
        float width = this.GetComponent<RectTransform>().rect.width;
        float height = this.GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2(width/numberOfButtons * 0.8f, height * 0.8f );
        this.GetComponent<GridLayoutGroup>().cellSize = newSize;
        this.GetComponent<GridLayoutGroup>().spacing = new Vector2(width / numberOfButtons * 0.1f, height * 0.1f);
    }


}
