using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModelStep : MonoBehaviour {


    public bool invertedOrder = false;

    private void OnEnable()
    {
        if (invertedOrder){
            StartCoroutine(EnableChildrenInverted());
        }
        else{
            StartCoroutine(EnableChildren());
        }

       
    }

    private void OnDisable()
    {
        DisableChilden();

    }


    private void DisableChilden(){
        foreach (Transform child in transform)
        {

            GameObject childGameObject = child.gameObject;
            childGameObject.SetActive(false);
        }
    }

    IEnumerator EnableChildrenInverted()
    {
        List<GameObject> childGameObjects = new List<GameObject>();


        foreach (Transform child in transform)
        {
            GameObject childGameObject = child.gameObject;
            childGameObjects.Add(childGameObject);
        }

        childGameObjects.Reverse();


        foreach(GameObject childGameObject in childGameObjects){

            yield return new WaitForSeconds(0.3f);
            childGameObject.SetActive(true);

        }

    }

    IEnumerator EnableChildren(){

        foreach(Transform child in transform){

            GameObject childGameObject = child.gameObject;
            yield return new WaitForSeconds(0.2f);
            childGameObject.SetActive(true);
        }

    }


}
