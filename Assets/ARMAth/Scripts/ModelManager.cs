using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{

    public List<GameObject> sceneModels;
    private GameObject activeModel = null;

    public void SetActiveModel(int index)
    {
        if (activeModel != null)
        {
            activeModel.SetActive(false);
        }
        activeModel = sceneModels[index];
        activeModel.SetActive(true);

        if (activeModel.GetComponent<Animation>() != null)
        {
            activeModel.GetComponent<Animation>().Play();
        }

        if(activeModel.GetComponent<CullingPlaneMovement>() != null){

            activeModel.GetComponent<CullingPlaneMovement>().ResetPosition();
        }

        if(activeModel.GetComponent<AnimationPlayback>() != null){
            StartCoroutine(activeModel.GetComponent<AnimationPlayback>().StartPlayback());
           
        }

    }


    public void AwakeChildren(){
        foreach (Transform child in activeModel.transform)
        {
            if(child.gameObject.GetComponent<Animation>() != null ){
                child.gameObject.GetComponent<Animation>().Play();
            }


        }
    }

}    
