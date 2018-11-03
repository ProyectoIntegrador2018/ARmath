using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIColorManager : MonoBehaviour {

    public UnityEngine.Gradient _effectGradient = new UnityEngine.Gradient() { colorKeys = new GradientColorKey[] { new GradientColorKey(Color.black, 0), new GradientColorKey(Color.white, 1) } };
    public Color accentColor;


   public void ChangeBackgroundColor(){

        GameObject[] backgroundGameObjects = GameObject.FindGameObjectsWithTag("GradientBackground");
        GameObject[] titleGameObjects = GameObject.FindGameObjectsWithTag("ColorTextTitle");

        foreach(GameObject titleGameObject in titleGameObjects)
        {
            titleGameObject.GetComponent<Text>().color = accentColor;
        }
    }
}
