using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayback : MonoBehaviour {

    public List<GameObject> models;
    private GameObject activeModel = null;

    public IEnumerator StartPlayback()
    {

        WaitForSeconds wait = new WaitForSeconds(2f);

        foreach (GameObject model in models){

            if (activeModel != null)
            {
                activeModel.SetActive(false);
            }
            activeModel = model;
            activeModel.SetActive(true);
            Animation animation = model.GetComponent<Animation>();
            wait = new WaitForSeconds(animation.clip.length);
            animation.Play();



            yield return wait;
        }
    }


}
