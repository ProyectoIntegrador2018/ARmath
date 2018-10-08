using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DynamicGrid : MonoBehaviour {



    private int numberOfButtons = 4;

    void Update()
    {

        if(numberOfButtons < 3){
            numberOfButtons = 3;
        }

        float width = this.GetComponent<RectTransform>().rect.width;
        float height = this.GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2(width - width * 0.05f, (height/numberOfButtons) - (height / numberOfButtons) * 0.05F);
        this.GetComponent<GridLayoutGroup>().cellSize = newSize;
        this.GetComponent<GridLayoutGroup>().spacing = new Vector2(width * 0.05f, (height / numberOfButtons) * 0.05F);
    }

    public void setNumberOfButtons(int number){

        numberOfButtons = number;
    }

}
