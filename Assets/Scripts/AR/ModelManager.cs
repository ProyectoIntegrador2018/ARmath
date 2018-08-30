using UnityEngine;
using System.Collections;

public class ModelManager : MonoBehaviour {

    public GameObject model;
    public TextMesh textModel;
    public GameObject textButton;
    public MegafierManager megafierManager;

    private GameObject[] prefabList;
    private int i;

    public void setModel(string model_name, string static_model_name, string text, bool megafiers)
    {
        //Hide 3d text of press button
        NGUITools.SetActive(textButton, false);

        //Check if has a model already
        if (model != null)
        {
            Destroy(model);//Delete past model
            model = null;
            Resources.UnloadUnusedAssets(); //Free resources
        }

        //Stop All Coroutines(stop all animations)
        StopAllCoroutines();

        //Load New Model
        if (megafiers) //Check if the model has a megafier component
            model = megafierManager.loadModel(gameObject, model_name);
        else
            model = NGUITools.AddChild(gameObject, Resources.Load<GameObject>("Models/" + model_name));
        model.transform.name = "model";

        //Get List of prefabs
        if (model.GetComponent<ListModel>())
        {
            prefabList = model.GetComponent<ListModel>().prefabList;
            i = 0; //reset pos
            //Load first model
            PlayList(model.transform.GetChild(0).GetComponent<Animation>().clip.length);
        }

        ////Set Static
        //if (!string.IsNullOrEmpty(static_model_name))
        //    StartCoroutine("waitStatic", static_model_name);

        //Set Text
        if (!string.IsNullOrEmpty(text))
        {
            textModel.text = text;
            StartCoroutine("waitText");
        }
        else
            textModel.text = "";
        NGUITools.SetActive(textModel.gameObject, false);
    }

    void PlayList(float length)
    {
        if(prefabList.Length > 0 && i < prefabList.Length)
            StartCoroutine("changeModel", new object[2]{length, prefabList[i]}); //Call coroutine to change to the next prefab
    }

    IEnumerator changeModel(object[] parms)
    {
        float length = (float)parms[0];
        GameObject nextPrefab = (GameObject)parms[1];

        //Wait to finish current animation
        yield return new WaitForSeconds(length);
        //Get Parent
        GameObject parent = model.transform.GetChild(0).gameObject;

        //Delete all childs that are not the grid
        foreach (Transform child in parent.transform)
        {
            if (child.name == "grid")
                continue;
            Destroy(child.gameObject);
        }

        //Load next model
        GameObject nextModel = NGUITools.AddChild(parent, nextPrefab);
        nextModel.transform.name = nextPrefab.transform.name;

        //Free memory
        Resources.UnloadUnusedAssets();

        //Call play list
        i++; //move on prefab list to the next one
        if (nextModel.GetComponent<Animation>()) //Call play list if has an animation component
            PlayList(nextModel.GetComponent<Animation>().clip.length);
        else if (nextModel.transform.GetChild(0).GetComponent<Animation>())
            PlayList(nextModel.transform.GetChild(0).GetComponent<Animation>().clip.length);
    }

    //IEnumerator waitStatic(string model_name)
    //{
    //    //Wait to finish animation
    //    yield return new WaitForSeconds(model.transform.GetChild(0).animation.clip.length);
    //    //Get Parent
    //    GameObject parent = model.transform.GetChild(0).gameObject;
    //    foreach (Transform child in parent.transform)
    //    {
    //        if (child.name == "grid")
    //            continue;
    //        Destroy(child.gameObject);
    //    }

    //    //Load static model
    //    GameObject static_model = NGUITools.AddChild(parent, Resources.Load<GameObject>("Models/" + model_name));
    //    static_model.transform.name = "recorrido";

    //    Resources.UnloadUnusedAssets();
    //}

    IEnumerator waitText()
    {
        yield return new WaitForSeconds(model.transform.GetChild(0).GetComponent<Animation>().clip.length);
        NGUITools.SetActive(textModel.gameObject, true);
    }

    /// <summary>
    /// Erase the model and show the 3d text
    /// </summary>
    public void Reset()
    {
        StopAllCoroutines();    //Stop all coroutines
        if (model != null)
        {
            Destroy(model);
            Resources.UnloadUnusedAssets();
            model = null;
        }
        textModel.text = "";
        NGUITools.SetActive(textModel.gameObject, false);
        NGUITools.SetActive(textButton, true);
    }
}
