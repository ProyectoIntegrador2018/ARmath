using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelFade : MonoBehaviour {
    public float fadeDuration = 1.0f;
    public List<GameObject> items = new List<GameObject>();
    public bool isSerialized = false;
    public string siblingName = "";
    public string childPrefix = "";
    public int startingIndex = 0;
    public int finalIndex = 0;


    private void OnEnable()
    {

        if (!isSerialized){
            foreach (GameObject item in items)
            {

                Material material = item.GetComponent<MeshRenderer>().material;

                StartCoroutine(FadeTo(material, 0, fadeDuration));
            }
        }
        else{
            GameObject parent = this.gameObject.transform.parent.gameObject;
            GameObject sibling = parent.transform.Find(siblingName).gameObject;

            for (int i = startingIndex; i < finalIndex + 1 ; i++ ){

                string childName = childPrefix + i;
                GameObject child = GetChildGameObject(sibling, childPrefix + i);
                Material material = child.GetComponent<MeshRenderer>().material;
                StartCoroutine(FadeTo(material, 0, fadeDuration));
            }
        }

    }

     private GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        //Author: Isaac Dart, June-13.
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }


    IEnumerator FadeTo(Material material, float targetOpacity, float duration)
    {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }
}
