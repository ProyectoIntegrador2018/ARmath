using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LayoutManager : MonoBehaviour {

    public List<GameObject> buttons;

    public void SetButtonText(int index, string text){

        Text buttonText = buttons[index].GetComponentInChildren<Text>();
        buttonText.text = text;
    }

}
