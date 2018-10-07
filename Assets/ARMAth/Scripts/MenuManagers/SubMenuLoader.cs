using System.Collections;
using System.Collections.Generic;
using KreliStudio;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuLoader : MonoBehaviour {


    public GameObject carouselPanel;
    public GameObject buttonPrefab;
    private List<TopicList> mainTopics;

    private void Start()
    {
       
        LoadMenu();
        carouselPanel.GetComponent<UICarouselPanel>().Init();
    }


    private void LoadMenu(){

        foreach(TopicList topicList in mainTopics){

            GameObject clone = Instantiate(buttonPrefab, this.transform);
            clone.transform.parent = this.transform;

            Text cloneText = clone.GetComponentInChildren<Text>();
            cloneText.text = topicList.listName;


        }

    }

    public void SetMainTopics(List<TopicList> mainTopics){
        this.mainTopics = mainTopics;
    }
	
   
}
