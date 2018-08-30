using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MegafierManager : MonoBehaviour {

    public List<string> keys;
    public List<GameObject> models;
    //private Dictionary<string, GameObject> dicModel;

    //void Awake()
    //{
    //    dicModel = new Dictionary<string, GameObject>(keys.Count);
    //    for (int i = 0; i < keys.Count; i++)
    //        dicModel.Add(keys[i], models[i]);
    //}

    public GameObject loadModel(GameObject parent, string keyModel)
    {
        int index = keys.FindIndex(item=>item.StartsWith(keyModel)); //Return index of the model
        GameObject model = MegaCopyObject.DoCopyObjects(models[index]); //Get model
        model.transform.parent = parent.transform; //Set child of the mark
        model.transform.localPosition = Vector3.zero;
        model.transform.localPosition = Vector3.zero;
        model.transform.localScale = Vector3.one;
        model.transform.name = models[index].transform.name;
        NGUITools.SetActive(model, true); //Activating the model
        return model; //Returning the model
    }
}
