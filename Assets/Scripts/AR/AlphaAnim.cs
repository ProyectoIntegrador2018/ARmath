using UnityEngine;
using System.Collections;

public class AlphaAnim : MonoBehaviour {

    public enum AnimationType
    {
        SameTime,
        Sequence
    };

    public AnimationType type;
    public bool reset;
    public bool reverse;
    public float time;
    public GameObject[] modelos;

    private float length;
    private int pos;
    private Hashtable hashtable;

    void Start()
    {
        hashtable = new Hashtable();
        hashtable.Add("alpha", 1);
        if (type == AnimationType.Sequence)
        {
            if (GetComponent<Animation>())
                length = (float)GetComponent<Animation>().clip.length / modelos.Length;
            else
                length = time / modelos.Length;
            hashtable.Add("oncomplete", "playSequenceAlpha");
            hashtable.Add("oncompletetarget", gameObject);
        }
        else
            length = time;
        hashtable.Add("time", length);

        if (reverse)
            pos = modelos.Length - 1;
        else
            pos = 0;

        if(reset)
            resetAlpha();
        if(type == AnimationType.Sequence)
            playSequenceAlpha();
    }

    //Reset Alpha to 0
    void resetAlpha()
    {
        foreach (GameObject model in modelos)
        {
            if (model.GetComponent<Renderer>())
            {
                model.GetComponent<Renderer>().enabled = false;
                Color micolor = model.GetComponent<Renderer>().material.color;
                micolor.a = 0;
                model.GetComponent<Renderer>().material.color = micolor;
            }
            if (model.transform.childCount > 0)
            {
                Renderer[] listRenderer = model.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in listRenderer)
                {
                    r.enabled = false;
                    Color micolor = r.material.color;
                    micolor.a = 0;
                    r.material.color = micolor;
                }
            }
        }
    }

    //
    void playSequenceAlpha()
    {
        //Debug.Log("PlayAlpha en modelo: " + pos);
        if (reverse)
        {
            if (pos >= 0)
            {
                modelos[pos].GetComponent<Renderer>().enabled = true;
                iTween.FadeTo(modelos[pos], hashtable);
                pos--;
            }
        }
        else
        {
            if (pos < modelos.Length)
            {
                modelos[pos].GetComponent<Renderer>().enabled = true;
                iTween.FadeTo(modelos[pos], hashtable);
                pos++;
            }
        }
    }

    void playSameAlpha()
    {
        foreach (GameObject model in modelos)
        {
            if(model.GetComponent<Renderer>())
                model.GetComponent<Renderer>().enabled = true;

            if (model.transform.childCount > 0)
            {
                Renderer[] renders = model.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renders)
                    r.enabled = true;
            }
            iTween.FadeTo(model, hashtable);
        }
    }
}