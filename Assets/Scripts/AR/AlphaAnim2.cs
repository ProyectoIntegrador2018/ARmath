using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AnimationType
{
    SameTime,
    Sequence
};

[System.Serializable]
public class Group
{
    public AnimationType type;
    public float lenght;
    public float targetAlpha;
    public GameObject[] models;
    [HideInInspector]
    public int pos_model = 0;
    [HideInInspector]
    public float[] alphas;
    [HideInInspector]
    public Hashtable hashtable;
}

public class AlphaAnim2 : MonoBehaviour {

    public bool reset;
    public bool reverse;
    public Group[] groups;
    private int pos_group = 0;

    void Awake()
    {
        saveAlphas(); //Save alphas of each group
		if(reset)
			resetAlphas();
        prepareAnimation();
    }

    void prepareAnimation()
    {
        int num_group = 0;
        foreach (Group group in groups)
        {
            group.hashtable = new Hashtable();
            group.hashtable.Add("alpha", group.targetAlpha); //target alpha of the models
            group.hashtable.Add("time", group.lenght);
            if (group.type == AnimationType.Sequence)
            {
                group.hashtable.Add("oncompletetarget", gameObject);
                group.hashtable.Add("oncompleteparams", num_group);
                group.hashtable.Add("oncomplete", "playNextModel");
            }
            else if (group.type == AnimationType.SameTime && group.targetAlpha <= 0)
            {
                group.hashtable.Add("oncompletetarget", gameObject);
                group.hashtable.Add("oncomplete", "disableModel");
            }
            num_group++;
        }
    }

    //Save alphas of the models
    void saveAlphas()
    {
        foreach (Group group in groups)
        {
            group.alphas = new float[group.models.Length]; //declare array of the length of the models
            //Iterate for each model in the group
            for (int i = 0; i < group.alphas.Length; i++)
            {
                if (group.models[i].GetComponent<Renderer>()) //getting the alpha value
                    group.alphas[i] = group.models[i].GetComponent<Renderer>().material.color.a;
                else if(group.models[i].transform.GetChild(0).GetComponent<Renderer>()) //if the parent doesnt have renderer check children
                    group.alphas[i] = group.models[i].transform.GetChild(0).GetComponent<Renderer>().material.color.a;
                else //if either one has renderer, save just 0 or 1 depending of the target
                {
                    if (group.targetAlpha > 0.5f)
                        group.alphas[i] = 0;
                    else
                        group.alphas[i] = 1;
                }
            }
        }
    }

    //Reset alpha to original value
    void resetAlphas()
    {
        foreach (Group group in groups)
        {
            int i = 0;
            foreach (GameObject model in group.models)
            {
                if (model.GetComponent<Renderer>())
                {
                    Color mycolor = model.GetComponent<Renderer>().material.color;
                    mycolor.a = group.alphas[i];
                    model.GetComponent<Renderer>().material.color = mycolor;
                    if (group.alphas[i] <= 0)
                        model.GetComponent<Renderer>().enabled = false;
                    else
                        model.GetComponent<Renderer>().enabled = true;
                }
                if (model.transform.childCount > 0)
                {
                    Renderer[] listRenderer = model.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in listRenderer)
                    {
                        Color mycolor = r.material.color;
                        mycolor.a = group.alphas[i];
                        r.material.color = mycolor;
                        if (group.alphas[i] <= 0)
                            r.enabled = false;
                        else
                            r.enabled = false;
                    }
                }
                i++;
            }
        }
    }

    public void PlayGroup()
    {
        StartCoroutine(playGroupCoroutine(pos_group, groups[pos_group])); // Play group
        pos_group++;
    }

    IEnumerator playGroupCoroutine(int idGroup, Group group)
    {
        ////Move next group
        //pos_group++;

        if (groups[pos_group].type == AnimationType.SameTime)
        {
            //Enable all renderers of each model on group first group
            foreach (GameObject model in groups[pos_group].models)
            {
                if (model.GetComponent<Renderer>())
                    model.GetComponent<Renderer>().enabled = true;
                if (model.transform.childCount > 0)
                {
                    Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
                    foreach (Renderer r in renderers)
                        r.enabled = true;
                }
                if (groups[pos_group].hashtable.ContainsKey("oncompleteparams"))
                    groups[pos_group].hashtable.Remove("oncompleteparams");
                if (groups[pos_group].targetAlpha <= 0)
                    groups[pos_group].hashtable.Add("oncompleteparams", model);

                if (!model.GetComponent<Renderer>().enabled)
                    model.GetComponent<Renderer>().enabled = true;
                iTween.FadeTo(model, groups[pos_group].hashtable);
            }
        }
        else
        {
            playNextModel(idGroup); //Calling first model of the group
        }
        yield return null;
    }

    public void playNextModel(int idGroup)
    {
        int _pos = groups[idGroup].pos_model; //Getting the pos of the model in the group
        if(_pos > 0)
        {
            GameObject model = groups[idGroup].models[_pos - 1];
            if (model.GetComponent<Renderer>())
                if(model.GetComponent<Renderer>().material.color.a == 0)
                model.GetComponent<Renderer>().enabled = false;
        }

        if (_pos < groups[idGroup].models.Length) //Check if there are more models in the group
        {
            GameObject model = groups[idGroup].models[_pos]; //getting the model
            if (model.GetComponent<Renderer>()) //check if has renderer
                model.GetComponent<Renderer>().enabled = true; //enable it
            if (model.transform.childCount > 0) //if his children have renderer enable them
            {
                Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                    r.enabled = true;
            }
            iTween.FadeTo(model, groups[idGroup].hashtable);
            groups[idGroup].pos_model++;  //prepare for next model in the group
        }
        //else
        //    pos_group++; //move to next group
    }

    void disableModel(object model)
    {
        GameObject _model = model as GameObject;
        Renderer[] rederers = _model.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in rederers)
            render.enabled = false;
    }
}
