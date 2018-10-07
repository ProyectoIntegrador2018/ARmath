using System.Collections;
using System.Collections.Generic;
using KreliStudio;
using UnityEngine;

public class ScreenLoader : MonoBehaviour {

    public GameObject screenPrefab;
    private List<GameObject> uiScreens = new List<GameObject>();
    private List<MainTopic> mainTopics;

	
	void Start () {
        GameObject.Find("UIManager").GetComponent<UIManager>().Init();
        LoadMenu();
    }


    private void LoadMenu(){


        mainTopics = GameObject.Find("TopicManager").GetComponent<TopicManager>().topics;

        foreach(MainTopic maintopic in mainTopics){

            
            GameObject clone = Instantiate(screenPrefab, this.transform);
            clone.transform.parent = this.transform;
            uiScreens.Add(clone);

            SubMenuLoader menuLoader = clone.GetComponentInChildren<SubMenuLoader>();

            menuLoader.SetMainTopics(maintopic.mainTopics);
           

        }

    }

    public GameObject getGameScreenAt(int index){
        return uiScreens[index];
    }
}
